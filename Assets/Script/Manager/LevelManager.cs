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
	public const string CURWORLD = "CURWORLD";
	public const string CURLEVEL = "CURLEVEL";
	public const string NEXTWORLD = "NEXTWORLD";
	public const string NEXTLEVEL = "NEXTLEVEL";
	public const string WORLDCOMPLETED = "WORLDCOMPLETED";

	public static Level[,] levels;
	public static Level curLevel;

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

		//load world Completed
		worldCompleted = PlayerPrefs.GetInt(WORLDCOMPLETED, 0);

		//LevelPlay Gamefield
		worldMax = levels.GetLength(0);
		levelMax = levels.GetLength(1);

		cubeType = 0;
		playerType = 0;
	}
		
	//setCurrentLevel to NextLevel and enable next World if max Level is finished
	public static void levelUp(){
		int nextWorld = 0;
		int nextLevel = 0;

		bool worldcompleted = true;

		//check wether all Levels in the World are completed
		for (int i = 0; i < levelMax; i++) {
			if (levels [curLevel.getWorld (), i].getCompleted () == 0) {
				worldcompleted = false;
				break;
			}
		}

		if (worldcompleted == true) {
			//NextWorld
			if (nextWorld != worldMax) {
				nextWorld = curLevel.getWorld () + 1;
				nextLevel = 0;
				PlayerPrefs.SetInt (WORLDCOMPLETED, nextWorld);
				worldCompleted = nextWorld;
				//LevelPlay.enableWorld (nextWorld);
			}
		} else {
			nextWorld = curLevel.getWorld ();
			//give the next Level OR the next not completed Level
			if (curLevel.getLevel () != levelMax - 1) {
				nextLevel = curLevel.getLevel () + 1;
			} else {
				for (int i = 0; i < levelMax; i++) {
					if (levels [curLevel.getWorld (), i].getCompleted () == 0) {
						nextLevel = i;
						break;
					}
				}
			}
		}

		PlayerPrefs.SetInt (NEXTWORLD, nextWorld);
		PlayerPrefs.SetInt (NEXTLEVEL, nextLevel);


		return;

		/*

		if (LevelPlay.playFromCurLevel == true) {
			
			//increase world
			if (PlayerPrefs.GetInt ("level") == levelMax && PlayerPrefs.GetInt ("world") <= worldMax) {
		
				//set color current
				PlayerPrefs.SetInt ("Color X:" + (PlayerPrefs.GetInt ("level") - 1) + " Y:" + ((PlayerPrefs.GetInt ("world") * 2 - 2)), LevelPlay.FIELD_COMPLETED); 

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

				//increase level
				PlayerPrefs.SetInt ("level", PlayerPrefs.GetInt ("level") + 1);

				//set color current
				PlayerPrefs.SetInt ("Color X:" + (PlayerPrefs.GetInt ("level") - 1) + " Y:" + ((PlayerPrefs.GetInt ("world") * 2 - 2)), LevelPlay.FIELD_SELECTED); 

				print ("world:" + PlayerPrefs.GetInt ("world") + " Level:" + PlayerPrefs.GetInt ("level"));
			}
		} else {

		}*/
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

	public static Level getLevel(int world, int level){
		return levels [world, level];
	}

	public static void setCurrentLevel(int world, int level){
		curLevel = levels [world, level];
		PlayerPrefs.SetInt (CURLEVEL, curLevel.getLevel());
		PlayerPrefs.SetInt (CURWORLD, curLevel.getWorld());

//		print ("Class: LevelManager; Function: setCurrentLevel");
//		print ("world: " + world + "; Level: " + level);

	}

	public static Level getCurrentLevel(){
		return curLevel;
	}
		

}
