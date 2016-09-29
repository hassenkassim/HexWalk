/************************************************
 * HAMOTO Production 2016						*
 * Project: HexWalk								*
 * Authors: Tolga, Mohamed, Dursun, Hassen		*
 * Year: 2016									*
 *************************************************/

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {

	// Buttons
	public GameObject soundOnBtn;
	public GameObject soundOffBtn;
	public GameObject continueBtn;
	public GameObject restartBtn;
	public GameObject mainMenuBtn;
	public GameObject exitBtn;

	public Canvas pauseCanvas;


	// Use this for initialization
	void Awake () {
		//Initialize Button
		soundOnBtn = GameObject.Find("SoundOnButton");
		soundOffBtn = GameObject.Find("SoundOffButton");
		continueBtn = GameObject.Find("ContinueButton");
		restartBtn = GameObject.Find("RestartButton");
		mainMenuBtn = GameObject.Find("MainMenuButton");
		exitBtn = GameObject.Find("QuitButton");


		//Setup Button
		GameObject tempObject = GameObject.Find("PauseCanvas");
		if(tempObject != null){
			//If we found the object , get the Canvas component from it.
			pauseCanvas = tempObject.GetComponent<Canvas>();
			if(pauseCanvas == null){
				Debug.Log("Could not locate Canvas component on " + tempObject.name);
			}
		}
		pauseCanvas.enabled = false;

		//Setup Music
		if (GetBool ("soundIsOn") == false) {
			AudioListener.pause = true;
		} else {
			AudioListener.pause = false;
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}


	// Button setup
	public void onPause(){
		Time.timeScale = 0;

		pauseCanvas.enabled = true;

		if (GetBool ("soundIsOn") == true) {
			soundOffBtn.gameObject.SetActive (false);
			AudioListener.pause = true;
		} else {
			soundOnBtn.gameObject.SetActive (false);
		}

		pauseCanvas.enabled = true;
		Gameplay.pauseBtn.SetActive (false);
	}

	public void onContinue(){
		Time.timeScale = 1;

		if (GetBool ("soundIsOn") == true) {
			AudioListener.pause = false;
		}

		pauseCanvas.enabled = false;
		Gameplay.pauseBtn.SetActive (true);
	}


	public void onRestart(){
		Time.timeScale = 1;
		LevelManager.levelReset ();
		SceneManager.LoadScene ("GameScene");
	}

	public void onMainMenu(){
		Time.timeScale = 1;
		LevelManager.levelReset ();
		SceneManager.LoadScene ("MenuScene");
	}

	public void onSoundOn(){

		soundOnBtn.gameObject.SetActive (false);
		soundOffBtn.gameObject.SetActive (true);

		SetBool("soundIsOn", false);
		PlayerPrefs.Save();
	}

	public void onSoundOff(){
		soundOnBtn.gameObject.SetActive (true);
		soundOffBtn.gameObject.SetActive (false);

		SetBool ("soundIsOn", true);
		PlayerPrefs.Save();
	}

	public void onExit(){
		Application.Quit();
	}


	// Method for boolean PlayerPrefs
	public static void SetBool(string name, bool booleanValue) 
	{
		PlayerPrefs.SetInt(name, booleanValue ? 1 : 0);
	}

	public static bool GetBool(string name)  
	{
		return PlayerPrefs.GetInt(name) == 1 ? true : false;
	}

	public static bool GetBool(string name, bool defaultValue)
	{
		if (PlayerPrefs.HasKey (name)) {
			return GetBool (name);
		}

		return defaultValue;
	}

}
