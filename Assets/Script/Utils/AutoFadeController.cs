using UnityEngine;
using System.Collections;

public class AutoFadeController : MonoBehaviour {


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (LevelPlay.changeWorld == true) {
			BackgroundManager.loadSkybox (LevelPlay.cam);
			LevelPlay.changeWorld = false;
		}

		if (LevelPlay.fading != null) {
			if (LevelPlay.fading.fadeIn == true) {
				LevelPlay.fading.fadeIn = false;
				FadeIn ();
			} else if (LevelPlay.fading.fadeOut == true) {
				LevelPlay.fading.fadeOut = false;
				FadeOut ();
			} 
			if (LevelPlay.fading.fadeInOut == true) {
				LevelPlay.fading.fadeInOut = false;
				FadeInOut ();
			}
		}
		else if (Gameplay.fading != null) {
			if (Gameplay.fading.fadeIn == true) {
				Gameplay.fading.fadeIn = false;
				FadeIn ();
			} else if (Gameplay.fading.fadeOut == true) {
				Gameplay.fading.fadeOut = false;
				FadeOut ();
			} else if (Gameplay.fading.fadeInOut == true) {
				Gameplay.fading.fadeInOut = false;
				FadeInOut ();
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

		StartCoroutine(LevelPlay.fading.FadeInOut());
	
		LevelPlay.fading.fadeInOut = false;
		InputManager.active=true;
	}
}
