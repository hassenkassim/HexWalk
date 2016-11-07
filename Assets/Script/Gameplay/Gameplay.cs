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

	//dursun
	public static AutoFade fading;
	public GameObject tmpOverlay;
	public static bool changeWorld;
	public static bool explode;
	private float radius= 5.0f;
	private float force= 0.01f;

	private static float starDistanceSqr;
	private static float starClipDistanceSqr;
	private static Transform tx;
	private static ParticleSystem.Particle[] points;
	public static int starsMax = 1000;
	public static float starSize = 0.2f;
	public static float starDistance = 10;
	public static float starClipDistance = 1;

	int version;

	public static int colorCount; // How many Colors should be in the game

	// Use this for initialization
	void Start () {

		InputManager.active = false;

		version = 1;

		//Call Score Manager constructor
		scoreMgr = new ScoreManager();

		//Call Prefab Manager constructor
		prefabsMgr = (PrefabsManager)GameObject.Find("System").GetComponent <PrefabsManager>();

		//Get Level Properties
		currentLevel = LevelPlay.levelmgr.getCurrentLevel ();

		GameObject.Find("WorldText").GetComponent<Text>().text = "World: " + (currentLevel.getWorld() + 1 );
		GameObject.Find("LevelText").GetComponent<Text>().text = "Level: " + (currentLevel.getLevel() + 1);

		//Create Player
		player = new Player(currentLevel.getColorCount(), version);

		//Create SavePlayerPrefs
		savePlayerPrefs= new SavePlayerPrefs();

		//Create Gamefield
		gamefield = new Gamefield (currentLevel.getWidth(), currentLevel.getHeight(), currentLevel.getColorCount());

		// Call Pathfinder constructor
		pathfinder = new Pathfinder (currentLevel.getColorCount());

		//Setup Camera
		cam = Camera.main;
		cam.gameObject.AddComponent <CameraPosition>();

		fading = new AutoFade (tmpOverlay);

		//dursun
		BackgroundManager.loadSkybox(cam);

		//tolga
		//Load Musics
		SoundManager.playLevelMusic(1);
		explode = false;

		//Setup Button
		pauseBtn = GameObject.Find("PauseButton");
	
	}

	void Update(){
		if (explode==true) {
			Vector3 tmp = new Vector3 (player.getPosition ().x,player.getPosition ().y,player.getPosition ().z);
			foreach (Collider col in Physics.OverlapSphere(transform.position,radius)) {
				Rigidbody rb = col.GetComponent<Rigidbody>();
				tmp.y = tmp.y - 0.03f;
				if (rb != null) {
					rb.AddExplosionForce (force, tmp, radius);
				}
			}
			explode = false;
		}
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
			LevelPlay.levelmgr.levelUp ();

			//save Level completed
			setLevelToCompleted();

		} else {
			
			if (pathfinder.path [pointer].Equals (platePos) && player.getColor ().Equals (pathfinder.pathcolor [pointer])) {
				pathfinder.pointer++;
				field.setColor (player.getColor ());

				//play the RotationSound
				SoundManager.playRotationSound ("GameScene");
			} else {
				cam.GetComponent<CameraPosition> ().setToFollowPlayerByRotation ();
				field.setColor (Color.red);
				field.activateRigidbody ();

				SoundManager.playGameoverMusic ();

				explode = true;
				field.fractureCube (0.125f, field);
				print ("GAMEOVER!");

				GamesceneManager.displayGameover ();


			}
		}
	}

	private static void setLevelToCompleted(){
		LevelPlay.levelmgr.getCurrentLevel().setCompleted();
	}

		


}