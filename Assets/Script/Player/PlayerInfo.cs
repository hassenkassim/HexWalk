using UnityEngine;
using System.Collections;

public class PlayerInfo  {

	float timeLeft;
	float timePerLevel;

	public PlayerInfo(){
		
	
	}


	// algorithm to calculate the stars (depending on fields and level)
	public void countStarsPerLevel(int level){
		// how long did it take to solve the level and count the stars
		int starsPerLevel = 0;
		int numStars = 3;

		timePerLevel=Time.timeSinceLevelLoad;
		timeLeft-=Time.deltaTime;

		if (timeLeft < 0) {
			//sterne zaehlsystem 
			starsPerLevel = (numStars-1);
			// und felder einblenden kurz 
		}
		//volle sterne bekommen
		Gameplay.savePlayerPrefs.saveStarsPerLevel(level,starsPerLevel);
	}

}
