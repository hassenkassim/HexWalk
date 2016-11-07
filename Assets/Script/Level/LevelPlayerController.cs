using UnityEngine;
using System.Collections;

public class LevelPlayerController : MonoBehaviour {

	const int SHOWSPLASH = 0;
	const int SHOWLEVELWORLD = 1;

	public static int showID;
	public float timer;

	GameObject splash;

	// Use this for initialization
	void Start () {
		splash = GameObject.Find ("Splash");
		if (splash.GetComponent<Splash> ().getSplashShown () == 0) {
			showID = SHOWSPLASH;
			timer = 6;
		}

	}
		
	// Update is called once per frame
	void Update () {
		switch (showID) {
			case SHOWSPLASH:
				if (timer < 0) {
					showID = SHOWLEVELWORLD;
				}
				break;
		case SHOWLEVELWORLD:
			LevelPlay.cam.GetComponent<CameraPositionLevelPlay> ().startTransition ();
			showID = 2;
			break;
		}

		if (timer > 0) {
			timer -= Time.deltaTime;
		}

		LevelPlay.enableText ();

		//dursun
		BackgroundManager.setParticleSystem(LevelPlay.cam);
	}


	void OnCollisionEnter(Collision coll)
	{
		if (coll.collider.gameObject.CompareTag("LevelField")) {
			LevelPlay.collision ();
		}
	}
		
	void OnCollisionExit() {
		LevelPlay.collisionExit ();
	}
}
