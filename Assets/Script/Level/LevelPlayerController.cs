using UnityEngine;
using System.Collections;

public class LevelPlayerController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
		
	// Update is called once per frame
	void Update () {

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
