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

		worldUp = false;
		levelMax = 10;
		worldMax = 10;

		//savedColor [levelMax * LevelPlay.height] = PlayerPrefsX.GetColorArray ("levelFieldColor");

		for (int i = 0; i < levelMax; i++) {
			for (int j = 0; j < LevelPlay.height; j++) {
				fieldColor [i, j] = savedColor [j * levelMax + i];
			}
		}

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

		//color finished field
		LevelPlay.fieldColor [(PlayerPrefs.GetInt ("level") - 1), (int)((PlayerPrefs.GetInt("world") - 1) * 2)] = LevelPlay.fields [(PlayerPrefs.GetInt ("level") - 1), (int)((PlayerPrefs.GetInt("world") - 1) * 2)].GetComponent<MeshRenderer> ().material.color = LevelPlay.finishedColor;

		//increase level
		PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") + 1);
		//color current field
		LevelPlay.fieldColor [(PlayerPrefs.GetInt ("level") - 1), (int)((PlayerPrefs.GetInt("world") - 1) * 2)] = LevelPlay.fields [(PlayerPrefs.GetInt ("level") - 1), (int)((PlayerPrefs.GetInt("world") - 1) * 2)].GetComponent<MeshRenderer> ().material.color = LevelPlay.currentColor;


		//increase world
		if (PlayerPrefs.GetInt("level") == levelMax + 1 || PlayerPrefs.GetInt("world") <= worldMax) {
			worldUp = true;
			//increase world
			PlayerPrefs.SetInt ("world", PlayerPrefs.GetInt ("world") + 1);
			//increase level
			PlayerPrefs.SetInt ("level", 1);

			LevelPlay.fieldColor [(PlayerPrefs.GetInt ("level") - 1), (int)((PlayerPrefs.GetInt("world") - 1) * 2)] = LevelPlay.fields [(PlayerPrefs.GetInt ("level") - 1), (int)((PlayerPrefs.GetInt("world") - 1) * 2)].GetComponent<MeshRenderer> ().material.color = LevelPlay.currentColor;

		}
		/*
		savedColor [levelMax * LevelPlay.height] = PlayerPrefsX.GetColorArray ("levelFieldColor");

		for (int i = 0; i < levelMax; i++) {
			for (int j = 0; j < LevelPlay.height; j++) {
				fieldColor [i, j] = savedColor [j * levelMax + i];
			}
		}

		for(int i = 0; i < LevelPlay.height; i++){
			for(int j = 0; j < levelMax; j++){
				Color color;
				color = fieldColor[j,i];
				savedColor[i*LevelPlay.height+j] = color;

			}
		}

		PlayerPrefsX.SetColorArray("levelFieldColor", savedColor);
		*/


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
