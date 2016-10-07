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

public class LevelManager : MonoBehaviour {

	public int initialWidth;
	public int initialHeight;
	public int levelCounter;


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

		worldUp = false;
		levelMax = 24;
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

	}
		

	//Setting difficulty grade up and loading a new Scene

	public void levelUp(){
		//increase level
		PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") + 1);

		//Increase world
		if (PlayerPrefs.GetInt("level") == levelMax + 1) {
			PlayerPrefs.SetInt ("world", PlayerPrefs.GetInt ("world") + 1); 
			PlayerPrefs.SetInt("level", 1);
			worldUp = true;

		}

		//increase color, field
		//PlayerPrefs.SetInt ("gameFieldWidth", PlayerPrefs.GetInt("gameFieldWidth") + 1);
		//PlayerPrefs.SetInt ("gameFieldHeight", PlayerPrefs.GetInt("gameFieldHeight") + 1);
		SceneManager.LoadScene ("GameScene");
			


	}

	//Decrease field to initial value
	public void levelReset(){

		//PlayerPrefs.SetInt ("gameFieldWidth", initialWidth);
		//PlayerPrefs.SetInt ("gameFieldHeight", initialHeight);
	}
		

}
