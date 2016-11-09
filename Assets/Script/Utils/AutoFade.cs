using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class AutoFade{
	/*
	//public static GameObject tmp;
	public static float fadeTime=2.0f; 
	public static PrefabsManagerLevelPlay prefabsMgr;
	public  GUITexture overlay;
	public bool fadeIn;
	public bool fadeOut;
	public static bool fadeStarted;
	public static bool fadeDone;


	public AutoFade(GameObject overlay1)
	{
		this.fadeIn = false;
		this.fadeOut = false;
		fadeDone = false;
		fadeStarted = false;
		this.overlay=overlay1.GetComponent<GUITexture> ();
	}

	//fade to clear:
	public IEnumerator FadeIn(){
		float rate = 1.5f/fadeTime;
		float progress = 0.0f;
		this.overlay.color = Color.black;

		while (progress<1.0f) {
			this.overlay.color = Color.Lerp (Color.black, Color.clear, progress);
			progress += rate * Time.deltaTime;
			yield return null;
		}

		this.overlay.color = Color.clear;
		this.overlay.gameObject.SetActive (false);
		fadeDone = true;
		yield return null;
	}

	//fade to black:
	public IEnumerator FadeOut(){
		fadeStarted = true;
		float rate = 1.5f/fadeTime;
		float progress = 0.0f;

		this.overlay.color = Color.clear;

		while (progress<1.0f) {
			this.overlay.color = Color.Lerp (Color.clear, Color.black, progress);
			progress += rate * Time.deltaTime;
			yield return null;
		}
		this.overlay.color = Color.black;
		this.overlay.gameObject.SetActive (false);
		Debug.Log ("SETZE AUF TRUE");
		fadeDone = true;
		yield return null;
	}

	public IEnumerator FadeInOut(){
		float rate = 1.5f/fadeTime;
		float progress = 0.0f;

		this.overlay.color = Color.clear;

		while (progress<1.0f) {
			this.overlay.color = Color.Lerp (Color.clear, Color.black, progress);
			progress += rate * Time.deltaTime;
			yield return null;
		}
		this.overlay.color = Color.black;

		LevelPlay.changeWorld = true;

		rate = 1.5f/fadeTime;
		progress = 0.0f;
		this.overlay.color = Color.black;


		while (progress<1.0f) {
			this.overlay.color = Color.Lerp (Color.black, Color.clear, progress);
			progress += rate * Time.deltaTime;
			yield return null;
		}
		this.overlay.color = Color.clear;
		this.overlay.gameObject.SetActive (false);

		yield return null;
	}
	*/
}   