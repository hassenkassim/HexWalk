using UnityEngine;
using System.Collections;


/*
 * This class starts and keeps track of the Game
 * */
public class Gameplay : MonoBehaviour {

	public static Player player;
	public static Camera cam;
	public static Gamefield gamefield;

	public GameObject PauseBtn;
	public GameObject SoundOnBtn;
	public GameObject SoundOffBtn;
	public GameObject ContinueBtn;
	public GameObject RestartBtn;
	public GameObject MainMenuBtn;
	public GameObject ExitBtn;


	// Use this for initialization
	void Start () {

		//Create Player
		player = new Player();
		player.setColor (Color.cyan);

		//Create Gamefield
		gamefield = new Gamefield (5, 30);
		gamefield.getField (1, 1).setColor (Color.cyan);

		//Setup Camera
		cam = Camera.main;
		cam.gameObject.AddComponent <CameraPosition>();

		SoundOffBtn.gameObject.SetActive (false);
		SoundOnBtn.gameObject.SetActive (false);
		ContinueBtn.gameObject.SetActive (false);
		RestartBtn.gameObject.SetActive (false);
		MainMenuBtn.gameObject.SetActive (false);
		ExitBtn.gameObject.SetActive (false);

		if (GetBool ("soundIsOn") == false) {
			AudioListener.pause = true;
		} else {
			AudioListener.pause = false;
		}
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void onPause(){

		Time.timeScale = 0;

		if (GetBool ("soundIsOn") == false) {
			SoundOffBtn.gameObject.SetActive (true);
		} else {
			SoundOnBtn.gameObject.SetActive (true);
			AudioListener.pause = true;
		}
			
		ContinueBtn.gameObject.SetActive (true);
		RestartBtn.gameObject.SetActive (true);
		MainMenuBtn.gameObject.SetActive (true);
		ExitBtn.gameObject.SetActive (true);
		PauseBtn.gameObject.SetActive (false);

	}

	public void onContinue(){

		Time.timeScale = 1;

		if (GetBool ("soundIsOn") == true) {
			AudioListener.pause = false;
		}

		SoundOffBtn.gameObject.SetActive (false);
		SoundOnBtn.gameObject.SetActive (false);
		ContinueBtn.gameObject.SetActive (false);
		RestartBtn.gameObject.SetActive (true);
		MainMenuBtn.gameObject.SetActive (false);
		ExitBtn.gameObject.SetActive (false);
		PauseBtn.gameObject.SetActive (true);
	}

	public void onRestart(){
		Time.timeScale = 1;
		Application.LoadLevel ("GameScene");
	}

	public void onMainMenu(){

		Time.timeScale = 1;
		Application.LoadLevel ("MenuScene");
	}

	public void onSoundOn(){

		SoundOnBtn.gameObject.SetActive (false);
		SoundOffBtn.gameObject.SetActive (true);

		SetBool("soundIsOn", false);
		PlayerPrefs.Save();

		//AudioListener.pause = true;
	}

	public void onSoundOff(){
		SoundOnBtn.gameObject.SetActive (true);
		SoundOffBtn.gameObject.SetActive (false);

		SetBool ("soundIsOn", true);
		PlayerPrefs.Save();

		//AudioListener.pause = false;
	}

	public void onExit(){
		Application.Quit();
	}


	// BOOLEAN FOR PLAYERPREFS
	// BOOLEAN FOR PLAYERPREFS
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
