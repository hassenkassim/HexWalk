using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class AutoFade{
	
	//public static GameObject tmp;
	public static float fadeTime=1.0f; 
	public static PrefabsManagerLevelPlay prefabsMgr;
	public  GUITexture overlay;
	public bool fadeIn;
	public bool fadeOut;
	public bool fadeInOut;

	public bool fadeReady=false;


	public AutoFade(GameObject overlay1)
	{

		this.fadeIn = false;
		this.fadeOut = false;
		this.fadeInOut = false;
		this.overlay=overlay1.GetComponent<GUITexture> ();
	}

	//fade to clear:
	public IEnumerator FadeIn(){
		float rate = 1.5f/fadeTime;
		float progress = 0.0f;
		this.overlay.color = Color.black;

		InputManager.active=false;

		while (progress<1.0f) {
			this.overlay.color = Color.Lerp (Color.black, Color.clear, progress);
			progress += rate * Time.deltaTime;
			yield return null;
		}
		this.overlay.color = Color.clear;
		this.overlay.gameObject.SetActive (false);
		InputManager.active=true;
		fadeReady = true;
	}

	//fade to black:
	public IEnumerator FadeOut(){
		float rate = 1.5f/fadeTime;
		float progress = 0.0f;

		overlay.color = Color.clear;
		overlay.gameObject.SetActive (true);
		InputManager.active=false;

		while (progress<1.0f) {
			overlay.color = Color.Lerp (Color.clear, Color.black, progress);
			progress += rate * Time.deltaTime;
			yield return null;
		}
		overlay.color = Color.black;
		LevelPlay.fading.fadeOut = false;
		InputManager.active=true;
		fadeReady = true;
		overlay.gameObject.SetActive (false);

	}

	public IEnumerator FadeInOut(){

		float rate = 1.5f/fadeTime;
		float progress = 0.0f;

		overlay.color = Color.clear;
		overlay.gameObject.SetActive (true);
		InputManager.active=false;

		while (progress<1.0f) {
			overlay.color = Color.Lerp (Color.clear, Color.black, progress);
			progress += rate * Time.deltaTime;
			yield return null;
		}
		overlay.color = Color.black;
		LevelPlay.fading.fadeOut = false;
		InputManager.active=true;
		fadeReady = true;


		LevelPlay.changeWorld = true;

		rate = 1.5f/fadeTime;
		progress = 0.0f;
		this.overlay.color = Color.black;

		InputManager.active=false;

		while (progress<1.0f) {
			this.overlay.color = Color.Lerp (Color.black, Color.clear, progress);
			progress += rate * Time.deltaTime;
			yield return null;
		}
		this.overlay.color = Color.clear;
		this.overlay.gameObject.SetActive (false);
		InputManager.active=true;
		fadeReady = true;
	}


}   

