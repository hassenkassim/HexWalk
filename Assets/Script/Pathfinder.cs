using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/*
 * This class generates new paths and organizes them
 * */
public class Pathfinder {

	public List<Vector2> path;
	public Vector2 start;
	public Vector2 end;

	public Pathfinder(){
		if (Gameplay.gamefield != null) {
			this.start = new Vector2(Random.Range(0, Gameplay.gamefield.width-1), 0);
			this.end = new Vector2(Random.Range(0, Gameplay.gamefield.width-1), Gameplay.gamefield.height-1);
			path = new List<Vector2> ();
			findRandomPath ();
			Gameplay.gamefield.getField ((int)end.x, (int)end.y).setColor (Color.blue);
			if (Gameplay.player != null) {
				Gameplay.player.setPositionByGamePosition (start);
			}
		} else {
			Debug.Log ("No Gamefield, can't find any paths without a Gamefield!");
		}
	}

	public void findRandomPath(){
		Vector2 tmp = start;
		path.Add (start);
		while(!tmp.Equals(end)){
			//find possible next steps
			List<Vector2> possibleSteps = getNextPossibleSteps(tmp);

			if (possibleSteps.Count == 0) {
				Debug.Log ("No next Step possible! Abort!");
				return;
			}

			//choose one possible step by random
			int i = Random.Range(0, possibleSteps.Count);
			path.Add (possibleSteps [i]);
			Debug.Log ("x: " + (int)possibleSteps [i].x + "  y: " + (int)possibleSteps [i].y);

			Gameplay.gamefield.getField ((int)possibleSteps [i].x, (int)possibleSteps [i].y).setColor (Color.yellow);
			tmp = possibleSteps [i];
		}
		Debug.Log ("RandomPath generated successfully!");
	}
		


	/*
	 * Rules for the next steps:
	 * 1. It can't be outside the gamefield
	 * 2. It can't be back, that means y value >= currentY
	 * 3. You can't go to the previous step
	 * */
	private List<Vector2> getNextPossibleSteps(Vector2 current){
		List<Vector2> nextSteps = new List<Vector2> ();

		//Player is on the last row, it should only be able to go to end, not the other way round
		if (current.y == Gameplay.gamefield.height - 1) {
			//compare x value of CURRENT and END field
			if (current.x < end.x) {
				nextSteps.Add (new Vector2(current.x+1,current.y));
				return nextSteps;
			}
		}


		if (current.x < Gameplay.gamefield.width-1) {
			Vector2 tmp = new Vector2 (current.x + 1, current.y);
			if(!path.Contains(tmp)) nextSteps.Add (tmp);
		}
		if (current.x > 0) {
			Vector2 tmp = new Vector2 (current.x - 1, current.y);
			if(!path.Contains(tmp)) nextSteps.Add (tmp);
		}
		if (current.y < Gameplay.gamefield.height-1) {
			Vector2 tmp = new Vector2 (current.x, current.y+1);
			if(!path.Contains(tmp)) nextSteps.Add (tmp);
		}
			
		return nextSteps;
	}




}
