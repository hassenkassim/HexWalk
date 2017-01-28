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

public class LevelManager {

	public const string WORLDPREF = "WORLD";
	public const string LEVELPREF = "LEVEL";
	public const string COMPLETEDPREF = "COMPLETED";
	public const string CURWORLD = "CURWORLD";
	public const string CURLEVEL = "CURLEVEL";
	public const string NEXTWORLD = "NEXTWORLD";
	public const string NEXTLEVEL = "NEXTLEVEL";
	public const string WORLDCOMPLETED = "WORLDCOMPLETED";

	public Level[,] levels;
	public Level curLevel;

	public int worldCompleted;

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
	public LevelManager(){

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

	public bool unlockWorld(Level curLevel){
		bool worldcompleted = isWorldCompleted(curLevel);

		if (worldcompleted == true) {
			AdManager.showRewardedVideo ();

			//unlock next World
			if (curLevel.getWorld() < worldMax) {
				PlayerPrefs.SetInt (WORLDCOMPLETED, (curLevel.getWorld() + 1));
			}
			return true;
		} else {
			return false;
		}
	}
		
	public void nextLevel(Level curLevel){
		int nWorld = curLevel.getWorld ();
		int nLevel = 0;

		if (isWorldCompleted (curLevel) && curLevel.getLevel() == levelMax-1) {
			if (curLevel.getWorld () < worldMax) {
				nWorld = curLevel.getWorld () + 1;
			} else {
				//TODO: GAME END
			}
		} else {
			if (curLevel.getLevel () < levelMax-1) { // TODO 
				nLevel = curLevel.getLevel () + 1;
			} else {
				//give me next free level
				for (int i = 0; i < levelMax; i++) {
					if (levels [curLevel.getWorld (), i].getCompleted () == 0) {
						nLevel = i;
						break;
					}
				}
			}
		}

		setNextLevel (nWorld, nLevel);
	}

	private bool isWorldCompleted(Level curLevel){
		int worldnr = curLevel.getWorld ();

		//check wether all Levels in the World are completed
		for (int i = 0; i < levelMax; i++) {
			if (levels [worldnr, i].getCompleted () == 0) {
				return false;
			}
		}
		return true;
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

	public Level getLevel(int world, int level){
		return levels [world, level];
	}

	public void setCurrentLevel(int world, int level){
		curLevel = levels [world, level];
		PlayerPrefs.SetInt (CURLEVEL, curLevel.getLevel());
		PlayerPrefs.SetInt (CURWORLD, curLevel.getWorld());
	}

	public Level getCurrentLevel(){
		return curLevel;
	}

	public void setNextLevel(int nworld, int nlevel){
		PlayerPrefs.SetInt (NEXTWORLD, nworld);
		PlayerPrefs.SetInt (NEXTLEVEL, nlevel);
	}
}
