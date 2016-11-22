/************************************************
 * HAMOTO Production 2016						*
 * Project: HexWalk								*
 * Authors: Tolga, Mohamed, Dursun, Hassen		*
 * Year: 2016									*
 *************************************************/

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
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

	public static bool first = true; //Indicates wether it is the first Level loaded from LevelScene

	public static SavePlayerPrefs savePlayerPrefs;

	public static Camera cam;
	public static Gamefield gamefield;
	public static Gamefield nextGamefield;
	public static Pathfinder pathfinder;
	public static ScoreManager scoreMgr;
	public static SoundManager soundMgr;
	public static PrefabsManager prefabsMgr;
	private BackgroundManager backgroundmgr;
	public static GameObject pauseBtn;

	public static float totalTime = 0f;

	//dursun
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
	public static Gameplay instance;

	static int offsetY;

	void Awake() {
		first = true;
		instance = this;
	}

	// Use this for initialization
	void Start () {
		PathfinderController.coloringStarted = false;
		PathfinderController.coloringEnd = true;

		offsetY = 0;

		InputManager.active = false;

		//Setup Camera
		cam = Camera.main;
		cam.gameObject.AddComponent <CameraPosition>();

		//dursun
		BackgroundManager.loadSkybox(cam);
		Fade.StartFadeIn (2.0f);

		//Call Score Manager constructor
		scoreMgr = new ScoreManager();

		//Call Prefab Manager constructor
		prefabsMgr = (PrefabsManager)GameObject.Find("System").GetComponent <PrefabsManager>();

		//Get Level Properties
		Level currentLevel = LevelPlay.levelmgr.getCurrentLevel ();

		//Level currentLevel = LevelPlay.levelmgr.getLevel (11, 9);

		//dursun
		BackgroundManager.loadSkybox(cam);
		Fade.StartFadeIn (2.0f);

		GameObject.Find("WorldText").GetComponent<Text>().text = "World: " + (currentLevel.getWorld() + 1);
		GameObject.Find("LevelText").GetComponent<Text>().text = "Level: " + (currentLevel.getLevel() + 1);

		version = currentLevel.getWorld() + 1;

		//Create Player
		player = new Player(currentLevel.getColorCount(), version);

		//Create SavePlayerPrefs
		savePlayerPrefs= new SavePlayerPrefs();

		//Create Gamefield
		gamefield = new Gamefield (currentLevel.getWidth(), currentLevel.getHeight(), currentLevel.getWorld() + 1);

		// Call Pathfinder constructor
		pathfinder = new Pathfinder (currentLevel.getColorCount());

		//tolga
		//Load Musics
		SoundManager.playLevelMusic(currentLevel.getWorld());

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

		if (!Camera.main.GetComponent<Skybox> ().material.name.Equals ("skybox" +LevelPlay.levelmgr.curLevel.getWorld())){
			Fade.FadeAndNewWorld (1.0f, cam);
		}

		int pointer = pathfinder.pointer;
		if (field.getColor ().Equals (Col.GRUEN))
			return;
		if (platePos.x == pathfinder.end.x && platePos.y == pathfinder.end.y) {
			win ();
		} else {
			goNext ();
		}
	}

	public static void win(){
		print ("Won");
		if (first == true) {
			first = false; //Not the first start start
		};

		//save Level completed
		setLevelToCompleted();

		LevelPlay.levelmgr.unlockWorld (LevelPlay.levelmgr.curLevel);
		LevelPlay.levelmgr.nextLevel (LevelPlay.levelmgr.curLevel);

		//load in next level
		offsetY += (int)player.getGamePosition().y + 1;
		loadNextLevelDynamic ();
	}

	private static void goNext(){
		Vector2 platePos = player.getGamePosition ();
		Field field = gamefield.getField ((int)platePos.x, (int)platePos.y);
		int pointer = pathfinder.pointer;

		if (pathfinder.path [pointer].Equals (platePos) && player.getColor ().Equals (pathfinder.pathcolor [pointer])) {
			pathfinder.pointer++;
			field.setColor (player.getColor ());

			//play the RotationSound
			if (PlayerPrefs.GetInt ("SoundOn", 1) == 1) {
				SoundManager.playRotationSound ("GameScene");
			}
		} else {
			lose ();
		}
	}

	private static void lose(){
		if (first == true) {
			first = false; //Not the first start start
		};
		print ("GAMEOVER!");

		Vector2 platePos = player.getGamePosition ();
		Field field = gamefield.getField ((int)platePos.x, (int)platePos.y);

		cam.GetComponent<CameraPosition> ().setToFollowPlayerByRotation ();

		field.getGameobject().SetActive(false);

		if (PlayerPrefs.GetInt ("SoundOn", 1) == 1) {
			SoundManager.stopMusic ();
			SoundManager.playGameoverMusic ();
		}

		explode = true;
		field.fractureCube (0.125f, field);

		InputManager.active = false;

		instance.StartCoroutine (waitForGameover(4.0f));
			
		//GamesceneManager.displayGameover ();

	}

	private static void setLevelToCompleted(){
		LevelPlay.levelmgr.getCurrentLevel().setCompleted();
	}


	private static void loadNextLevelDynamic(){
		InputManager.active = false;

		int x = (int)player.getGamePosition ().x;
		int nextWorld = PlayerPrefs.GetInt (LevelManager.NEXTWORLD, 0);
		int nextLevel = PlayerPrefs.GetInt (LevelManager.NEXTLEVEL, 0);

		LevelPlay.levelmgr.setCurrentLevel (nextWorld, nextLevel);
		Level currentLevel = LevelPlay.levelmgr.getCurrentLevel ();

		GameObject.Find("WorldText").GetComponent<Text>().text = "World: " + (currentLevel.getWorld() + 1);
		GameObject.Find("LevelText").GetComponent<Text>().text = "Level: " + (currentLevel.getLevel() + 1);


		nextGamefield = new Gamefield (currentLevel.getWidth(), currentLevel.getHeight(), currentLevel.getWorld() + 1, offsetY, Gamefield.SLIDEFROMBOTTOM);
		gamefield = nextGamefield;

		// Call Pathfinder constructor
		pathfinder = new Pathfinder (currentLevel.getColorCount(), x);
		player.setGamePosition (new Vector2(x, -1));
		player.setColor (Col.GRUEN);
		player.setColorCount (currentLevel.getColorCount ());
	}

	public static IEnumerator waitForGameover(float timetowait){
	
		yield return new WaitForSeconds (timetowait);
		AdManager.showAd ();
		Fade.FadeAndStartScene ("LevelScene", 2.0f, cam);
	}
		
}