/************************************************
 * HAMOTO Production 2016						*
 * Project: HexWalk								*
 * Authors: Tolga, Mohamed, Dursun, Hassen		*
 * Year: 2016									*
 *************************************************/

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

	public int initialWidth;
	public int initialHeight;
	public int numberOfColor;
	public int levelCounter;
	public int worldCounter;
	public Color[,] fieldColor;
	public List<Color> savedColor;


	public static int cubeType;
	public static int playerType;


	public static int worldMax; //number of worlds
	public static int levelMax; //number of levels

	public static bool worldUp;

	public int color;

	// Use this for initialization
	public void Awake(){ 

		//Create Gamefield
		initialWidth = 4;
		initialHeight = 5;
		numberOfColor = 2;

		levelMax = 7;
		worldMax = 10;

		cubeType = 0;
		playerType = 0;


		if (PlayerPrefs.HasKey ("world") == false) {
			PlayerPrefs.SetInt ("world", 1);
		}

		if (PlayerPrefs.HasKey ("level") == false) {
			PlayerPrefs.SetInt ("level", 1);
		}

		if (PlayerPrefs.HasKey ("gameFieldWidth") == false) {
			PlayerPrefs.SetInt ("gameFieldWidth", initialWidth);
		}

		if (PlayerPrefs.HasKey ("gameFieldHeight") == false) {
			PlayerPrefs.SetInt ("gameFieldHeight", initialHeight);
		}

		if (PlayerPrefs.HasKey ("numberOfColor") == false) {
			PlayerPrefs.SetInt ("numberOfColor", 1);
		}

		PlayerPrefs.Save ();
	}
		

	//Setting difficulty grade up and loading a new Scene

	public static void levelUp(){

		if (LevelPlay.playFromCurLevel == true) {
			
			//increase world
			if (PlayerPrefs.GetInt ("level") == levelMax && PlayerPrefs.GetInt ("world") <= worldMax) {
		
				//set color current
				PlayerPrefs.SetInt ("Color X:" + (PlayerPrefs.GetInt ("level") - 1) + " Y:" + ((PlayerPrefs.GetInt ("world") * 2 - 2)), LevelPlay.finCol); 

				//save Stars
				PlayerPrefs.SetInt ("Star X:" + (PlayerPrefs.GetInt ("level") - 1) + " Y:" + ((PlayerPrefs.GetInt ("world") * 2 - 2)), Gameplay.star);

				//increase world
				PlayerPrefs.SetInt ("world", PlayerPrefs.GetInt ("world") + 1);

				//increase level
				PlayerPrefs.SetInt ("level", 1);

				//unblock black field
				PlayerPrefs.SetInt ("Color X:" + (PlayerPrefs.GetInt ("level") - 1) + " Y:" + ((PlayerPrefs.GetInt ("world") * 2 - 3)), LevelPlay.unlockCol);

				//set color current
				PlayerPrefs.SetInt ("Color X:" + (PlayerPrefs.GetInt ("level") - 1) + " Y:" + ((PlayerPrefs.GetInt ("world") * 2 - 2)), LevelPlay.curCol);

				print ("world:" + PlayerPrefs.GetInt ("world") + " Level:" + PlayerPrefs.GetInt ("level"));
		
				//increase only level
			} else {
			
				//set color finished
				PlayerPrefs.SetInt ("Color X:" + (PlayerPrefs.GetInt ("level") - 1) + " Y:" + ((PlayerPrefs.GetInt ("world") * 2 - 2)), LevelPlay.finCol); 

				//save Stars
				PlayerPrefs.SetInt ("Star X:" + (PlayerPrefs.GetInt ("level") - 1) + " Y:" + ((PlayerPrefs.GetInt ("world") * 2 - 2)), Gameplay.star);

				//increase level
				PlayerPrefs.SetInt ("level", PlayerPrefs.GetInt ("level") + 1);

				//set color current
				PlayerPrefs.SetInt ("Color X:" + (PlayerPrefs.GetInt ("level") - 1) + " Y:" + ((PlayerPrefs.GetInt ("world") * 2 - 2)), LevelPlay.curCol); 

				print ("world:" + PlayerPrefs.GetInt ("world") + " Level:" + PlayerPrefs.GetInt ("level"));
			}
		} else {
			//save Stars
			PlayerPrefs.SetInt("Star X:" + LevelPlay.gamePosition.x + " Y:" + LevelPlay.gamePosition, Gameplay.star);
		}


	}
		

	//Decrease field to initial value
	public static void levelReset(){
		
	}

	public static int getLevelMax(){
		return levelMax;
	}

	public static int getWorldMax(){
		return worldMax;
	}
		

}
