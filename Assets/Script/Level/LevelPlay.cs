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

	public static LevelManager levelmgr;

	public static bool playFromCurLevel;

	public static int level;
	public static int world;
	public static int height;
	public static int soundIsOn;
	//public static int curWorld;
	//public static int curLevel;

	public static Level curLevel;

	//Playercontroller
	public float rotationPeriod = 0.25f;		
	public float sideLength = 1f;			

	public bool isRotate = false;					
	public float directionX = 0;					
	public float directionZ = 0;	
	public float rotationTime = 0;					
	public float radius;	

	public static Player player;

	public static SavePlayerPrefs savePlayerPrefs;

	public static Text levelText;
	public static Text worldText;
	public static Text statusText;

	public static GameObject playerobj;

	public static Field[,] fields;

	public static Gamefield gamefield;

	public static Camera cam;

	public static Vector2 gamePosition;
	public static Vector2 oldPosition;

	public Vector3 startPos;						
						
	public Quaternion fromRotation;				
	public Quaternion toRotation;

	public static PrefabsManagerLevelPlay prefabsMgr;

	public GameObject splash;
	public static GameObject gameName; //dursun

	//Buttons
	public static Button listButton;
	public static Button soundOnButton;
	public static Button shareButton;
	public static Button infoButton;
	public static Vector3 startButtonPos;
	public static Vector3 soundOnEndPosition;
	public static Vector3 shareEndPosition;
	public static Vector3 infoEndPosition;
	public bool listButtonClicked;
	public bool transitionEnd;
	public Sprite soundOnImage;
	public Sprite soundOffImage;

	private static float starDistanceSqr;
	private static float starClipDistanceSqr;
	private static Transform tx;
	private static ParticleSystem.Particle[] points;
	public static int starsMax = 1000;
	public static float starSize = 0.2f;
	public static float starDistance = 10;
	public static float starClipDistance = 1;


	// Use this for initialization
	public void Start () {
		
		//PlayerPrefs.DeleteAll ();

		splash = GameObject.Find ("Splash");

		//Setup Camera
		cam = Camera.main;
		cam.gameObject.AddComponent <CameraPositionLevelPlay> ();

		//Disable Input
		InputManager.active = false;

		//Call Level Manager constructor
		levelmgr = new LevelManager ();

		//Call Prefab Manager constructor
		prefabsMgr = (PrefabsManagerLevelPlay)GameObject.Find ("System").GetComponent <PrefabsManagerLevelPlay> ();

		//load GameField
		loadFields ();

		//Load the Player
		loadPlayer ();

		//Load Text Objects
		loadGameObjects ();


		//Initiate all variables
		init ();

			

		//start music
//		if (splash.GetComponent<Splash> ().getSplashShown () != 0) {
//			SoundManager.playMenuMusic ();
//		} 


		//load buttons
		transitionEnd = true;
		loadButtons ();

		//sound
		if (PlayerPrefs.GetInt ("SoundIsOn", 1) == 1) {
//			soundOnButton.image.sprite = soundOnImage; 
			AudioListener.pause = false;
		} else {
//			soundOnButton.image.sprite = soundOffImage;
			AudioListener.pause = true;
		}
			
	}
		
	private void init(){
		curLevel = levelmgr.curLevel;

		radius = sideLength * Mathf.Sqrt (2f) / 2f;

		//dursun
		BackgroundManager.loadSkybox(cam);

		//tolga
		disableText();


		//dursun
		gameName = SplashLoad.prefabsMgr.generateObjectFromPrefab("gameName");
		gameName.GetComponent<Rigidbody>().useGravity=false;
		gameName.transform.position = new Vector3 (-1.1f,10.0f,playerobj.transform.position.z);
		gameName.AddComponent<Splash> ();
		gameName.SetActive (true);
	}


	public void Update () {
		

		Vector2 input = InputManager.getInput ();

		if (input.x == 0 && input.y == 0) {
			if (InputManager.getClickTouchInput ()) {
				
				print ("STARTLEVEL");
				startLevel ();
			}
		}

		if ((input.x != 0 || input.y != 0) && !isRotate) {
			rotatePlayer (input.x, input.y);
		}
	}

	public void FixedUpdate() {
		rotation ();
	}

	private void rotatePlayer(float x, float y){
		if (moveAllowed ((int)x, (int)y) == false) {
			return;
		}
		oldPosition = new Vector2 (gamePosition.x, gamePosition.y);
		if (x != 0) {
			gamePosition = new Vector2 (gamePosition.x + x, gamePosition.y);
		} else if (y != 0) {
			gamePosition = new Vector2(gamePosition.x, gamePosition.y + y);
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
		SoundManager.playRotationSound ("LevelScene");
	}

	private void rotation(){
		if (isRotate) {

			rotationTime += Time.fixedDeltaTime;									
			float ratio = Mathf.Lerp (0, 1, rotationTime / rotationPeriod);			

			float thetaRad = Mathf.Lerp (0, Mathf.PI / 2f, ratio);					
			float distanceX = -directionX * radius * (Mathf.Cos (45f * Mathf.Deg2Rad) - Mathf.Cos (45f * Mathf.Deg2Rad + thetaRad));		
			float distanceY = radius * (Mathf.Sin (45f * Mathf.Deg2Rad + thetaRad) - Mathf.Sin (45f * Mathf.Deg2Rad));						
			float distanceZ = directionZ * radius * (Mathf.Cos (45f * Mathf.Deg2Rad) - Mathf.Cos (45f * Mathf.Deg2Rad + thetaRad));			
			playerobj.transform.position = new Vector3 (startPos.x + distanceX, startPos.y + distanceY, startPos.z + distanceZ);						

			playerobj.transform.rotation = Quaternion.Lerp (fromRotation, toRotation, ratio);	

			if (ratio == 1) {
				isRotate = false;
				directionX = 0;
				directionZ = 0;
				rotationTime = 0;
			}
		}
	}

	private bool moveAllowed(int x, int y){
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

	public static void collision(){

//		curWorld = (int)gamePosition.y / 2;
//		curLevel = (int)gamePosition.x;

		levelmgr.setCurrentLevel ((int)gamePosition.y / 2, (int)gamePosition.x);

		setCurrentFieldColor (Col.SELECTEDCOLOR);


		if (gamePosition.y % 2 != 1) {
			levelText.text = "Level: " + (gamePosition.x + 1);
			worldText.text = "World: " + ((int)(gamePosition.y/2 + 1));

			if (levelmgr.curLevel.getCompleted() == 1) {
				statusText.text = "Level finished";
			} else if (levelmgr.curLevel.getCompleted() == 0) {
				statusText.text = "Level not finished";
			}
			enableText ();
		} else {
			disableText ();
		}
		if (!Camera.main.GetComponent<Skybox> ().material.name.Equals ("skybox" + levelmgr.curLevel.getWorld())){//(((int)((gamePosition.y) / 2 + 1)) - 1))) {
			Fade.FadeAndNewWorld (1.0f, cam);
		}
	}

	public static void collisionExit(){
		//get old World and Level
		int oldWorld = (int)oldPosition.y / 2;
		int oldLevel = (int)oldPosition.x;

		if (oldPosition.y % 2 != 1) {
			if (levelmgr.levels [oldWorld, oldLevel].getCompleted () == 0) {
				setOldFieldColor (Col.ENABLEDCOLOR);
			} else {
				setOldFieldColor (Col.COMPLETEDCOLOR);
			}
		} else {
			setOldFieldColor (Col.WORLDUNLOCKEDCOLOR);
		}
	}
		
	public void startLevel(){
		if (gamePosition.y % 2 != 0)
			return;
		//setting currentLevel

		levelmgr.setNextLevel(levelmgr.curLevel.getWorld(), levelmgr.curLevel.getLevel());

		Fade.FadeAndStartScene ("GameScene", 3.0f, cam);

		return;
	}

	public static void startNextLevel(){
		int nextWorld = PlayerPrefs.GetInt (LevelManager.NEXTWORLD, 0);
		int nextLevel = PlayerPrefs.GetInt (LevelManager.NEXTLEVEL, 0);
		levelmgr.setCurrentLevel (nextWorld, nextLevel);
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

	//Load the Gamefield
	public void loadFields(){

		//Create Gamefield
		level = LevelManager.getLevelMax ();
		world = LevelManager.getWorldMax ();

		//für blockierende Steine zwischen den Welten
		height = world * 2 - 1; 

		fields = new Field[level, height];
		for (int j = 0; j < height; j++) { 
			//add WorldBlock Field
			if (j % 2 == 1) {
				addField (0, j, 9);
				loadFieldColor (0, j);

				continue;
			}
			for (int i = 0; i < level; i++) {
				addField (i, j, 9);
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
		
	public void loadFieldColor(int x, int y){
		Field fie = fields [x, y];
		if (y % 2 == 1) {
			if(levelmgr.worldCompleted >= (y/2 + 1)) {
				// World Enabled
				fie.setColor(Col.WORLDUNLOCKEDCOLOR);
			} else {
				// World Disable
				fie.setColor(Col.WORLDBLOCKCOLOR);
			}
		} else {
			if(levelmgr.worldCompleted >= y/2) {
				// Field Enabled
				if (levelmgr.levels [y / 2, x].getCompleted () == 0) {
					fie.setColor(Col.ENABLEDCOLOR);
				} else {
					fie.setColor(Col.COMPLETEDCOLOR);
				}
			} else {
				// Field Disable
				fie.setColor(Col.BLOCKEDCOLOR);
			}
		}
	}

	private static void setColor(GameObject g, Color col){
		g.GetComponent<MeshRenderer> ().material.SetColor("_DiffuseColor",col);
	}

	public void loadPlayer(){
		//Create Player
		playerobj = LevelPlay.prefabsMgr.generateObjectFromPrefab ("cube0");
		playerobj.AddComponent<MeshRenderer> ().material = Materials.glanz;
		playerobj.GetComponent<MeshRenderer>().material.SetColor("_DiffuseColor",Col.WEISS);
		playerobj.transform.localScale = new Vector3 (0.3f, 0.3f, 0.3f);
		playerobj.name = "PlayerDynamic";
		playerobj.AddComponent <LevelPlayerController>();

		Vector2 pos = new Vector2(PlayerPrefs.GetInt(LevelManager.NEXTLEVEL,0), PlayerPrefs.GetInt(LevelManager.NEXTWORLD,0));

		playerobj.transform.position =  new Vector3(pos.x, 1.39f +splash.GetComponent<Splash>().getSplashOffset(), pos.y*2); //new Vector3 (0.0f,9.4f,0.0f);
		splash.GetComponent<Splash>().setSplashOffset(0.0f);
		playerobj.transform.rotation = Quaternion.Euler(0, 0, 0);
		playerobj.tag = "Player";
		gamePosition = new Vector2 (pos.x, pos.y*2);
	}

	public void loadGameObjects(){
		//initialize text
		levelText = GameObject.Find("LevelText").GetComponent<Text> ();
		worldText = GameObject.Find("WorldText").GetComponent<Text> ();
		statusText = GameObject.Find("StatusText").GetComponent<Text> ();
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

	public void loadButtons(){
		listButton = Resources.Load<Button> ("listButton");//GameObject.Find ("ListButton").GetComponent<Button> ();
		soundOnButton = Resources.Load<Button> ("soundOnButton");//GameObject.Find ("soundOnButton").GetComponent<Button> ();
		shareButton = Resources.Load<Button> ("shareButton"); //GameObject.Find ("shareButton").GetComponent<Button> ();
		infoButton = Resources.Load<Button> ("infoButton"); //GameObject.Find ("infoButton").GetComponent<Button> ();

//		startButtonPos = new Vector3 (infoButton.transform.position.x, infoButton.transform.position.y, infoButton.transform.position.z);
//		soundOnEndPosition = new Vector3 (infoButton.transform.position.x, infoButton.transform.position.y - 65, infoButton.transform.position.z);
//		shareEndPosition = new Vector3 (infoButton.transform.position.x, infoButton.transform.position.y - 130, infoButton.transform.position.z);
//		infoEndPosition = new Vector3 (infoButton.transform.position.x, infoButton.transform.position.y - 195, infoButton.transform.position.z);

		soundOnImage = Resources.Load <Sprite>("soundOnButton");
		soundOffImage = Resources.Load <Sprite>("soundOffButton");

	}

	public void onSoundOn(){
		if (PlayerPrefs.GetInt("SoundOn") == 1) {
			AudioListener.pause = true;
			soundOnButton.image.sprite = soundOffImage;
			PlayerPrefs.SetInt ("SoundOn", 0);
		} else {
			AudioListener.pause = false;
			soundOnButton.image.sprite = soundOnImage;
			PlayerPrefs.SetInt ("SoundOn", 1);
		}
	}
		
	public void onShare(){
		Share.IntentShareText ("This game is awesome! Get the game from play store: balblaLink!");
	}

	public void onInfo(){

	}


	public void onListButton(){ 
		if (transitionEnd == true) {
			StartCoroutine (TransitionButtons (0.5f));
			if (listButtonClicked == false) {
				listButtonClicked = true;
			} else {
				listButtonClicked = false;
			}
		}
	}


	IEnumerator TransitionButtons(float lerpSpeed)
	{    
		
		float t = 0.0f;

		if (listButtonClicked == false) {
			
			while (t < 1.0f) {
				
				transitionEnd = false;
				t += Time.deltaTime * (Time.timeScale / lerpSpeed);

				soundOnButton.transform.position = new Vector3 (
					startButtonPos.x, Mathf.SmoothStep (startButtonPos.y, soundOnEndPosition.y, t), startButtonPos.z);

				shareButton.transform.position = new Vector3 (
					startButtonPos.x, Mathf.SmoothStep (startButtonPos.y, shareEndPosition.y, t),startButtonPos.z);

				infoButton.transform.position = new Vector3 (
					startButtonPos.x, Mathf.SmoothStep (startButtonPos.y, infoEndPosition.y, t), startButtonPos.z);
			
				yield return 0;

			}
			transitionEnd = true;
			InputManager.active = false;
		
		}else{
			
			while (t < 1.0f) {
				
				transitionEnd = false;
				t += Time.deltaTime * (Time.timeScale / lerpSpeed);

				soundOnButton.transform.position = new Vector3 (
					Mathf.SmoothStep (soundOnEndPosition.x, startButtonPos.x, t),
					Mathf.SmoothStep (soundOnEndPosition.y, startButtonPos.y, t),
					Mathf.SmoothStep (soundOnEndPosition.z, startButtonPos.z, t));

				shareButton.transform.position = new Vector3 (
					Mathf.SmoothStep (shareEndPosition.x, startButtonPos.x, t),
					Mathf.SmoothStep (shareEndPosition.y, startButtonPos.y, t),
					Mathf.SmoothStep (shareEndPosition.z, startButtonPos.z, t));

				infoButton.transform.position = new Vector3 (
					Mathf.SmoothStep (infoEndPosition.x, startButtonPos.x, t),
					Mathf.SmoothStep (infoEndPosition.y, startButtonPos.y, t),
					Mathf.SmoothStep (infoEndPosition.z, startButtonPos.z, t));
				
				yield return 0;

			}
			transitionEnd = true;
			InputManager.active = true;
		}
			
	}

}