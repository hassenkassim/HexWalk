using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class AutoFade
{

	//public static GameObject tmp;
	public static float fadeTime=0.5f; 
	public static PrefabsManagerLevelPlay prefabsMgr;
	public  GUITexture overlay;
	public bool fadeIn;



	public AutoFade(GameObject overlay1)
	{
		this.fadeIn = false;
		this.overlay=overlay1.GetComponent<GUITexture> ();
	}


	public IEnumerator FadeIn(){
		//fade to black:
		overlay.gameObject.SetActive (true);
		overlay.color = Color.clear;
		InputManager.active=false;

		float rate = 1.5f/fadeTime;
		float progress = 0.0f;
		while (progress<1.0f) {
			overlay.color = Color.Lerp (Color.clear, Color.black, progress);
			progress += rate * Time.deltaTime;
			yield return null;
		}
		overlay.color = Color.black;

		// change the world here ....
		yield return null;
		LevelPlay.changeWorld = true;

		//fade to clear:
		this.overlay.color = Color.black;

		progress = 0.0f;
		while (progress<1.0f) {
			this.overlay.color = Color.Lerp (Color.black, Color.clear, progress);
			progress += rate * Time.deltaTime;
			yield return null;
		}
		this.overlay.color = Color.clear;
		this.overlay.gameObject.SetActive (false);
		LevelPlay.fading.fadeIn = false;
		InputManager.active=true;

	}
}   