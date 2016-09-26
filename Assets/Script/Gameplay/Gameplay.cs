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
	public GameObject gameoverBtn;

	// Use this for initialization
	void Start () {

		Eventsystem = GameObject.Find("EventSystem");

		//Create Player
		player = new Player();
		player.setColor (Color.cyan);

		//Create Gamefield
		gamefield = new Gamefield (5, 10);



		// Call Pathfinder constructor
		pathfinder = new Pathfinder ();

		//Setup Camera
		cam = Camera.main;
		cam.gameObject.AddComponent <CameraPosition>();

		//Setup Button
		pauseBtn = GameObject.Find("PauseButton");
		gameoverBtn = GameObject.Find ("GameOverButton");

	}


	void Update () {
		
	}



}
