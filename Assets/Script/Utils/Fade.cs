using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class Fade : MonoBehaviour {
	static public Fade instance; //the instance of our class that will do the work

	public static string sceneStr;
	public static GUITexture overlay;
	private static bool changeScene = false; // to load new scene
	private static bool changeWorld = false; // to load new world


	void Awake(){ //called when an instance awakes in the game
		instance = this; //set our static reference to our newly initialized instance
		overlay = (GUITexture)GameObject.Find ("FadingOverlay").GetComponent <GUITexture> ();
		overlay.color = Color.black;
		overlay.gameObject.SetActive (false);
	}


	public static void StartFadeIn (float time){
		InputManager.active = false;
		overlay.gameObject.SetActive (true);
		instance.StartCoroutine (FadeIn (time));
	}

	public static void StartFadeOut(float time){
		InputManager.active = false;
		overlay.gameObject.SetActive (true);
		instance.StartCoroutine (FadeOut (time));
	}

	public static void FadeAndStartScene(string sceneName, float time){
		sceneStr = sceneName;
		changeScene = true;
		StartFadeOut (time);
	}

	public static void FadeAndNewWorld(float time){
		changeWorld = true;
		StartFadeOut (time);
	}

	//fade to clear:
	public static IEnumerator FadeIn(float fadeTime){

		float rate = 1.5f/fadeTime;
		float progress = 0.0f;

		while (progress<1.0f) {
			if (progress > 0.5f && changeScene) { // changing the scene 
				instance.StartCoroutine(startScene ());
				changeScene = false;
				yield break;
			}
			overlay.color = Color.Lerp (Color.black, Color.clear, progress);
			progress += rate * Time.deltaTime;

			yield return null;
		}
		InputManager.active = true;
		yield return null;
	}

	//fade to black:
	public static IEnumerator FadeOut(float fadeTime){
		float rate = 1.5f/fadeTime;
		float progress = 0.0f;

		while (progress<1.0f) {
			if (progress > 0.5f && changeScene) {
				instance.StartCoroutine(startScene ());
				changeScene = false;
				yield break;
			}

			if (progress > 0.5f && changeWorld) { // changing the world and fadeOut
				instance.StartCoroutine(FadeIn(fadeTime));
				changeWorld=false;
				BackgroundManager.loadSkybox (LevelPlay.cam);
				yield break;
			}

			overlay.color = Color.Lerp (Color.clear, Color.black, progress);
			progress += rate * Time.deltaTime;

			yield return null;
		}
		InputManager.active = true;
		yield return null;
	}


	public static IEnumerator startScene() {
		AsyncOperation async = Application.LoadLevelAsync(sceneStr);
		while (!async.isDone) {
			yield return null;
		}
	}
}
