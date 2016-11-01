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

	public static int playerPositionX;
	public static int playerPositionZ;

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
	public static Text starNumberText;

	public static GameObject playerobj;

	public static GameObject[,] fields;

	public static Gamefield gamefield;

	public static Button firstStar;
	public static Button secondStar;
	public static Button thirdStar;

	public static Button settingsButton;
	public static Button shoppingButton;
	public static Button soundOnButton;
	public static Button soundOffButton;
	public static Button infoButton;
	public static Button playButton;

	public static Sprite starGold;
	public static Sprite starGrey;

	public static Camera cam;

	public static Vector2 gamePosition;

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

		//Call Sound Manager constructor
		soundMgr = new SoundManager();

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

		//load StarObjects
		loadStarObjects();

		//setAudio
		setAudio();

		radius = sideLength * Mathf.Sqrt (2f) / 2f;



		//Set Stars count
		starNumberText.text = LevelManager.stars.ToString();

		//Setup Camera
		cam = Camera.main;
		cam.gameObject.AddComponent <CameraPositionLevelPlay>();

	}


	public void Update () {
			

		if (InputManager.getClickTouchInput ()) {
			//start Level




			if(fields [(int)gamePosition.x, (int)gamePosition.y].GetComponent<MeshRenderer> ().material.color == Col.COMPLETEDCOLOR || fields [(int)gamePosition.x, (int)gamePosition.y].GetComponent<MeshRenderer> ().material.color == Col.SELECTEDCOLOR){
				if (shoppingCanvasActive != true || settingsCanvas != true) {
					//TODO: implement difficult grade algorithm

					PlayerPrefs.SetInt ("gameFieldWidth", 3);
					PlayerPrefs.SetInt ("gameFieldHeight", 2);
					PlayerPrefs.SetInt ("numberOfColor", 1);

					if (fields [(int)gamePosition.x, (int)gamePosition.y].GetComponent<MeshRenderer> ().material.color == Col.SELECTEDCOLOR) {
						playFromCurLevel = true;
					}

					SceneManager.LoadScene ("GameScene");
					return;
				}
			}

		}

			
		float x = InputManager.getHorizontalInput();
		float y = 0;

		if(x==0) y = InputManager.getVerticalInput();


		//check if move is allowed
		if (checkOutside (x,y)) {
			//Debug.Log ("Player out of Gamefield, No Rotation possible!");
			return;
		}

		if ((x != 0 || y != 0) && !isRotate) {

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


	//Load the Gamefield
	public void loadFields(){
		
	//Create Gamefield
	level = LevelManager.getLevelMax ();
	world = LevelManager.getWorldMax ();


	//für blockierende Steine zwischen den Welten
	height = world * 2 - 1; 

	fields = new GameObject[level, height];


		for (int j = 0; j < height; j++) { 
			//add WorldBlock Field
			if (j % 2 == 1) {
				addField (0, j, "plate3");
				loadFieldColor (0, j);
				continue;
			}

			for (int i = 0; i < level; i++) {
				addField (i, j, "plate3");
				loadFieldColor (i, j);
			}
		}
	}

	public void addField(int x, int y, string type){
		fields [x, y] = LevelPlay.prefabsMgr.generateObjectFromPrefab (type);
		fields [x, y].transform.localScale = new Vector3 (0.4f, 0.4f, 0.05f);
		fields [x, y].transform.position = new Vector3 (x, 1, y);
		fields [x, y].name = x + "_" + y;
		fields [x, y].tag = "LevelField";
	}
		
	public static void loadFieldColor(int x, int y){
		GameObject field = fields [x, y];

		if (y % 2 == 1) {
			if(LevelManager.worldCompleted >= (y/2 + 1)) {
				// World Enabled
				setColor (field, Col.WORLDUNLOCKEDCOLOR);
			} else {
				// World Disable
				setColor (field, Col.WORLDBLOCKCOLOR);
			}
		} else {
			if(LevelManager.worldCompleted >= y/2) {
				// Field Enabled
				if (LevelManager.levels [y / 2, x].getCompleted () == 0) {
					setColor (field, Col.ENABLEDCOLOR);
				} else {
					setColor (field, Col.COMPLETEDCOLOR);
				}
			} else {
				// Field Disable
				setColor (field, Col.BLOCKEDCOLOR);
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
		playerobj.transform.position = new Vector3(playerPositionX, 3, playerPositionZ);
		//playerobj.transform.position = new Vector3(PlayerPrefs.GetInt("level") - 1, 3, PlayerPrefs.GetInt("world") - 1);
		playerobj.transform.rotation = Quaternion.Euler(0, 0, 0);
		playerobj.tag = "Player";
		gamePosition = new Vector2 (playerPositionX, playerPositionZ);
		Rigidbody playerRigidBody = playerobj.AddComponent<Rigidbody>(); // Add the rigidbody
		playerRigidBody.mass = 0.5f;
		playerRigidBody.angularDrag = 0.05f;
		playerRigidBody.useGravity = true;

		//Load previous Player Location
		playerPositionX = (PlayerPrefs.GetInt("level") - 1);
		playerPositionZ = (PlayerPrefs.GetInt ("world") * 2 - 2);
	}

	public void loadGameObjects(){
		//initialize text
		levelText = GameObject.Find("LevelText").GetComponent<Text>();
		worldText = GameObject.Find("WorldText").GetComponent<Text>();
		statusText = GameObject.Find("StatusText").GetComponent<Text>();
		starNumberText = GameObject.Find("StarNumberText").GetComponent<Text>();
		firstStar = GameObject.Find("FirstStar").GetComponent<Button>();
		secondStar = GameObject.Find("SecondStar").GetComponent<Button>();
		thirdStar = GameObject.Find("ThirdStar").GetComponent<Button>();
	}

	public static void loadStarObjects(){
		starGold = Resources.Load<Sprite> ("stern_gold2");
		starGrey = Resources.Load<Sprite> ("stern_leer2");
		disableStars ();
	}

	public static void enableStars(){
		firstStar.gameObject.SetActive (true);
		secondStar.gameObject.SetActive (true);
		thirdStar.gameObject.SetActive (true);
	}

	public static void disableStars(){
		firstStar.gameObject.SetActive (false);
		secondStar.gameObject.SetActive (false);
		thirdStar.gameObject.SetActive (false);
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

	public static void collision(){

		if (firstTouchWithPlate == true) {
			InputManager.active = true;
			print ("Touch set to active");
			firstTouchWithPlate = false;
		}

		//play the RotationSound
		soundMgr.playRotationSound ("LevelScene");
		
		if (gamePosition.y % 2 != 1) {
			levelText.text = "Level: " + (gamePosition.x + 1);
			worldText.text = "World: " + ((int)(gamePosition.y/2 + 1));

			if (fields [(int)gamePosition.x, (int)gamePosition.y].GetComponent<MeshRenderer> ().material.color == Col.COMPLETEDCOLOR) {
				statusText.text = "Level finished";
			} else if (fields [(int)gamePosition.x, (int)gamePosition.y].GetComponent<MeshRenderer> ().material.color == Col.SELECTEDCOLOR) {
				statusText.text = "Level current";
			} else {
				statusText.text = "Level blocked";
			}
		
			enableText ();

			if (PlayerPrefs.GetInt ("Star X:" + gamePosition.x + " Y:" + gamePosition.y) == 3) {
				firstStar.image.sprite = starGold;
				secondStar.image.sprite = starGold;
				thirdStar.image.sprite = starGold;

				enableStars ();
				
			} else if (PlayerPrefs.GetInt ("Star X:" + gamePosition.x + " Y:" + gamePosition.y) == 2) {
				firstStar.image.sprite = starGold;
				secondStar.image.sprite = starGold;
				thirdStar.image.sprite = starGrey;

				enableStars ();

			} else if (PlayerPrefs.GetInt ("Star X:" + gamePosition.x + " Y:" + gamePosition.y) == 1) {
				firstStar.image.sprite = starGold;
				secondStar.image.sprite = starGrey;
				thirdStar.image.sprite = starGrey;

				enableStars ();

			} else if (PlayerPrefs.GetInt ("Star X:" + gamePosition.x + " Y:" + gamePosition.y) == 0) {
				disableStars ();
			}
		
			print ("Stars:" + PlayerPrefs.GetInt("Star X:" + gamePosition.x + " Y:" + gamePosition.y));

		} else {
			disableText ();
			playButton.gameObject.SetActive (false);
			disableStars ();
		}
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

	//Button functions
	public void onPlay(){
		if(fields [(int)gamePosition.x, (int)gamePosition.y].GetComponent<MeshRenderer> ().material.color == Col.COMPLETEDCOLOR || fields [(int)gamePosition.x, (int)gamePosition.y].GetComponent<MeshRenderer> ().material.color == Col.SELECTEDCOLOR){
			if (shoppingCanvasActive != true || settingsCanvas != true) {
				
				//TODO: implement difficult grade algorithm

				PlayerPrefs.SetInt ("gameFieldWidth", 3);
				PlayerPrefs.SetInt ("gameFieldHeight", 2);
				PlayerPrefs.SetInt ("numberOfColor", 1);

				if (fields [(int)gamePosition.x, (int)gamePosition.y].GetComponent<MeshRenderer> ().material.color == Col.SELECTEDCOLOR) {
					playFromCurLevel = true;
				}

				SceneManager.LoadScene ("GameScene");
				return;
			}
		}
	}

	public void onSettings(){
		if (settingsCanvasActive == false) {
			InputManager.active = false;
			settingsCanvas.gameObject.SetActive (true);
			settingsCanvasActive = true;
			disableText ();
			disableStars ();
		} else {
			InputManager.active = true;
			settingsCanvas.gameObject.SetActive (false);
			settingsCanvasActive = false;
			if (fields [(int)gamePosition.x, (int)gamePosition.y].GetComponent<MeshRenderer> ().material.color != Col.WORLDUNLOCKEDCOLOR) {
				enableText ();
			}
			if (fields [(int)gamePosition.x, (int)gamePosition.y].GetComponent<MeshRenderer> ().material.color == Col.COMPLETEDCOLOR) {
				enableStars ();
			}
		}
	}

	public void onShopping(){
		if (shoppingCanvasActive == false) {
			InputManager.active = false;
			shoppingCanvas.gameObject.SetActive (true);
			shoppingCanvasActive = true;
			disableText ();
			disableStars ();
		} else {
			InputManager.active = true;
			shoppingCanvas.gameObject.SetActive (false);
			shoppingCanvasActive = false;
			if (fields [(int)gamePosition.x, (int)gamePosition.y].GetComponent<MeshRenderer> ().material.color != Col.WORLDUNLOCKEDCOLOR) {
				enableText ();
			}
			if (fields [(int)gamePosition.x, (int)gamePosition.y].GetComponent<MeshRenderer> ().material.color == Col.COMPLETEDCOLOR) {
				enableStars ();
			}
		}
	}

	public void onInfo(){
		
	}

	public void onSoundOn()
	{
		soundOnButton.gameObject.SetActive (false);
		soundOffButton.gameObject.SetActive (true);

		PlayerPrefs.SetInt("soundIsOn", 0);

		//PlayerPrefs.Save ();
		Debug.Log("Sound: "+soundIsOn);
		PlayerPrefs.Save();

		AudioListener.pause = true;
	}

	public void onSoundOff()
	{
		soundOnButton.gameObject.SetActive (true);
		soundOffButton.gameObject.SetActive (false);

		PlayerPrefs.SetInt("soundIsOn", 1);
			
		Debug.Log("Sound: "+soundIsOn);
		PlayerPrefs.Save();

		AudioListener.pause = false;
	}

	public void onBack(){
		settingsCanvas.gameObject.SetActive (false);
		InputManager.active = true;
		settingsCanvasActive = false;
		if (fields [(int)gamePosition.x, (int)gamePosition.y].GetComponent<MeshRenderer> ().material.color != Col.WORLDUNLOCKEDCOLOR) {
			enableText ();
		}
		if (fields [(int)gamePosition.x, (int)gamePosition.y].GetComponent<MeshRenderer> ().material.color == Col.COMPLETEDCOLOR) {
			enableStars ();
		}
	}

	public static bool checkOtherCanvasActive(){
		if (shoppingCanvasActive == true || settingsCanvas == true) {
			return true;
		}else{
			return false;
		}
	}





}
