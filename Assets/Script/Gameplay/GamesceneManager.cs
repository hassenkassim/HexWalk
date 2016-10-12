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

public class GamesceneManager : MonoBehaviour {

	// Buttons
	public static GameObject soundOnBtn;
	public static GameObject soundOffBtn;
	public static GameObject restartBtn;
	public static GameObject mainMenuBtn;
	public static GameObject shareBtn;
	public static GameObject nextLevelBtn;

	public static Text highscoreText;
	public static Text scoreText;
	public static GameObject wonText;
	public static GameObject gameoverText;
	public static Canvas gameOverOrWonCanvas;
	public static int highscore;



	// Use this for initialization
	void Awake () {
		//Initialize Button
		soundOnBtn = GameObject.Find("SoundOnButton");
		soundOffBtn = GameObject.Find("SoundOffButton");
		shareBtn = GameObject.Find ("ShareButton");
		restartBtn = GameObject.Find("RestartButton");
		mainMenuBtn = GameObject.Find("MainMenuButton");	
		nextLevelBtn = GameObject.Find ("NextLevelButton");

		wonText = GameObject.Find ("WonText");
		gameoverText = GameObject.Find ("GameoverText");

		scoreText = GameObject.Find("Score1Text").GetComponent<Text>();
		highscoreText = GameObject.Find ("HighscoreText").GetComponent<Text> ();
		GameObject tempObject = GameObject.Find("GameOverOrWonCanvas");
		if(tempObject != null){
			//If we found the object , get the Canvas component from it.
			gameOverOrWonCanvas = tempObject.GetComponent<Canvas>();
			if(gameOverOrWonCanvas == null){
				Debug.Log("Could not locate Canvas component on " + tempObject.name);
			}
		}

		gameOverOrWonCanvas.enabled = false;


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

	public static void displayGameover(){

		InputManager.active = false;

		gameOverOrWonCanvas.enabled = true;

		restartBtn.gameObject.SetActive (true);
		nextLevelBtn.gameObject.SetActive (false);	

		wonText.gameObject.SetActive (false);
		gameoverText.SetActive (true);

		if (PlayerPrefs.HasKey ("highscore") == false) {
			PlayerPrefs.SetInt ("highscore", 0);
		}

		if(Gameplay.scoreMgr.getScore() > highscore){
			PlayerPrefs.SetInt ("highscore", Gameplay.scoreMgr.getScore());
			highscore = Gameplay.scoreMgr.getScore();
		}

		highscore = PlayerPrefs.GetInt ("highscore");

		scoreText.text = "Score: " + Gameplay.scoreMgr.getScore().ToString ();
		highscoreText.text = "Highscore: " + highscore.ToString ();

	}

	public static void displayWon(){

		InputManager.active = false;
		gameOverOrWonCanvas.enabled = true;

		nextLevelBtn.gameObject.SetActive (true);
		restartBtn.gameObject.SetActive (false);

		gameoverText.gameObject.SetActive (false);
		wonText.gameObject.SetActive (true);

		if (PlayerPrefs.HasKey ("highscore") == false) {
			PlayerPrefs.SetInt ("highscore", 0);
		}

		if(Gameplay.scoreMgr.getScore() > highscore){
			PlayerPrefs.SetInt ("highscore", Gameplay.scoreMgr.getScore());
			highscore = Gameplay.scoreMgr.getScore();
		}

		highscore = PlayerPrefs.GetInt ("highscore");

		scoreText.text = "Score: " + Gameplay.scoreMgr.getScore().ToString ();
		highscoreText.text = "Highscore: " + highscore.ToString ();
	}


	// Button setup
	public void onNextLevel(){
		Time.timeScale = 1;
		SceneManager.LoadScene ("GameScene");
	}


	public void onRestart(){
		Time.timeScale = 1;
		SceneManager.LoadScene ("GameScene");
	}

	public void onMainMenu(){
		Time.timeScale = 1;
		SceneManager.LoadScene ("LevelScene");
	}

	public void onShare(){
		Share.IntentShareText ("This game is awesome! Get the game from play store: balblaLink!");
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
