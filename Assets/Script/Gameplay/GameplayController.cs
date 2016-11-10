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

	const int SHOWREADY = 0;
	const int SHOWREMAIN = 1;
	const int SHOWNOTHING = 2;
	const int SHOWNOTHING2 = 3;

	private int showID;
	private float timer;


	// Use this for initialization
	void Start () {
		//setup all timer here
		timer = 2;
		showID = SHOWREADY;
	}

	// Update is called once per frame
	void Update () {
		changeState ();
		countTime ();
	}

	private void changeState(){
		switch(showID){
		case SHOWREADY:
			if (timer < 0) {
				showID = SHOWREMAIN;
				timer = 2;
			}
			break;
		case SHOWREMAIN:
			if (Gameplay.pathfinder != null)
				Gameplay.pathfinder.coloring = true;
			if (timer < 0) {
				showID = SHOWNOTHING;
				Gameplay.cam.GetComponent<CameraPosition> ().startTransition ();
			}
			break;
		case SHOWNOTHING:
			if (Gameplay.pathfinder != null) {
				Gameplay.pathfinder.coloringWhite = true;
				showID = SHOWNOTHING2;
			}
			break;
		case SHOWNOTHING2:
			break;
		}
	}

	private void countTime(){
		if (timer > 0) {
			timer -= Time.deltaTime;
		}
	}
}
