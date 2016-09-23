using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;


/*
 * This class starts and keeps track of the Game
 * */
public class Gameplay : MonoBehaviour {
	
		/*
		 * Ablauf Spiel:
		 * 1. Felder kurz aus Vogelperspektive anzeigen
		 * 2. Pfad aufblinken lassen
		 * 3. Kamera herunterschwingen und Pfad verstecken
		 * 4. Spieler kann Player kontrollieren
		 * */



	public static Player player;
	public static Camera cam;
	public static Gamefield gamefield;
	public static Pathfinder pathfinder;

	// Buttons
	public GameObject pauseBtn;
	public GameObject soundOnBtn;
	public GameObject soundOffBtn;
	public GameObject continueBtn;
	public GameObject restartBtn;
	public GameObject mainMenuBtn;
	public GameObject exitBtn;

	// Use this for initialization
	void Start () {

		//Create Player
		player = new Player();
		player.setColor (Color.cyan);

		//Create Gamefield
		gamefield = new Gamefield (5, 7);

		//Setup Camera
		cam = Camera.main;
		cam.gameObject.AddComponent <CameraPosition>();

		//Setup Button
		soundOffBtn.gameObject.SetActive (false);
		soundOnBtn.gameObject.SetActive (false);
		continueBtn.gameObject.SetActive (false);
		restartBtn.gameObject.SetActive (false);
		mainMenuBtn.gameObject.SetActive (false);
		exitBtn.gameObject.SetActive (false);

		if (GetBool ("soundIsOn") == false) {
			AudioListener.pause = true;
		} else {
			AudioListener.pause = false;
		}

		// Call Pathfinder constructor
		pathfinder = new Pathfinder ();
	}


	void Update () {
		
	}

	// Button functions
	public void onPause(){

		Time.timeScale = 0;

		if (GetBool ("soundIsOn") == false) {
			soundOffBtn.gameObject.SetActive (true);
		} else {
			soundOnBtn.gameObject.SetActive (true);
			AudioListener.pause = true;
		}
			
		continueBtn.gameObject.SetActive (true);
		restartBtn.gameObject.SetActive (true);
		mainMenuBtn.gameObject.SetActive (true);
		exitBtn.gameObject.SetActive (true);
		pauseBtn.gameObject.SetActive (false);

	}

	public void onContinue(){

		Time.timeScale = 1;

		if (GetBool ("soundIsOn") == true) {
			AudioListener.pause = false;
		}

		soundOffBtn.gameObject.SetActive (false);
		soundOnBtn.gameObject.SetActive (false);
		continueBtn.gameObject.SetActive (false);
		restartBtn.gameObject.SetActive (false);
		mainMenuBtn.gameObject.SetActive (false);
		exitBtn.gameObject.SetActive (false);
		pauseBtn.gameObject.SetActive (true);
	}

	public void onRestart(){
		Time.timeScale = 1;
		SceneManager.LoadScene ("GameScene");
	}

	public void onMainMenu(){

		Time.timeScale = 1;
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
		if(PlayerPrefs.HasKey(name)) 
		{
			return GetBool(name);
		}

		return defaultValue;
	}
}
