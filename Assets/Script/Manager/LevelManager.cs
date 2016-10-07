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

	public Button [,] levelBtn; 
	public Button [,] levelLockedBtn;
	public Button [] worldBtn; 
	public Button [] worldLockedBtn; 
	public int worldMax;
	public int levelMax;
	public bool changeWorld;
	public int level;
	public int world;

	public Button level1;
	public Button level2;
	public Button level3;

	public Button levelLocked1;
	public Button levelLocked2;
	public Button levelLocked3;

	public Button world1;
	public Button world2;
	public Button world3;

	public Button worldLocked1;
	public Button worldLocked2;
	public Button worldLocked3;


	// Use this for initialization
	public void Awake(){ 


		changeWorld = false;
		levelMax = 3;
		worldMax = 3;
		levelBtn = new Button[worldMax, levelMax];
		levelLockedBtn = new Button[worldMax, levelMax];
		worldBtn = new Button[worldMax];
		worldLockedBtn = new Button[worldMax];

		if (PlayerPrefs.HasKey ("world") == false) {
			PlayerPrefs.SetInt ("world", 1);
		}

		if (PlayerPrefs.HasKey ("level") == false) {
			PlayerPrefs.SetInt ("level", 1);
		}
		level = PlayerPrefs.GetInt ("level");

		world = PlayerPrefs.GetInt ("world");

		//initialization the field with unlocked or locked Buttons
		//unlocked Buttons
		//levelBtn[world - 1, level - 1] = GameObject.Find("level "+level.ToString()).GetComponent;
		//levelBtn[world - 1, level - 1].gameObject.SetActive(true);
		//worldBtn[world - 1] = GameObject.Find("world "+world.ToString()).GetComponent;
		//worldBtn[world - 1].gameObject.SetActive(true);

		//locked Buttons
		//levelLockedBtn[world - 1, level - 1] = GameObject.Find("levelLocked "+level.ToString()).GetComponent;
		//levelLockedBtn[world - 1, level - 1].gameObject.SetActive(false);
		//worldLockedBtn[world - 1] = GameObject.Find("worldLocked "+world.ToString()).GetComponent;
		//worldLockedBtn[world - 1].gameObject.SetActive(false);


		for(int i = world; i > worldMax - 1; i++){
			for (int j = level; j > levelMax - 1; j++) {
				//locked Buttons
				//levelLockedBtn[i, j] = GameObject.Find("levelLocked "+(j+1).ToString()).GetComponent;
				//levelLockedBtn[i, j].gameObject.SetActive (true);
				//worldLockedBtn [i] = GameObject.Find("worldLocked "+(i+1).ToString()).GetComponent;
				//worldLockedBtn[i].gameObject.SetActive (true);
				//unlocked Buttons
				//levelLockedBtn[i, j] = GameObject.Find("level "+(j+1).ToString()).GetComponent;
				//levelLockedBtn[i, j].gameObject.SetActive (false);
				//worldLockedBtn [i] = GameObject.Find("world "+(i+1).ToString()).GetComponent;
				//worldLockedBtn[i].gameObject.SetActive (false);
			} 
		}
	}

	public LevelManager () {
		//Initialize LevelCounter
		levelCounter=0;

		//Create Gamefield
		initialWidth = 4;
		initialHeight = 5;

		cubeType = 0;
		playerType = 0;


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

		//increase field
		//PlayerPrefs.SetInt ("gameFieldWidth", PlayerPrefs.GetInt("gameFieldWidth") + 1);
		//PlayerPrefs.SetInt ("gameFieldHeight", PlayerPrefs.GetInt("gameFieldHeight") + 1);
		SceneManager.LoadScene ("GameScene");

		//increase level
		PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") + 1);

		if (level - 1 == levelMax) {
			PlayerPrefs.SetInt ("world", PlayerPrefs.GetInt ("world") + 1); 
			PlayerPrefs.SetInt("level", 1);
		}
			


	}

	//Decrease field to initial value
	public void levelReset(){

		//PlayerPrefs.SetInt ("gameFieldWidth", initialWidth);
		//PlayerPrefs.SetInt ("gameFieldHeight", initialHeight);
	}
		

}
