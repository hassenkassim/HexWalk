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

	public static Sprite starGold;
	public static Sprite starGrey;
	public static Button soundOnBtn;
	public static Button soundOffBtn;
	public static Button restartBtn;
	public static Button mainMenuBtn;
	public static Button shareBtn;
	public static Button nextLevelBtn;
	public static Button firstStar;
	public static Button secondStar;
	public static Button thirdStar;
	public static Text wonText;
	public static Text gameoverText;
	public static Canvas gameOverOrWonCanvas;


	// Use this for initialization
	void Start () {

		//Setup Music
		if (PlayerPrefs.GetInt ("soundIsOn") == 0) {
			AudioListener.pause = true;
		} else {
			AudioListener.pause = false;
		}

		loadGameObjects ();

	}

	// Update is called once per frame
	void Update () {

	}

	public void loadGameObjects(){

		starGold = Resources.Load<Sprite> ("stern_gold2");
		starGrey = Resources.Load<Sprite> ("stern_leer2");

		//Initialize Button
		//soundOnBtn = GameObject.Find("SoundOnButton").GetComponent<Button>();
		//soundOffBtn = GameObject.Find("SoundOffButton").GetComponent<Button>();
		shareBtn = GameObject.Find ("ShareButton").GetComponent<Button>();
		restartBtn = GameObject.Find("RestartButton").GetComponent<Button>();
		mainMenuBtn = GameObject.Find("MainMenuButton").GetComponent<Button>();	
		nextLevelBtn = GameObject.Find ("NextLevelButton").GetComponent<Button>();
		firstStar = GameObject.Find("FirstStar").GetComponent<Button>();
		secondStar = GameObject.Find("SecondStar").GetComponent<Button>();
		thirdStar = GameObject.Find("ThirdStar").GetComponent<Button>();

		wonText = GameObject.Find ("WonText").GetComponent<Text>();
		gameoverText = GameObject.Find ("GameoverText").GetComponent<Text>();

		gameOverOrWonCanvas = GameObject.Find("GameOverOrWonCanvas").GetComponent<Canvas>();

		disableStars ();

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

		enableStars ();

	}


	// Button setup
	public void onNextLevel(){
		//Time.timeScale = 1;
		SceneManager.LoadScene ("GameScene");
	}


	public void onRestart(){
		//Time.timeScale = 1;
		SceneManager.LoadScene ("GameScene");
	}

	public void onMainMenu(){
		//Time.timeScale = 1;
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
		

	public static void disableStars(){
		firstStar.gameObject.SetActive (false);
		secondStar.gameObject.SetActive (false);
		thirdStar.gameObject.SetActive (false);
	}

	public static void enableStars(){
		setStars ();
		firstStar.gameObject.SetActive (true);
		secondStar.gameObject.SetActive (true);
		thirdStar.gameObject.SetActive (true);
	}

	public static void setStars(){

		if (Gameplay.star == 3) {
			firstStar.image.sprite = starGold;
			secondStar.image.sprite = starGold;
			thirdStar.image.sprite = starGold;

			enableStars ();

		} else if (Gameplay.star == 2) {
			firstStar.image.sprite = starGold;
			secondStar.image.sprite = starGold;
			thirdStar.image.sprite = starGrey;

			enableStars ();

		} else if (Gameplay.star == 1) {
			firstStar.image.sprite = starGold;
			secondStar.image.sprite = starGrey;
			thirdStar.image.sprite = starGrey;

			enableStars ();

		} else if (Gameplay.star == 0) {
			disableStars ();
		}
	}
}
