using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProgressBar : MonoBehaviour {
	public Transform LoadingBar;
	public Transform TextLoading;
	public Transform TextIndicator;

	string levelName;
	AsyncOperation async;
	public static bool loading = true;

	void Start(){

//		PlayerPrefs.DeleteAll ();

		if (PlayerPrefs.GetInt ("introLoad", 0) == 0) {
				
			PlayerPrefs.SetInt ("introLoad", 1);
			// load intro
			levelName= "Scene/IntroScene";

			StartCoroutine (loadNewScene());

		} else{
			levelName= "Scene/LevelScene";
			StartCoroutine (loadNewScene());
		}
	}

	void Update(){
		StartCoroutine (fadingLoadingbar ());
	}

	IEnumerator loadNewScene(){
		SoundManager.playSplashMusic ();
		yield return new WaitForSeconds (2.0f);
		async = SceneManager.LoadSceneAsync (levelName);

		while (!async.isDone)
		{
			float progress = Mathf.Clamp01(async.progress / 0.9f);
			//Debug.Log("Loading progress: " + (progress * 100) + "%");
			if (async.progress<0.3f){
				//SoundManager.stopMusicSmoothly ();
				SoundManager.stopMusic ();
			}
			if (async.progress<0.9f) {
				TextIndicator.GetComponent<Text> ().text = (Mathf.Round(progress * 100) + "%");
				TextLoading.gameObject.SetActive (true);

			} else {
				TextLoading.gameObject.SetActive (false);
				//because of fading maybe alpha is less than 1.. so set it to 1
				Color tmp = TextIndicator.GetComponent<Text> ().color;
				tmp.a = 1.0f;
				TextIndicator.GetComponent<Text> ().color = tmp;

				TextIndicator.GetComponent<Text> ().text = "100 %"; // Done
			}
			LoadingBar.GetComponent<Image> ().fillAmount = progress;
			yield return async.isDone;
		}
	}


	IEnumerator fadingLoadingbar(){
		// loading bar fading
//		Color tmp = LoadingBar.GetComponent<Image> ().color;
//		while (true) {
//			while (tmp.a > 0) {
//				tmp.a -= 0.1f * Time.deltaTime * 20.5f;//Mathf.Lerp (0.0f, 1.0f, progress);
//				LoadingBar.GetComponent<Image> ().color = tmp;
//				yield return null;
//			}
//			while (tmp.a < 1) {
//				tmp.a += 0.1f * Time.deltaTime * 20.5f;//Mathf.Lerp (0.0f, 1.0f, progress);
//				LoadingBar.GetComponent<Image> ().color = tmp;
//				yield return null;
//			}
//		}
//		yield return null;

		// loadingText fading
		Color tmp = TextLoading.GetComponent<Text> ().color;
		while (true) {
			while (tmp.a > 0) {
				tmp.a -= Time.deltaTime * 2.5f;
				TextLoading.GetComponent<Text> ().color = tmp;
				TextIndicator.GetComponent<Text> ().color = tmp;
				yield return null;
			}
			while (tmp.a < 1) {
				tmp.a +=  Time.deltaTime * 2.5f;
				TextLoading.GetComponent<Text> ().color = tmp;
				TextIndicator.GetComponent<Text> ().color = tmp;
				yield return null;
			}
			while (tmp.a >= 1 && tmp.a < 2) {
				tmp.a+= Time.deltaTime * 2.5f;
			}
		}
//		yield return null;
	}
}
