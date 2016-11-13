using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProgressBar : MonoBehaviour {
	public Transform LoadingBar;
	public Transform TextLoading;
	public Transform TextIndicator;

	string levelName= "Splash1";
	AsyncOperation async;

	IEnumerator Start(){
		async = SceneManager.LoadSceneAsync (levelName);
		async.allowSceneActivation = false;
		while (!async.isDone)
		{
			// [0, 0.9] > [0, 1]
			float progress = Mathf.Clamp01(async.progress / 0.9f);
			Debug.Log("Loading progress: " + (progress * 100) + "%");
			Debug.Log ("async.progress= " + async.progress);

			// Loading completed
			if (async.progress == 0.9f)
			{
				//Fade.StartFadeOut (1.0f); // it is too fast to fade...
				async.allowSceneActivation = true;
			}
			yield return null;
		}

	}

	// Update is called once per frame
	void Update () {
//	version 1:		
		if (async != null) {
			if (async.progress<1) {
				TextIndicator.GetComponent<Text> ().text = (((int)100 * async.progress).ToString () + "%");
				TextLoading.gameObject.SetActive (true);
			} else {
				TextLoading.gameObject.SetActive (false);
				TextIndicator.GetComponent<Text> ().text = "Done!";
			}
			LoadingBar.GetComponent<Image> ().fillAmount = (100 * async.progress) / 100;
		}
	}
}
