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
	public static GameObject Eventsystem;
	public static GameObject pauseBtn;

	public static int gamestate;

	// Use this for initialization
	void Start () {
		gamestate = 0;

		print("FieldWidth: " + PlayerPrefs.GetInt("gameFieldWidth"));
		print("FieldHeight: " + PlayerPrefs.GetInt("gameFieldHeight"));
		Eventsystem = GameObject.Find("EventSystem");

		//Create Player
		player = new Player();

		//Create Gamefield

		gamefield = new Gamefield (PlayerPrefs.GetInt("gameFieldWidth"), PlayerPrefs.GetInt("gameFieldHeight"));
		//gamefield = new Gamefield (4, 5);

		// Call Pathfinder constructor
		pathfinder = new Pathfinder (2);

		//Setup Camera
		cam = Camera.main;
		cam.gameObject.AddComponent <CameraPosition>();



		//Setup Button
		pauseBtn = GameObject.Find("PauseButton");

		Time.timeScale = 1;

	}


	void Update () {
		
	}



}