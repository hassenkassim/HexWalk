using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProgressBar : MonoBehaviour {
	public Transform LoadingBar;
	public Transform TextLoading;
	public Transform TextIndicator;

	string levelName= "LevelScene";
	AsyncOperation async;
	public static bool loading = true;

	IEnumerator Start(){
		SoundManager.playSplashMusic ();
		yield return new WaitForSeconds (3.5f);
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
				TextIndicator.GetComponent<Text> ().text = "100 %"; // Done
			}
			LoadingBar.GetComponent<Image> ().fillAmount = progress;
			yield return async.isDone;
		}
	}
}
