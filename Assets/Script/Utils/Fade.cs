﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class Fade : MonoBehaviour {
	static public Fade instance; //the instance of our class that will do the work

	public static LevelManager levelmgr;
	public static string sceneStr;
	public static GUITexture overlay;
	private static bool changeScene = false; // to load new scene
	private static bool changeWorld = false; // to load new world
	private static bool fadeOutbool = false; // to load new world

	private static bool changeCube = false;



	void Awake(){ //called when an instance awakes in the game
		instance = this; //set our static reference to our newly initialized instance
		overlay = (GUITexture)GameObject.Find ("FadingOverlay").GetComponent <GUITexture> ();
		overlay.color = Color.black;
		overlay.gameObject.SetActive (false);

		levelmgr = new LevelManager ();  
	}
		
	public static void StartFadeIn (float time){
		//Debug.Log ("FadeIn");
		InputManager.active = false;
		overlay.gameObject.SetActive (true);
		instance.StartCoroutine (FadeIn (time));
	}

	public static void StartFadeOut(float time, Camera cam){
		//Debug.Log ("FadeOut");
		InputManager.active = false;
		overlay.gameObject.SetActive (true);
		instance.StartCoroutine (FadeOut (time, cam));
	}

	public static void FadeAndStartScene(string sceneName, float time, Camera cam){
		//Debug.Log ("FadeAndStartScene");
		sceneStr = sceneName;
		changeScene = true;
		StartFadeOut (time, cam);
	}

	public static void FadeAndNewWorld(float time, Camera cam){
		changeWorld = true;
		fadeOutbool = true;
		StartFadeOut (time, cam);
	}
	public static void FadeAndNewWorldForGameplay(float time, Camera cam){

		//AdManager.showRewardedVideo ();
		changeCube = true;
		changeWorld = true;
		StartFadeOut (time, cam);
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
		//trotzdem lassen
		Scene tmp =SceneManager.GetActiveScene();


		if (tmp.name== "LevelScene" || tmp.name== "Scene/LevelScene" && LevelPlay.playerobj.transform.position.y <= 1.4f) {
			if (fadeOutbool) {
				fadeOutbool = false;
				InputManager.active = true;
			}
			//SoundManager.playLevelMusic ((int)LevelPlay.playerobj.transform.position.z / 2 + 1);
		//} else if (Application.loadedLevelName == "GameScene" && PathfinderController.camID == true) {

			//SoundManager.playLevelMusic ((int)LevelPlay.playerobj.transform.position.z / 2 + 1);
		} else if (tmp.name == "GameScene"  && PathfinderController.camID == true) {

			InputManager.active = true;
		}
		yield return null;
	}



	//fade to black:
	public static IEnumerator FadeOut(float fadeTime, Camera cam){
		float rate = 1.5f/fadeTime;
		float progress = 0.0f;

		//if (Application.loadedLevelName == "LevelScene") {
		//	SoundManager.stopMusic ();
		//}

		Scene tmp = SceneManager.GetActiveScene();
//		if (tmp.name == "LevelScene") {
//			SoundManager.stopMusic ();
//		}

		while (progress<1.0f) {
			if (progress > 0.5f && changeScene) {
				instance.StartCoroutine(startScene ());
				changeScene = false;
				yield break;
			}

			if (progress > 0.5f && changeWorld) { // changing the world and fadeOut
				instance.StartCoroutine(FadeIn(fadeTime));
				changeWorld=false;
				BackgroundManager.loadSkybox (cam);
				if (changeCube == true) {
					Gameplay.changeCubeGameplay ();
					changeCube = false;
				}
				yield break;
			}

			overlay.color = Color.Lerp (Color.clear, Color.black, progress);
			progress += rate * Time.deltaTime;
			yield return null;
		}
		if (tmp.name== "LevelScene" || tmp.name== "Scene/LevelScene" && LevelPlay.playerobj.transform.position.y <= 1.5f) {
			InputManager.active = true; 
		} else if (tmp.name == "GameScene"  && PathfinderController.camID == true) {

			InputManager.active = true;
		}
		yield return null;

	}

	public static IEnumerator startScene() {
		AsyncOperation async = SceneManager.LoadSceneAsync(sceneStr);
		while (!async.isDone) {
			yield return null;
		}
	}
}
