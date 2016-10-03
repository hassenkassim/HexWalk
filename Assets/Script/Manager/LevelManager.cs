/************************************************
 * HAMOTO Production 2016						*
 * Project: HexWalk								*
 * Authors: Tolga, Mohamed, Dursun, Hassen		*
 * Year: 2016									*
 *************************************************/

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager {

	public int initialWidth;
	public int initialHeight;
	public int levelCounter;


	// Use this for initialization
	public LevelManager () {
		//Initialize LevelCounter
		levelCounter=0;

		//Create Gamefield
		initialWidth = 4;
		initialHeight = 5;


		if (PlayerPrefs.HasKey ("gameFieldWidth") == false) {
			PlayerPrefs.SetInt ("gameFieldWidth", initialWidth);
		}

		if (PlayerPrefs.HasKey ("gameFieldHeight") == false) {
			PlayerPrefs.SetInt ("gameFieldHeight", initialHeight);
		}
	}

	//Setting difficulty grade up and loading a new Scene
	public void levelUp(){
		levelCounter++;

		PlayerPrefs.SetInt ("gameFieldWidth", PlayerPrefs.GetInt("gameFieldWidth") + 1);
		PlayerPrefs.SetInt ("gameFieldHeight", PlayerPrefs.GetInt("gameFieldHeight") + 1);
		SceneManager.LoadScene ("GameScene");
	}

	//Setting difficulty grade down
	public void levelReset(){

		PlayerPrefs.SetInt ("gameFieldWidth", initialWidth);
		PlayerPrefs.SetInt ("gameFieldHeight", initialHeight);
	}

}
