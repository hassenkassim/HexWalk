/************************************************
 * HAMOTO Production 2016						*
 * Project: HexWalk								*
 * Authors: Tolga, Mohamed, Dursun, Hassen		*
 * Year: 2016									*
 *************************************************/

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
	public static GameObject pauseBtn;

	public static int gamestate; //In which state ist the Game, Important to block input until the game starts

	public static int colorCount; // How many Colors should be in the game

	// Use this for initialization
	void Start () {
		gamestate = 0; //set the initial game state to 0: No Input allowed in this state
		colorCount = 2;//set the count of colors in the game

		print("FieldWidth: " + PlayerPrefs.GetInt("gameFieldWidth"));
		print("FieldHeight: " + PlayerPrefs.GetInt("gameFieldHeight"));

		//Create Player
		player = new Player(colorCount);

		//Create Gamefield

		gamefield = new Gamefield (PlayerPrefs.GetInt("gameFieldWidth"), PlayerPrefs.GetInt("gameFieldHeight"));
		//gamefield = new Gamefield (4, 5);

		// Call Pathfinder constructor
		pathfinder = new Pathfinder (colorCount);

		//Setup Camera
		cam = Camera.main;
		cam.gameObject.AddComponent <CameraPosition>();



		//Setup Button
		pauseBtn = GameObject.Find("PauseButton");

	}


	void Update () {
		
	}



}