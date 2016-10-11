using UnityEngine;
using System.Collections;

public class PlayerInfo  {

	public int numStars=0;

	public static int threeStars = 3;
	public static int twoStars = 2;
	public static int oneStar = 1;


	public PlayerInfo(){

	
	}

	// algorithm to calculate the stars (depending on fields and level)
	public void countStarsPerLevel(int level){
		// how long did it take to solve the level and count the stars
		int starsPerLevel = 0;
		numStars = 3;

		int numFields = Gameplay.pathfinder.path.Count;
		float timeInThisLevel = 0.0f;

		int benchmark = numFields;

//		Debug.Log ("Number of fields for this path: " + numFields);



		//volle sterne bekommen
		Gameplay.savePlayerPrefs.saveStarsPerLevel(level,starsPerLevel);
	}

}
