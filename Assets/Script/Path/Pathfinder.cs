/************************************************
 * HAMOTO Production 2016						*
 * Project: HexWalk								*
 * Authors: Tolga, Mohamed, Dursun, Hassen		*
 * Year: 2016									*
 *************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/*
 * This class generates new paths and organizes them
 * */
public class Pathfinder {

	public List<Vector2> path;
	public List<Color> pathcolor;
	public Vector2 start;
	public Color startColor;
	public Vector2 end;
	public Color endColor;
	public bool coloring;
	public bool coloringWhite;
	public int colorCount;
	public int pointer;

	public Pathfinder(int colorCount){
			

			this.start = new Vector2(Random.Range(0, Gameplay.gamefield.width-1), 0);
			this.startColor = Col.GRUEN;
			this.end = new Vector2(Random.Range(0, Gameplay.gamefield.width-1), Gameplay.gamefield.height-1);
			this.endColor = Col.GELB; //TODO: Finish Flag

			this.colorCount = colorCount;

			path = new List<Vector2> ();
			pathcolor = new List<Color>();

			findRandomPath ();

			Gameplay.gamefield.getField ((int)end.x, (int)end.y).setColor (endColor);

			if (Gameplay.player != null) {
				Gameplay.player.setPositionByGamePosition (start);
			}
			pointer = 0;
	}

	public Pathfinder(int colorCount, int xOffset){
			this.start = new Vector2(xOffset, 0);
			this.startColor = Col.GRUEN;
			this.end = new Vector2(Random.Range(0, Gameplay.gamefield.width-1), Gameplay.gamefield.height-1);
			this.endColor = Col.GELB; //TODO: Finish Flag
			this.colorCount = colorCount;

			path = new List<Vector2> ();
			pathcolor = new List<Color>();

			findRandomPath ();

			Gameplay.gamefield.getField ((int)end.x, (int)end.y).setColor (endColor);

			/*if (Gameplay.player != null) {
				Gameplay.player.setPositionByGamePosition (start);
			}*/
			pointer = 0;
	}




	public void findRandomPath(){
		Vector2 tmp = start;
		path.Add (start);
		pathcolor.Add (Col.GRUEN);
		while(!tmp.Equals(end)){
			//find possible next steps
			List<Vector2> possibleSteps = getNextPossibleSteps(tmp);

			if (possibleSteps.Count == 0) {
				Debug.Log ("No next Step possible! Abort!");
				return;
			}

			//choose one possible step by random
			int i = Random.Range(0, possibleSteps.Count);

//			if (possibleSteps.Count > 1) {
//				int rand = Random.Range (0, 9);
//				if (rand <= 3) { // 30% get closer to the end 
//					Vector2 schlepp = possibleSteps [0];
//					for (int k = 1; k < possibleSteps.Count; k++) {
//						if (schlepp.y < possibleSteps [k].y) { // if closer to end chose this way
//							schlepp = possibleSteps [k];
//						}
//					}
//					path.Add (schlepp);
//					tmp = schlepp;
//				} else if (rand > 3) { // 70% random one from the possible list
//					path.Add (possibleSteps [i]);
//					tmp = possibleSteps [i];
//				}
//			} else { // if there is just 1 possible path, add it...
				path.Add (possibleSteps [i]);
				tmp = possibleSteps [i];
//			}
			int j = Random.Range(0, colorCount);
			pathcolor.Add (Col.colors [j]);
		}
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
		if (((int)current.y) == Gameplay.gamefield.height - 1) {
			//compare x value of CURRENT and END field
			if (current.x < end.x) {
				nextSteps.Add (new Vector2 (current.x + 1, current.y));
			} else {
				nextSteps.Add (new Vector2 (current.x - 1, current.y));
			}
			return nextSteps;
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
