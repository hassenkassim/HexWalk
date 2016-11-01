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

using System.IO;
using System;

/*
 * This class defines all Levels
 * */

public class LevelManager : MonoBehaviour {

	public const string WORLDPREF = "WORLD";
	public const string LEVELPREF = "LEVEL";
	public const string COMPLETEDPREF = "COMPLETED";
	public const string STARSPREF = "STARS";
	public const string ALLSTARS = "ALLSTARS";
	public const string CURWORLD = "CURWORLD";
	public const string CURLEVEL = "CURLEVEL";
	public const string WORLDCOMPLETED = "WORLDCOMPLETED";

	public static Level[,] levels;

	public static int stars;

	public static int curWorld;
	public static int curLevel;

	public static int worldCompleted;

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



		//Load levels
		levels = FileUtils.loadLevelsFromFile("levelListe.csv", 12, 12);

		//load stars
		stars = PlayerPrefs.GetInt(ALLSTARS, 0);

		//load current World
		curWorld = PlayerPrefs.GetInt (CURWORLD, 0);

		//load current Level
		curLevel = PlayerPrefs.GetInt(CURLEVEL, 0);

		//load world Completed
		worldCompleted = PlayerPrefs.GetInt(WORLDCOMPLETED, 0);

		//LevelPlay Gamefield
		worldMax = levels.GetLength(0);
		levelMax = levels.GetLength(1);

		cubeType = 0;
		playerType = 0;
	}
		

	public static void levelUp(){

		if (LevelPlay.playFromCurLevel == true) {
			
			//increase world
			if (PlayerPrefs.GetInt ("level") == levelMax && PlayerPrefs.GetInt ("world") <= worldMax) {
		
				//set color current
				PlayerPrefs.SetInt ("Color X:" + (PlayerPrefs.GetInt ("level") - 1) + " Y:" + ((PlayerPrefs.GetInt ("world") * 2 - 2)), LevelPlay.FIELD_COMPLETED); 

				//save Stars
				PlayerPrefs.SetInt ("Star X:" + (PlayerPrefs.GetInt ("level") - 1) + " Y:" + ((PlayerPrefs.GetInt ("world") * 2 - 2)), Gameplay.star);

				//increase world
				PlayerPrefs.SetInt ("world", PlayerPrefs.GetInt ("world") + 1);

				//increase level
				PlayerPrefs.SetInt ("level", 1);

				//unblock black field
				PlayerPrefs.SetInt ("Color X:" + (PlayerPrefs.GetInt ("level") - 1) + " Y:" + ((PlayerPrefs.GetInt ("world") * 2 - 3)), LevelPlay.FIELD_WORLDUNBLOCKED);

				//set color current
				PlayerPrefs.SetInt ("Color X:" + (PlayerPrefs.GetInt ("level") - 1) + " Y:" + ((PlayerPrefs.GetInt ("world") * 2 - 2)), LevelPlay.FIELD_SELECTED);

				print ("world:" + PlayerPrefs.GetInt ("world") + " Level:" + PlayerPrefs.GetInt ("level"));
		
				//increase only level
			} else {
			
				//set color finished
				PlayerPrefs.SetInt ("Color X:" + (PlayerPrefs.GetInt ("level") - 1) + " Y:" + ((PlayerPrefs.GetInt ("world") * 2 - 2)), LevelPlay.FIELD_COMPLETED); 

				//save Stars
				PlayerPrefs.SetInt ("Star X:" + (PlayerPrefs.GetInt ("level") - 1) + " Y:" + ((PlayerPrefs.GetInt ("world") * 2 - 2)), Gameplay.star);

				//increase level
				PlayerPrefs.SetInt ("level", PlayerPrefs.GetInt ("level") + 1);

				//set color current
				PlayerPrefs.SetInt ("Color X:" + (PlayerPrefs.GetInt ("level") - 1) + " Y:" + ((PlayerPrefs.GetInt ("world") * 2 - 2)), LevelPlay.FIELD_SELECTED); 

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
