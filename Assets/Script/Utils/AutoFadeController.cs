using UnityEngine;
using System.Collections;

public class AutoFadeController : MonoBehaviour {

	/*
	// Use this for initialization
	void Start () {

	}
	// Update is called once per frame
	void Update () {
		if (LevelPlay.changeWorld == true) {
			BackgroundManager.loadSkybox (LevelPlay.cam);
			LevelPlay.changeWorld = false;
		}

		if (!Camera.main.GetComponent<Skybox> ().material.name.Equals ("skybox" + (((int)((LevelPlay.gamePosition.y) / 2 + 1)) - 1))) {
			FadeInOut ();
		}

		if (LevelPlay.fading != null) {
			if (LevelPlay.fading.fadeIn == true) {
				LevelPlay.fading.fadeIn = false;
				InputManager.active=false;
				FadeIn ();
				InputManager.active=true;
			} 
			if (LevelPlay.fading.fadeOut == true) {
				LevelPlay.fading.fadeOut = false;
				InputManager.active=false;
				FadeOut ();
				InputManager.active=true;
			} 
		}
		else if (Gameplay.fading != null) {
			if (Gameplay.fading.fadeIn == true) {
				Gameplay.fading.fadeIn = false;
				FadeIn ();
			} 
			if (Gameplay.fading.fadeOut == true) {
				Gameplay.fading.fadeOut = false;
				FadeOut ();
			}
		}
	}

	public void FadeIn(){
		LevelPlay.fading.overlay.gameObject.SetActive (true);
		LevelPlay.fading.overlay.pixelInset = new Rect (0, 0, Screen.width, Screen.height);

		StartCoroutine(LevelPlay.fading.FadeIn());
	}

	public void FadeOut(){
		LevelPlay.fading.overlay.gameObject.SetActive (true);
		LevelPlay.fading.overlay.pixelInset = new Rect (0, 0, Screen.width, Screen.height);

		StartCoroutine(LevelPlay.fading.FadeOut());
	}

	public void FadeInOut(){
		LevelPlay.fading.overlay.gameObject.SetActive (true);
		LevelPlay.fading.overlay.pixelInset = new Rect (0, 0, Screen.width, Screen.height);

		InputManager.active=false;

		StartCoroutine(LevelPlay.fading.FadeInOut());
	
		InputManager.active=true;
	}
	*/
}
