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
	public static PrefabsManager prefabsMgr;
	public static GameObject pauseBtn;

	public static Level currentLevel;

	public static float totalTime = 0f;



	int version;

	public static int colorCount; // How many Colors should be in the game

	// Use this for initialization
	void Start () {

		version = 1;
		//Call Score Manager constructor
		scoreMgr = new ScoreManager();

		//Call Sound Manager constructor
		soundMgr = new SoundManager();

		//Call Prefab Manager constructor
		prefabsMgr = (PrefabsManager)GameObject.Find("System").GetComponent <PrefabsManager>();

		//Get Level Properties
		currentLevel = LevelManager.getCurrentLevel ();

		//Create Player
		player = new Player(currentLevel.getColorCount(), version);

		//Create Player Info
		playerInfo = new PlayerInfo();

		//Create SavePlayerPrefs
		savePlayerPrefs= new SavePlayerPrefs();

		//Create Gamefield
		//gamefield = new Gamefield (PlayerPrefs.GetInt("gameFieldWidth"), PlayerPrefs.GetInt("gameFieldHeight"), version);
		print("Class: GamePlay; Function: Start: CurrentLevel: Width: " + currentLevel.getWidth() + "; Height: " + currentLevel.getHeight());
		gamefield = new Gamefield (currentLevel.getWidth(), currentLevel.getHeight(), currentLevel.getColorCount());

		// Call Pathfinder constructor
		pathfinder = new Pathfinder (currentLevel.getColorCount());

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
		Field field = gamefield.getField ((int)platePos.x, (int)platePos.y);
		int pointer = pathfinder.pointer;
		if (field.getColor ().Equals (Color.green))
			return;
		if (platePos.x == pathfinder.end.x && platePos.y == pathfinder.end.y) {
			pathfinder.pointer = -1;
			print ("Level Completed!");
			GamesceneManager.displayWon ();
				
			//setup and save level, coloring, stars
			LevelManager.levelUp ();

			//save Level completed
			setLevelToCompleted();

		} else {
			
			if (pathfinder.path [pointer].Equals (platePos) && player.getColor ().Equals (pathfinder.pathcolor [pointer])) {
				pathfinder.pointer++;
				field.setColor (player.getColor ());

				//play the RotationSound
				soundMgr.playRotationSound ("GameScene");
			} else {
				cam.GetComponent<CameraPosition> ().setToFollowPlayerByRotation ();
				field.setColor (Color.red);
				field.activateRigidbody ();
			
			
				field.fractureCube (0.5f, field);

				//Destroy (field.getGameobject ());

				print ("GAMEOVER!");

				GamesceneManager.displayGameover ();
			}
		}

	}

	private static void setLevelToCompleted(){
		LevelManager.getCurrentLevel().setCompleted();
		print("Class: Gameplay.cd Function: setLevelToCompleted");
		Level curlevel = LevelManager.getCurrentLevel ();
		print("World: " + curlevel.getWorld() + "; Level: " + curlevel.getLevel()  + "; COMPLETED: " + curlevel.getCompleted());
	}

		


}