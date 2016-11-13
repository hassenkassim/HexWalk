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

	public static Button soundOnBtn;
	public static Button soundOffBtn;
	public static Button restartBtn;
	public static Button mainMenuBtn;
	public static Button shareBtn;
	public static Button nextLevelBtn;
	public static Button pauseBtn;
	public static Text wonText;
	public static Text gameoverText;
	public static Canvas gameOverOrWonCanvas;


	// Use this for initialization
	void Start () {

		//Setup Music
		if (PlayerPrefs.GetInt ("soundIsOn") == 0) {
			AudioListener.pause = false;
		} else {
			AudioListener.pause = false;
		}
			
		loadGameObjects ();

	}

	// Update is called once per frame
	void Update () {

	}

	public void loadGameObjects(){

		//Initialize Button
		//soundOnBtn = GameObject.Find("SoundOnButton").GetComponent<Button>();
		//soundOffBtn = GameObject.Find("SoundOffButton").GetComponent<Button>();
		shareBtn = GameObject.Find ("ShareButton").GetComponent<Button>();
		restartBtn = GameObject.Find("RestartButton").GetComponent<Button>();
		mainMenuBtn = GameObject.Find("MainMenuButton").GetComponent<Button>();	
		nextLevelBtn = GameObject.Find ("NextLevelButton").GetComponent<Button>();

		wonText = GameObject.Find ("WonText").GetComponent<Text>();
		gameoverText = GameObject.Find ("GameoverText").GetComponent<Text>();

		gameOverOrWonCanvas = GameObject.Find("GameOverOrWonCanvas").GetComponent<Canvas>();

		gameOverOrWonCanvas.gameObject.SetActive (false);


	}

	public static void displayGameover(){

		InputManager.active = false;

		gameOverOrWonCanvas.gameObject.SetActive (true);

		restartBtn.gameObject.SetActive (true);
		nextLevelBtn.gameObject.SetActive (false);	

		wonText.gameObject.SetActive (false);
		gameoverText.gameObject.SetActive (true);


	}

	public static void displayWon(){

		InputManager.active = false;

		gameOverOrWonCanvas.gameObject.SetActive (true);

		nextLevelBtn.gameObject.SetActive (true);
		restartBtn.gameObject.SetActive (false);

		gameoverText.gameObject.SetActive (false);
		wonText.gameObject.SetActive (true);
	}


	// Button setup
	public void onNextLevel(){
		LevelPlay.startNextLevel ();
	}


	public void onRestart(){
		SceneManager.LoadScene ("GameScene");
	}

	public void onMainMenu(){
		SceneManager.LoadScene ("LevelScene");
	}

	public void onShare(){
		Share.IntentShareText ("This game is awesome! Get the game from play store: balblaLink!");
	}
}
