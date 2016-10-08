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

	public static PlayerInfo playerInfo;
	public static SavePlayerPrefs savePlayerPrefs;

	public static Camera cam;
	public static Gamefield gamefield;
	public static Pathfinder pathfinder;
	public static ScoreManager scoreMgr;
	public static SoundManager soundMgr;
	public static LevelManager levelMgr;
	public static PrefabsManager prefabsMgr;
	public static GameObject pauseBtn;

	int version;

	static AudioClip rotationSound;
	int score;
	Text scoreText;

	public static int colorCount; // How many Colors should be in the game

	// Use this for initialization
	void Start () {

		version = 1;
		//Call Score Manager constructor
		scoreMgr = new ScoreManager();

		//Call Sound Manager constructor
		soundMgr = new SoundManager();

		//Call Level Manager constructor
		levelMgr = new LevelManager();

		//Call Prefab Manager constructor
		prefabsMgr = (PrefabsManager)GameObject.Find("System").GetComponent <PrefabsManager>();



		colorCount = 2;//set the count of colors in the game

		print("FieldWidth: " + PlayerPrefs.GetInt("gameFieldWidth"));
		print("FieldHeight: " + PlayerPrefs.GetInt("gameFieldHeight"));

		//Create Player
		player = new Player(colorCount, version);

		//Create Player Info
		playerInfo = new PlayerInfo();


		//Create Gamefield
		//gamefield = new Gamefield (PlayerPrefs.GetInt("gameFieldWidth"), PlayerPrefs.GetInt("gameFieldHeight"), version);
		gamefield = new Gamefield (3, 3, 1);

		// Call Pathfinder constructor
		pathfinder = new Pathfinder (colorCount);



		//Setup Camera
		cam = Camera.main;
		cam.gameObject.AddComponent <CameraPosition>();



		//Setup Button
		pauseBtn = GameObject.Find("PauseButton");




	}

	public static void collision(){
		//Alles was ich brauche bekomme ich durch unsere Gameplay Klasse. Egal was ich brauche, ich gehe zuerst ins Gameplay rein, dann entweder Gamefield, 
		//oder Player oder Path, je nachdem was ich brauche und dann rufe ich den jeweiligen Getter auf!
		Vector2 platePos = player.getGamePosition ();//dazu gehe ich in unser Gameplay->Player->getGamePosition
		Field field = gamefield.getField((int)platePos.x, (int) platePos.y);
		int pointer = pathfinder.pointer;
		if(field.getColor().Equals(Color.green)) return;
		if (field.getColor ().Equals (Color.blue)) {
			pathfinder.pointer = - 1;
			print ("WON!");

			GameSceneButtonManager.displayWon();
			LevelManager.levelUp ();
			//count stars and assign it
			//playerInfo.countStarsPerLevel(levelMgr.levelCounter); //TODO: level number

			//load next Level


		}

		if (pathfinder.path[pointer].Equals(platePos) && player.getColor().Equals(pathfinder.pathcolor[pointer])) {
			pathfinder.pointer++;
			field.setColor (player.getColor());
			//increment Score
			scoreMgr.incScore ();
			//play the RotationSound
			soundMgr.playRotationSound ();
		} else {
			cam.GetComponent<CameraPosition> ().setToFollowPlayerByRotation ();
			field.setColor (Color.red);
			field.activateRigidbody ();
			
			
			field.fractureCube (0.5f, field);

			Destroy (field.getGameobject ());

			print ("GAMEOVER!");

			GameSceneButtonManager.displayGameover ();
		}
	}
		

	void Update () {
		
	}
		


}