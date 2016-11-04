using UnityEngine;
using System.Collections;

public class AutoFadeController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (LevelPlay.fading!=null ){
			if (LevelPlay.fading.fadeIn == true) 
				FadeIn();

		}

		if (LevelPlay.changeWorld == true) {
			LevelPlay.changeWorld = false;
			LevelPlay.changeWorldBackground (((int)((LevelPlay.gamePosition.y) / 2 + 1))-1);
		}
	}

	public void FadeIn(){
		if (LevelPlay.fading.overlay == null) {
			Debug.Log ("THERE IS NO OVERLAY GAMEOBJECT!");
		}
		else {
			LevelPlay.fading.overlay.gameObject.SetActive (true);
			LevelPlay.fading.overlay.pixelInset = new Rect (0, 0, Screen.width, Screen.height);

			// fade to clear secondly
			StartCoroutine(LevelPlay.fading.FadeIn());

		}
	}
}
