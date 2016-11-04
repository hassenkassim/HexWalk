using UnityEngine;
using System.Collections;

public class LevelPlayerController : MonoBehaviour {

	const int SHOWSPLASH = 0;
	const int SHOWLEVELWORLD = 1;
	public static int showID;
	public float timer1;

	// Use this for initialization
	void Start () {
		showID = SHOWSPLASH;
		timer1 = 6;
	}
	
	// Update is called once per frame
	void Update () {
		switch(showID){
		case SHOWSPLASH:
			if (timer1 < 0) {
				showID = SHOWLEVELWORLD;
			}
			break;
		case SHOWLEVELWORLD:
				LevelPlay.cam.GetComponent<CameraPositionLevelPlay> ().startTransition ();
			break;
		}

		if (timer1 > 0) {
			timer1 -= Time.deltaTime;
		}
	}

	//TODO: Gamover Check transfer to Gameover class
	void OnCollisionEnter(Collision coll)
	{
		print ("Collision");
		if (coll.collider.gameObject.CompareTag("LevelField")) {
			LevelPlay.collision ();
		}
	}


	void OnCollisionExit(Collision collisionInfo) {

	}





}
