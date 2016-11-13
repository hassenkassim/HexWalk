using UnityEngine;
using System.Collections;

public class LevelPlayerController : MonoBehaviour {

	const int SHOWSPLASH = 0;
	const int SHOWLEVELWORLD = 1;

	public static int showID;
	public float timer;

	public static bool explode=false;
	private float radius= 10.0f;
	private float force= 0.002f;

	GameObject splash;

	// Use this for initialization
	void Start () {
		splash = GameObject.Find ("Splash");
		if (splash.GetComponent<Splash> ().getSplashShown () == 0) {
			SoundManager.playSplashMusic ();
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


		if (explode==true) {
			explode = false;
			Vector3 tmp = new Vector3 (0.0f,0.0f,0.0f);
			foreach (Collider col in Physics.OverlapSphere(transform.position,radius)) {
				Rigidbody rb = col.GetComponent<Rigidbody>();
				if (rb != null) {
					rb.AddExplosionForce (force, tmp, radius);
				}
			}
		}

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
