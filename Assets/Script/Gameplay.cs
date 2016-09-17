using UnityEngine;
using System.Collections;


/*
 * This class starts and keeps track of the Game
 * */
public class Gameplay : MonoBehaviour {

	public static Player player;
	public static Camera cam;
	public static Gamefield gamefield;


	// Use this for initialization
	void Start () {

		//Create Player
		player = new Player();
		player.setColor (Color.cyan);

		//Create Gamefield
		gamefield = new Gamefield (5, 10);
		gamefield.getField (1, 1).setColor (Color.cyan);

		//Setup Camera
		cam = Camera.main;
		cam.gameObject.AddComponent <CameraPosition>();


	}

	// Update is called once per frame
	void Update () {
		
	}

}
