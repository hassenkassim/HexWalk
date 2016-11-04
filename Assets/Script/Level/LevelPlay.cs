using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelPlay : MonoBehaviour {

	public const int FIELD_WORLDBLOCKED = 0;
	public const int FIELD_SELECTED = 1;
	public const int FIELD_COMPLETED = 2;
	public const int FIELD_BLOCKED = 3;
	public const int FIELD_WORLDUNBLOCKED = 4;

	public static bool playFromCurLevel;
	public static bool firstTouchWithPlate;
	public static bool settingsCanvasActive;
	public static bool shoppingCanvasActive;

	public static int level;
	public static int world;
	public static int height;
	public static int soundIsOn;
	public static int curWorld;
	public static int curLevel;

	//Playercontroller
	public float rotationPeriod = 0.25f;		
	public float sideLength = 1f;			

	public bool isRotate = false;					
	public float directionX = 0;					
	public float directionZ = 0;	
	public float rotationTime = 0;					
	public float radius;	

	public static Player player;

	public static PlayerInfo playerInfo;
	public static SavePlayerPrefs savePlayerPrefs;

	public static Text levelText;
	public static Text worldText;
	public static Text statusText;

	public static GameObject playerobj;

	public static Field[,] fields;

	public static Gamefield gamefield;

	public static Button settingsButton;
	public static Button shoppingButton;
	public static Button soundOnButton;
	public static Button soundOffButton;
	public static Button infoButton;
	public static Button playButton;

	public static Camera cam;

	public static Vector2 gamePosition;
	public static Vector2 oldPosition;

	public static Canvas settingsCanvas;
	public static Canvas shoppingCanvas;

	public static SoundManager soundMgr;

	public Vector3 startPos;						
						
	public Quaternion fromRotation;				
	public Quaternion toRotation;

	public static PrefabsManagerLevelPlay prefabsMgr;


	static AudioClip rotationSound;

	// Use this for initialization
	public void Start () {

		InputManager.active = true;

		//Call Sound Manager constructor
		//soundMgr = new SoundManager();

		//Call Prefab Manager constructor
		prefabsMgr = (PrefabsManagerLevelPlay)GameObject.Find("System").GetComponent <PrefabsManagerLevelPlay>();

		//load GameField
		loadFields ();

		//Load the Player
		loadPlayer();

		playFromCurLevel = false;
		firstTouchWithPlate = true;
		settingsCanvasActive = false;
		shoppingCanvasActive = false;

		//Load Text Objects
		loadGameObjects ();

		//setAudio
		//setAudio();

		radius = sideLength * Mathf.Sqrt (2f) / 2f;

		//Setup Camera
		cam = Camera.main;
		cam.gameObject.AddComponent <CameraPositionLevelPlay>();
	}

	public void Update () {
			

		if (InputManager.getClickTouchInput ()) {
			//start Level
			startLevel ();
		}

			
		float x = InputManager.getHorizontalInput();
		float y = 0;

		if(x==0) y = InputManager.getVerticalInput();



		//check if move is allowed
		//if (checkOutside (x,y)) {
		//	//Debug.Log ("Player out of Gamefield, No Rotation possible!");
		//	return;
		//}



		if ((x != 0 || y != 0) && !isRotate) {
			if (moveAllowed ((int)x, (int)y) == false) {
				return;
			}
			oldPosition = new Vector2 (gamePosition.x, gamePosition.y);
			if (x != 0) {
				gamePosition = new Vector2 (gamePosition.x + x, gamePosition.y);
			} else if (y != 0) {
				gamePosition = new Vector2(gamePosition.x, gamePosition.y+y);
			}

			directionX = -x;															
			directionZ = y;																
			startPos = playerobj.transform.position;												
			fromRotation = playerobj.transform.rotation;											
			playerobj.transform.Rotate (directionZ * 90, 0, directionX * 90, Space.World);		
			toRotation = playerobj.transform.rotation;											
			playerobj.transform.rotation = fromRotation;											
			rotationTime = 0;															
			isRotate = true;

			//play the RotationSound
			//soundMgr.playRotationSound ("LevelScene");
		}
		
	}

	public void FixedUpdate() {

		if (isRotate) {

			rotationTime += Time.fixedDeltaTime;									
			float ratio = Mathf.Lerp(0, 1, rotationTime / rotationPeriod);			

			float thetaRad = Mathf.Lerp(0, Mathf.PI / 2f, ratio);					
			float distanceX = -directionX * radius * (Mathf.Cos (45f * Mathf.Deg2Rad) - Mathf.Cos (45f * Mathf.Deg2Rad + thetaRad));		
			float distanceY = radius * (Mathf.Sin(45f * Mathf.Deg2Rad + thetaRad) - Mathf.Sin (45f * Mathf.Deg2Rad));						
			float distanceZ = directionZ * radius * (Mathf.Cos (45f * Mathf.Deg2Rad) - Mathf.Cos (45f * Mathf.Deg2Rad + thetaRad));			
			playerobj.transform.position = new Vector3(startPos.x + distanceX, startPos.y + distanceY, startPos.z + distanceZ);						

			playerobj.transform.rotation = Quaternion.Lerp(fromRotation, toRotation, ratio);	

			if (ratio == 1) {
				isRotate = false;
				directionX = 0;
				directionZ = 0;
				rotationTime = 0;
			}

		}
	}







	public static void collision(){
		int gameposy = (int)gamePosition.y;
		int gameposx = (int)gamePosition.x;
		print ("x: " + gameposx + "; y: " + gameposy);
		print ("Name: " + fields [gameposx, gameposy].getName ());

		curWorld = (int)gamePosition.y / 2;
		curLevel = (int)gamePosition.x;
		setCurrentFieldColor (Col.SELECTEDCOLOR);

		if (gamePosition.y % 2 != 1) {
			levelText.text = "Level: " + (gamePosition.x + 1);
			worldText.text = "World: " + ((int)(gamePosition.y/2 + 1));

			if (fields [(int)gamePosition.x, (int)gamePosition.y].getColor() == Col.COMPLETEDCOLOR) {
				statusText.text = "Level finished";
			} else if (fields [(int)gamePosition.x, (int)gamePosition.y].getColor() == Col.SELECTEDCOLOR) {
				statusText.text = "Level current";
			} else {
				statusText.text = "Level blocked";
			}
		
			enableText ();
		
		} else {
			disableText ();
		}
	}

	public static void collisionExit(){
		//get old World and Level
		int oldWorld = (int)oldPosition.y / 2;
		int oldLevel = (int)oldPosition.x;

		if (oldPosition.y % 2 != 1) {
			if (LevelManager.levels [oldWorld, oldLevel].getCompleted () == 0) {
				setOldFieldColor (Col.ENABLEDCOLOR);
			} else {
				setOldFieldColor (Col.COMPLETEDCOLOR);
			}
		} else {
			setOldFieldColor (Col.WORLDUNLOCKEDCOLOR);
		}
	}


		
	public static void startLevel(){
		if (gamePosition.y % 2 != 0)
			return;

		//setting currentLevel
		LevelManager.setCurrentLevel (curWorld, curLevel);

		print ("Class: LevelPlay; Function: startLevel");
		print("World: " + curWorld + " Level: " + curLevel);

		SceneManager.LoadScene ("GameScene");
		return;
	}

	public static void startNextLevel(){
		int nextWorld = PlayerPrefs.GetInt (LevelManager.NEXTWORLD, 0);
		int nextLevel = PlayerPrefs.GetInt (LevelManager.NEXTLEVEL, 0);
		LevelManager.setCurrentLevel (nextWorld, nextLevel);
		print ("Class: LevelPlay; Function: startNextLevel");
		print("World: " + nextWorld + " Level: " + nextLevel);

		SceneManager.LoadScene ("GameScene");
		return;
	}





	public static void setCurrentFieldColor(Color col){
		fields [(int)gamePosition.x, (int)gamePosition.y].setColor(col);
	}
	public static void setOldFieldColor(Color col){
		fields [(int)oldPosition.x, (int)oldPosition.y].setColor(col);
	}
		
	public static void setAudio(){

		if (PlayerPrefs.HasKey ("soundIsOn") == false) {
			PlayerPrefs.SetInt ("soundIsOn", 1);
			AudioListener.pause = false;

		} else {
			if (PlayerPrefs.GetInt ("soundIsOn") == 1) {
				AudioListener.pause = false;
			} else {
			}
		}
	}

	private static bool moveAllowed(int x, int y){
		Field field = null;
		try {
			field= fields [(int)gamePosition.x + x, (int)gamePosition.y + y];
			if (field.blocked () == true) {
				return false;
			} else {
				return true;
			}
		} catch(System.Exception e){
			if (e is System.NullReferenceException) {
				print ("Field not available");
			} else if (e is System.IndexOutOfRangeException) {
				print ("Field Index out of Range");
			}
			return false;
		}
	}

	public static bool checkOutside(float x, float y){
		if (x == -1) {
			if (gamePosition.x == 0)
				return true;
		} else if (x == 1) {
			if (gamePosition.x == level - 1 || gamePosition.y%2 == 1)
				return true;
		} else if (y == -1) {
			if (gamePosition.y == 0)
				return true;
			if(PlayerPrefs.GetInt ("Color X:" +gamePosition.x  + " Y:" + ((int)gamePosition.y - 1)) == FIELD_WORLDBLOCKED)
				return true;
		} else if (y == 1) {
			if (gamePosition.y == height - 1)
				return true;
			if(PlayerPrefs.GetInt ("Color X:" +gamePosition.x  + " Y:" + ((int)gamePosition.y + 1)) == FIELD_WORLDBLOCKED)
				return true;

		}
		return false;
	}

	//Load the Gamefield
	public void loadFields(){

		//Create Gamefield
		level = LevelManager.getLevelMax ();
		world = LevelManager.getWorldMax ();


		//für blockierende Steine zwischen den Welten
		height = world * 2 - 1; 

		fields = new Field[level, height];

		print ("Leere Field generiert" + level + " - " + height);


		for (int j = 0; j < height; j++) { 
			//add WorldBlock Field
			if (j % 2 == 1) {
				addField (0, j, 1);
				loadFieldColor (0, j);
				continue;
			}

			for (int i = 0; i < level; i++) {
				addField (i, j, 1);
				loadFieldColor (i, j);
			}
		}
	}

	public void addField(int x, int y, int version){
		fields [x, y] = new Field ("LevelPlay_" + x + "_" + y, version, true);
		fields [x, y].setScale (new Vector3 (0.4f, 0.4f, 0.05f));
		fields [x, y].setPosition(x, y);
		fields [x, y].setTag("LevelField");
	}



	public static void loadFieldColor(int x, int y){
		Field field = fields [x, y];

		if (y % 2 == 1) {
			if(LevelManager.worldCompleted >= (y/2 + 1)) {
				// World Enabled
				field.setColor(Col.WORLDUNLOCKEDCOLOR);
			} else {
				// World Disable
				field.setColor(Col.WORLDBLOCKCOLOR);
			}
		} else {
			if(LevelManager.worldCompleted >= y/2) {
				// Field Enabled
				if (LevelManager.levels [y / 2, x].getCompleted () == 0) {
					field.setColor(Col.ENABLEDCOLOR);
				} else {
					field.setColor(Col.COMPLETEDCOLOR);
				}
			} else {
				// Field Disable
				field.setColor(Col.BLOCKEDCOLOR);
			}
		}
	}

	private static void setColor(GameObject g, Color col){
		g.GetComponent<MeshRenderer> ().material.color = col;
	}

	public void loadPlayer(){
		//Create Player
		playerobj = LevelPlay.prefabsMgr.generateObjectFromPrefab ("cubeEckig");
		playerobj.transform.localScale = new Vector3 (0.3f, 0.3f, 0.3f);
		playerobj.name = "PlayerDynamic";
		playerobj.AddComponent <LevelPlayerController>();

		//Load previous Player Location

		playerobj.transform.position = new Vector3(0, 3, 0);
		//playerobj.transform.position = new Vector3(PlayerPrefs.GetInt("level") - 1, 3, PlayerPrefs.GetInt("world") - 1);
		playerobj.transform.rotation = Quaternion.Euler(0, 0, 0);
		playerobj.tag = "Player";
		gamePosition = new Vector2 (0, 0);
		Rigidbody playerRigidBody = playerobj.AddComponent<Rigidbody>(); // Add the rigidbody
		playerRigidBody.mass = 0.5f;
		playerRigidBody.angularDrag = 0.05f;
		playerRigidBody.useGravity = true;
	}

	public void loadGameObjects(){
		//initialize text
		levelText = GameObject.Find("LevelText").GetComponent<Text>();
		worldText = GameObject.Find("WorldText").GetComponent<Text>();
		statusText = GameObject.Find("StatusText").GetComponent<Text>();
	}


	public static void enableText(){
		levelText.GetComponent<Text> ().enabled = true;
		worldText.GetComponent<Text> ().enabled = true;
		statusText.GetComponent<Text> ().enabled = true;
	}

	public static void disableText(){
		levelText.GetComponent<Text> ().enabled = false;
		worldText.GetComponent<Text> ().enabled = false;
		statusText.GetComponent<Text> ().enabled = false;
	}

	//worlnr starting at 0
	/*public static void enableWorld(int worldnr){
		int y = worldnr * 2 - 1;
		print ("y: " + y);
		if (fields!= null)
			fields[0, y].setColor(Col.WORLDUNLOCKEDCOLOR);
	}*/
}
