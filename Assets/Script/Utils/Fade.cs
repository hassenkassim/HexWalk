using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class Fade : MonoBehaviour {
	static public Fade instance; //the instance of our class that will do the work

	public static string sceneStr;
	public static GUITexture overlay;
	private static bool changeWorld = false;

	void Awake(){ //called when an instance awakes in the game
		instance = this; //set our static reference to our newly initialized instance
		overlay = (GUITexture)GameObject.Find ("FadingOverlay").GetComponent <GUITexture> ();
		overlay.color = Color.black;
		overlay.gameObject.SetActive (false);
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		
	}


	public static void StartFadeIn (float time){
		overlay.gameObject.SetActive (true);
		instance.StartCoroutine (FadeIn (time));
	}

	public static void StartFadeOut(float time){
		overlay.gameObject.SetActive (true);
		instance.StartCoroutine (FadeOut (time));
	}

	public static void FadeAndStartLevel(string sceneName, float time){
		sceneStr = sceneName;
		changeWorld = true;
		StartFadeOut (time);
	}


	//fade to clear:
	public static IEnumerator FadeIn(float fadeTime){

		float rate = 1.5f/fadeTime;
		float progress = 0.0f;

		while (progress<1.0f) {
			if (progress > 0.5f && changeWorld) {
				instance.StartCoroutine(startLevel ());
				changeWorld = false;
				yield break;
			}
			overlay.color = Color.Lerp (Color.black, Color.clear, progress);
			progress += rate * Time.deltaTime;

			yield return null;
		}
		yield return null;
	}

	//fade to black:
	public static IEnumerator FadeOut(float fadeTime){
		float rate = 1.5f/fadeTime;
		float progress = 0.0f;

		while (progress<1.0f) {
			if (progress > 0.5f && changeWorld) {
				instance.StartCoroutine(startLevel ());
				changeWorld = false;
				yield break;
			}
			overlay.color = Color.Lerp (Color.clear, Color.black, progress);
			progress += rate * Time.deltaTime;

			yield return null;
		}

		yield return null;
	}


	public static IEnumerator startLevel() {
		AsyncOperation async = Application.LoadLevelAsync(sceneStr);
		while (!async.isDone) {
			yield return null;
		}
	}
}
