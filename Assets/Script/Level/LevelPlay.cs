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

	public static GameObject Gyro;

	public static GameObject splash;
	public static GameObject gameName; //dursun

	//Buttons
	public static Button listButton;
	public static Button soundButton;
	public static Button shareButton;
	public static Button infoButton;

	private static float buttonHeight;


	public static float soundEndPosition;
	public static float shareEndPosition;
	public static float infoEndPosition;
	float soundBtnActPos;
	float shareBtnActPos;
	float infoBtnActPos;
	public bool listButtonClicked;
	public bool transitionEnd;
	public bool enumeratorEnd;
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

	public static IEnumerator buttonTrans;
	public static float startInfoY;
	public static float startShareY;
	public static float startSoundY;
	public static float endInfoY;
	public static float endShareY;
	public static float endSoundY;
	public static int aufzu = -1;
	public static float yvalueInfo;
	public static float yvalueShare;
	public static float yvalueSound;

	public static bool landing;
	public static bool infoPanel = false;

	public static GameObject staticLight;

	void Awake(){
		//Set GyroController
		//Gyro = GameObject.Find("GyroCanvas");
		//Gyro.AddComponent<GyroController> ();
	}

	// Use this for initialization
	public void Start () {
		
		//PlayerPrefs.DeleteAll ();

		InputManager.active = false;

		buttonTrans = TransitionButtons (0.2f);

		splash = GameObject.Find ("Splash");

		//Setup Camera
		cam = Camera.main;
		cam.gameObject.AddComponent <CameraPositionLevelPlay> ();

		initLight ();


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

		//load buttons
		transitionEnd = true;
		loadButtons ();

		//play music
		setAudio ();

		landing = false;

	}

	void initLight(){
		cam.gameObject.AddComponent<Light> ();
		cam.GetComponent<Light> ().type = LightType.Directional;
		cam.GetComponent<Light> ().transform.eulerAngles= new Vector3 (200.0f,140.0f,15.0f);

		cam.GetComponent<Light> ().intensity = 0.8f;

		//HASSEN: Additional faint Light to see CubeWalk
		staticLight = new GameObject();
		staticLight.name = "staticLight";
		staticLight.AddComponent<Light> ();
		staticLight.GetComponent<Light> ().color = Color.white;
		staticLight.GetComponent<Light> ().type = LightType.Directional;
		staticLight.GetComponent<Light> ().intensity = 0.2f;
		staticLight.transform.rotation = Quaternion.Euler(new Vector3(200.0f, 180.0f, 10.0f));

	}
		
	private void init(){
		curLevel = levelmgr.curLevel;

		radius = sideLength * Mathf.Sqrt (2f) / 2f;

		//dursun
		BackgroundManager.loadSkybox(cam);

		//tolga
		disableText();

		//print (splash.GetComponent<Splash> ().getSplashShown ());

		if (splash.GetComponent<Splash> ().getSplashShown () == 0) {
			//dursun
				gameName = SplashLoad.prefabsMgr.generateObjectFromPrefab ("gameName");
				gameName.GetComponent<Rigidbody> ().useGravity = false;

				gameName.transform.position = new Vector3 (-1.25f + playerobj.transform.position.x, 10.0f, playerobj.transform.position.z);
				gameName.AddComponent<Splash> ();
				gameName.SetActive (true);	
		}
		
	}


	public void Update () {
		//print (InputManager.active);
		Vector2 input = InputManager.getInput ();

		if (input.x == 0 && input.y == 0) {

			if (InputManager.getClickTouchInput ()) {
				//print ("STARTLEVEL");
				if(PlayerPrefs.GetInt("SoundOn", 1) == 1){
				SoundManager.stopMusic ();
				}
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
		if (PlayerPrefs.GetInt ("SoundOn", 1) == 1) {
			SoundManager.playRotationSound ("LevelScene");
		}

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
		return inGamefield (x, y);
	}

	private bool inGamefield(int x, int y){
		int maxwidth = fields.GetLength (0);
		int maxheight = fields.GetLength (1);

		int X = (int)gamePosition.x + x;
		int Y = (int)gamePosition.y + y;

		if (X >= maxwidth|| X < 0 || Y >= maxheight || Y < 0) {
			return false;
		} 

		if (X != 0 && (Y % 2 == 1))
			return false;

		Field field = fields [X, Y];
		if (field.blocked () == true) {
			return false;
		}
		return true;
	}
		

	public static void collision(){
		
		levelmgr.setCurrentLevel ((int)gamePosition.y / 2, (int)gamePosition.x);

		if (splash.GetComponent<Splash> ().getSplashShown () == 2) {

			if (landing == false) {
				SoundManager.playMenuMusic ();
				InputManager.active = true;
				if (PlayerPrefs.GetInt ("SoundOn", 1) == 0) {
					AudioListener.pause = true;
				}


				landing = true;			
			}
		}
		
		setCurrentFieldColor (Col.SELECTEDCOLOR);


		if (gamePosition.y % 2 != 1) {
			levelText.text = "Level: " + (gamePosition.x + 1);
			worldText.text = "World: " + ((int)(gamePosition.y/2 + 1));

			if (levelmgr.curLevel.getCompleted() == 1) {
				statusText.text = "";
			} else if (levelmgr.curLevel.getCompleted() == 0) {
				statusText.text = "";
			}
			enableText ();
		} else {
			disableText ();
		}

		if (!Camera.main.GetComponent<Skybox> ().material.name.Equals ("skybox" + levelmgr.curLevel.getWorld())){
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
		if (InputManager.active == true) {
			levelmgr.setNextLevel (levelmgr.curLevel.getWorld (), levelmgr.curLevel.getLevel ());
		}

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
	
		if (PlayerPrefs.GetInt ("soundIsOn", 1) == 1) {
			AudioListener.pause = false;
		} else {
			AudioListener.pause = true;
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
				addField (0, j, 0);
				loadFieldColor (0, j);

				continue;
			}
			for (int i = 0; i < level; i++) {
				addField (i, j, 0);
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
				if (levelmgr.levels [y/2, x].getCompleted () == 0) {
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
		g.GetComponent<MeshRenderer> ().material.SetColor("_Color",col);
	}

	public void loadPlayer(){
		//Create Player

		//DebugConsole.Log ("Cube Name: " + SplashLoad.getCubeName());

		playerobj = LevelPlay.prefabsMgr.generateObjectFromPrefab (SplashLoad.getCubeName());
		playerobj.AddComponent<MeshRenderer> ().material = Materials.glanz;
		playerobj.GetComponent<MeshRenderer>().material.SetColor("_Color",Col.WEISS);
		playerobj.transform.localScale = new Vector3 (0.3f, 0.3f, 0.3f);
		playerobj.name = "PlayerDynamic";
		playerobj.AddComponent <LevelPlayerController>();
		setColor (playerobj, Col.ENABLEDCOLOR);

		Vector2 pos = new Vector2(PlayerPrefs.GetInt(LevelManager.NEXTLEVEL,0), PlayerPrefs.GetInt(LevelManager.NEXTWORLD,0));
		//Debug.Log ("LevelManager.NEXTLEVEL:"+PlayerPrefs.GetInt(LevelManager.NEXTLEVEL,0)+ "   LevelManager.NEXTWORLD:"+PlayerPrefs.GetInt(LevelManager.NEXTWORLD,0));

		playerobj.transform.position =  new Vector3(pos.x, 1.39f +splash.GetComponent<Splash>().getSplashOffset(), pos.y*2); //new Vector3 (0.0f,9.4f,0.0f);


		splash.GetComponent<Splash>().setSplashOffset(20.0f);

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
		listButton = GameObject.Find ("ListButton").GetComponent<Button> ();
		soundButton = GameObject.Find ("SoundButton").GetComponent<Button> ();
		shareButton = GameObject.Find ("ShareButton").GetComponent<Button> ();
		infoButton = GameObject.Find ("InfoButton").GetComponent<Button> ();

		buttonHeight = getButtonHeight ();

		soundEndPosition = listButton.transform.position.y + listButton.transform.position.y/2;
		shareEndPosition = soundEndPosition + listButton.transform.position.y + listButton.transform.position.y;
		infoEndPosition = listButton.transform.position.y + listButton.transform.position.y + listButton.transform.position.y/2*3;


		soundOnImage = Resources.Load <Sprite>("soundOnButton");
		soundOffImage = Resources.Load <Sprite>("soundOffButton");


		if (PlayerPrefs.GetInt ("SoundOn", 1) == 1) {
			soundButton.image.sprite = soundOnImage;
		} else {
			soundButton.image.sprite = soundOffImage;
		}

		yvalueInfo = listButton.transform.position.y;
		yvalueShare = listButton.transform.position.y;
		yvalueSound = listButton.transform.position.y;

	}

	public void onSoundOn(){
		
		if (PlayerPrefs.GetInt("SoundOn") == 1) {
			AudioListener.pause = true;
			soundButton.image.overrideSprite = soundOffImage;
			PlayerPrefs.SetInt ("SoundOn", 0);
			

		} else {
			AudioListener.pause = false;
			soundButton.image.overrideSprite = soundOnImage;
			PlayerPrefs.SetInt ("SoundOn", 1);
	
		}
	}
		
	public void onShare(){
		Share.IntentShareText ("This game is awesome! Get the game from play store: balblaLink!");
	}

	public void onListButton(){
		//aufzu = 1 == zu
		//aufzu = -1 == auf

		float initheight = buttonHeight + 12;

		float height1 = initheight;
		float height2 = initheight*2.0f;
		float height3 = initheight*3.0f;


		StopCoroutine (buttonTrans);//Stop Coroutine

		aufzu = -aufzu; //toggle

		if (aufzu == 1) { //öffnen
//			InputManager.active = false;
			startInfoY = yvalueInfo;
			endInfoY = listButton.transform.position.y - height3;
			startShareY = yvalueShare;
			endShareY = listButton.transform.position.y - height2;
			startSoundY = yvalueSound;
			endSoundY = listButton.transform.position.y - height1;

		} else { // schließen
			if(infoPanel == true){
				InfoPanel.FadeOutPanel ();
				infoPanel = false;
			}
			startInfoY = yvalueInfo;
			endInfoY = listButton.transform.position.y;
			startShareY = yvalueShare;
			endShareY = listButton.transform.position.y;
			startSoundY = yvalueSound;
			endSoundY = listButton.transform.position.y;

		}

		float speed = Mathf.Abs (endInfoY - startInfoY) / 1000;

		buttonTrans = TransitionButtons (speed);

		StartCoroutine (buttonTrans);
	}
		

	IEnumerator TransitionButtons(float lerpSpeed)
	{    
	
		float t = 0.0f;

		while (t < 1.0f) {
			
			t += Time.deltaTime * (Time.timeScale / lerpSpeed);

			yvalueSound = Mathf.SmoothStep (startSoundY, endSoundY, t);
			yvalueInfo = Mathf.SmoothStep (startInfoY, endInfoY, t);
			yvalueShare = Mathf.SmoothStep (startShareY, endShareY, t);

			infoButton.transform.position = new Vector3(listButton.transform.position.x, yvalueInfo, listButton.transform.position.z);
			shareButton.transform.position = new Vector3(listButton.transform.position.x, yvalueShare, listButton.transform.position.z);
			soundButton.transform.position = new Vector3(listButton.transform.position.x, yvalueSound, listButton.transform.position.z);


			yield return 0;

		}

		yield return 0;

	}

	public void showAd(){
		AdManager.adFrequence = 5;
		AdManager.showVideo ();
	}

	public void showRewarded(){
		AdManager.showRewardedVideo ();
	}

	public void onInfo(){
		if (infoPanel == false) {
			InfoPanel.FadeInPanel ();
			infoPanel = true;
		} else {
			InfoPanel.FadeOutPanel ();
			infoPanel = false;
		}

	}

	public void onDescription(){

		Fade.FadeAndStartScene ("IntroScene", 3.0f, cam);
		InfoPanel.FadeOutPanel ();

	}

	public void onBack(){
		InfoPanel.FadeOutPanel ();
		infoPanel = false;
	}

	private float getButtonHeight(){

		Vector3 size = infoButton.GetComponent<BoxCollider2D> ().bounds.size;
		//print ("Size X: " + size.x);
		//print ("Size Y: " + size.y);
		//print ("Size Z: " + size.z);

		return 100 * size.x;
	}

}