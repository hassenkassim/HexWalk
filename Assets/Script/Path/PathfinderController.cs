/************************************************
 * HAMOTO Production 2016						*
 * Project: HexWalk								*
 * Authors: Tolga, Mohamed, Dursun, Hassen		*
 * Year: 2016									*
 *************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathfinderController : MonoBehaviour {
	static public PathfinderController instance; //the instance of our class that will do the work

	public static bool coloringStarted = false;
	public static bool coloringEnd = true;

	void Awake(){ //called when an instance awakes in the game
		instance = this; //set our static reference to our newly initialized instance
	}

	//This starts the coroutines (threads) for the timly painting of the fields (COLOR: WHITE)
	public static void paintWhitePath(float timebetweenfields, int startingIndex, List<Vector2> path){
		coloringStarted = true;
		coloringEnd = false;
		instance.StartCoroutine(paintFieldWhite(timebetweenfields, startingIndex, path));
	}

	//This starts the coroutines (threads) for the timly painting of the fields (COLOR: FROM PATHFINDER CLASS)
	public static void paintColorPath(float timebetweenfields, List<Vector2> path){
		coloringStarted = true;
		coloringEnd = false;
		instance.StartCoroutine (paintFieldPathColor (timebetweenfields, path));
	}


	public static IEnumerator paintFieldWhite(float timetowait, int startingIndex, List<Vector2> path){
		yield return new WaitForSeconds (timetowait);
		for (int i = startingIndex; i < Gameplay.pathfinder.path.Count - 1; i++) {
					Gameplay.gamefield.getField ((int)path [i].x, (int)path [i].y).setColor (Col.WEISS);
			yield return new WaitForSeconds (timetowait);
		}
		coloringEnd = true;
		yield return 0;
	}

	public static IEnumerator paintFieldPathColor(float timetowait, List<Vector2> path){
		yield return new WaitForSeconds (timetowait);
		for (int i = 0; i < Gameplay.pathfinder.path.Count - 1; i++) {
			Gameplay.gamefield.getField ((int)path [i].x, (int)path [i].y).setColor (Gameplay.pathfinder.pathcolor[i]);
			yield return new WaitForSeconds (timetowait);
		}
		coloringEnd = true;
		yield return 0;
	}

}
