/************************************************
 * HAMOTO Production 2016						*
 * Project: HexWalk								*
 * Authors: Tolga, Mohamed, Dursun, Hassen		*
 * Year: 2016									*
 *************************************************/

using UnityEngine;
using System.Collections;

/*
 * This class controls the Gameplay
 * 
 * There are several Gamplay states: SHOWREADY, SHOWREMAIN, SHOWNOTHING, SHOWNOTHING2
 * All these values represents a certain state in the game, this is used to show different texts and to control the flow of the game
 * VERY IMPORTANT: This is the class to manage the main timer!
 * */
public class GameplayController : MonoBehaviour {
	static public GameplayController instance; //the instance of our class that will do the work


	const int SHOWREADY = 0;
	const int SHOWREMAIN = 1;
	const int SHOWNOTHING = 2;
	const int SHOWNOTHING2 = 3;

	private static int showID;
	private static float timer;

	public static LevelManager levelmanager;

	void Awake(){ //called when an instance awakes in the game
		instance = this; //set our static reference to our newly initialized instance
	}


	// Use this for initialization
	void Start () {
		//setup all timer here
		timer = 1.0f;
		showID = SHOWREADY;
		InputManager.active = false;
		levelmanager = new LevelManager();
	}

	// Update is called once per frame
	void Update () {
		if (changeState () == true) {
			countTime ();
		}
	}

	private bool changeState(){
		switch(showID){
		case SHOWREADY:
			if (timer < 0) {
				showID = SHOWREMAIN;
				timer = 1.0f;
			}
			return true;
		case SHOWREMAIN:
			if (PathfinderController.coloringStarted == false) {
				PathfinderController.paintColorPath (getTimeBetweenFields(), Gameplay.pathfinder.path);
				return false;
			}

			if (PathfinderController.coloringEnd == true) {
				/*if (starteddd == false) {
					starteddd = true;
					if ((LevelPlay.levelmgr.curLevel.getWorld () % 2) == 1) {
						instance.StartCoroutine (PlayerController.startswitchingcolor (0.8f));
					}
				}*/

				if (timer < 0) {
					PathfinderController.coloringStarted = false;
					showID = SHOWNOTHING;
					if (Gameplay.first == true) {
						Gameplay.cam.GetComponent<CameraPosition> ().startTransition ();
					} else {
						
					}
				}
				return true;
			} else {
				return false;
			}
		case SHOWNOTHING:
			if (PathfinderController.coloringStarted == false) {
				if(Gameplay.first == true) PathfinderController.paintWhitePath (getTimeBetweenFields(), 1, Gameplay.pathfinder.path);
				else PathfinderController.paintWhitePath (getTimeBetweenFields(), 0, Gameplay.pathfinder.path);
				return false;
			}

			if (PathfinderController.coloringEnd == true) {
				
				if (timer < 0) {
					showID = SHOWNOTHING2;
				}
				return true;
			} else {
				return false;
			}
		}
		return false;
	}

	private void countTime(){
		if (timer > 0) {
			timer -= Time.deltaTime;
		}
	}

	public static void resetGameplayController(){
		timer = 1;
		PathfinderController.coloringStarted = false;
		PathfinderController.coloringEnd = true;
		showID = SHOWREADY;
	}

	private float getTimeBetweenFields(){
		int level = LevelPlay.levelmgr.curLevel.getLevel ();
		int world = LevelPlay.levelmgr.curLevel.getWorld ();
		return (0.1f + ((level+1) * (world+1) * 0.003f));
	}
}
