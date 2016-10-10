using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelPlay : MonoBehaviour {

	public static Player player;

	public static PlayerInfo playerInfo;
	public static SavePlayerPrefs savePlayerPrefs;

	public static Text levelText;
	public static Text worldText;
	public static Text statusText;

	public static GameObject levelTextObj;
	public static GameObject worldTextObj;
	public static GameObject statusTextObj;

	public static Camera cam;
	public static Gamefield gamefield;
	public static GameObject playerobj;
	public static Vector2 gamePosition;
	public static GameObject[,] fields;

	public static int level;
	public static int world;
	public static int height;

	public static Color [,] fieldColor;
	public static Color currentColor;
	public static Color finishedColor;
	public static Color blockedColor;
	public static Color[] saveColor1D;

	public static int [,] starField;

	//Playercontroller
	public float rotationPeriod = 0.25f;		
	public float sideLength = 1f;			

	bool isRotate = false;					
	float directionX = 0;					
	float directionZ = 0;					

	Vector3 startPos;						
	float rotationTime = 0;					
	float radius;							
	Quaternion fromRotation;				
	Quaternion toRotation;

	public static PrefabsManagerLevelPlay prefabsMgr;


	static AudioClip rotationSound;

	// Use this for initialization
	void Start () {

		if (PlayerPrefs.HasKey ("firstStart") == false) {
			PlayerPrefs.SetInt ("firstStart", 1);
		}

		levelTextObj = GameObject.Find("LevelText");
		levelText = levelTextObj.GetComponent<Text>();

		worldTextObj = GameObject.Find("WorldText");
		worldText = worldTextObj.GetComponent<Text>();

		statusTextObj = GameObject.Find("StatusText");
		statusText = statusTextObj.GetComponent<Text>();

		InputManager.active = true;

		radius = sideLength * Mathf.Sqrt (2f) / 2f;

		//Call Prefab Manager constructor
		prefabsMgr = (PrefabsManagerLevelPlay)GameObject.Find("System").GetComponent <PrefabsManagerLevelPlay>();

		//Create Player
		playerobj = LevelPlay.prefabsMgr.generateObjectFromPrefab ("cubeEckig");
		playerobj.transform.localScale = new Vector3 (0.3f, 0.3f, 0.3f);
		playerobj.name = "PlayerDynamic";
		playerobj.AddComponent <LevelPlayerController>();
		playerobj.transform.position = new Vector3(0, 3, 0);
		//playerobj.transform.position = new Vector3(PlayerPrefs.GetInt("level") - 1, 3, PlayerPrefs.GetInt("world") - 1);
		playerobj.transform.rotation = Quaternion.Euler(0, 0, 0);
		playerobj.tag = "Player";
		gamePosition = new Vector2 (0, 0);
		Rigidbody playerRigidBody = playerobj.AddComponent<Rigidbody>(); // Add the rigidbody
		playerRigidBody.mass = 0.5f;
		playerRigidBody.angularDrag = 0.05f;
		playerRigidBody.useGravity = true;

		//Create Gamefield
		//gamefield = new Gamefield (PlayerPrefs.GetInt("gameFieldWidth"), PlayerPrefs.GetInt("gameFieldHeight"), version);
		level = LevelManager.getLevelMax ();
		world = LevelManager.getWorldMax ();

		finishedColor = Color.green;
		currentColor = Color.cyan;
		blockedColor = Color.grey;

		height = world * 2 - 1; //für blockierende Steine zwischen den Welten

		fieldColor = new Color[level, height];

		fields = new GameObject[level, height];



		for (int j = 0; j < height; j++) { 
			for (int i = 0; i < level; i++) {

				//every second row is a blocking row
				if (j % 2 == 1) {
					fields[i,j] = LevelPlay.prefabsMgr.generateObjectFromPrefab ("plate3");
					fields[i,j].transform.localScale = new Vector3 (0.4f, 0.4f, 0.2f);
					fields[i,j].transform.position = new Vector3 (i, 1, j);
					fields[i,j].name = i + "_" + j;

					fieldColor[i,j] = fields [i, j].GetComponent<MeshRenderer> ().material.color = Color.black;
					fields[i,j].tag = "LevelField";

					if (i > 0) {
						fields [i, j].SetActive (false);
					}
				}else{

					if (PlayerPrefs.GetInt ("firstStart") == 1) {
					}

					fields[i,j] = LevelPlay.prefabsMgr.generateObjectFromPrefab ("plate3");
					fields[i,j].transform.localScale = new Vector3 (0.4f, 0.4f, 0.05f);
					fields[i,j].transform.position = new Vector3 (i, 1, j);
					fields[i,j].tag = "LevelField";
					fields[i,j].name = i + "_" + j;

					//coloring for first game start
					if (PlayerPrefs.GetInt ("firstStart") == 1) {
						fieldColor [i, j] = fields [i, j].GetComponent<MeshRenderer> ().material.color = blockedColor;
						if (i == 0 && j == 0) {
							fieldColor [i, j] = fields [i, j].GetComponent<MeshRenderer> ().material.color = currentColor;
						}
					} else {
						fields [i, j].GetComponent<MeshRenderer> ().material.color = fieldColor [i, j];
					}
						
				}

				}

		}
			
		/*
		//convert 2D array to 1d array for PlayerPrefsX function (only fpr 1d arrays)
		saveColor1D = new Color[level * height];
		for(int i = 0; i < height; i++){
			for(int j = 0; j < level; j++){
				Color color;
				color = fieldColor[j,i];
				saveColor1D[i*height+j] = color;

			}
		}

		PlayerPrefsX.SetColorArray("levelFieldColor", saveColor1D);
		*/

			
		//Setup Camera
		cam = Camera.main;
		cam.gameObject.AddComponent <CameraPositionLevelPlay>();

	}



	void Update () {
		if (InputManager.getClickTouchInput ()) {
			if(fields [(int)gamePosition.x, (int)gamePosition.y].GetComponent<MeshRenderer> ().material.color == finishedColor || fields [(int)gamePosition.x, (int)gamePosition.y].GetComponent<MeshRenderer> ().material.color == currentColor){

				//TODO: implement difficult algorithm

				PlayerPrefs.SetInt("gameFieldWidth", 4);
				PlayerPrefs.SetInt("gameFieldHeight", 3);
				PlayerPrefs.SetInt("numberOfColor", 1);

				SceneManager.LoadScene ("GameScene");
				return;
			}

		}
			
		float x = InputManager.getHorizontalInput();
		float y = 0;
		if(x==0) y = InputManager.getVerticalInput();

		//print (x + "");


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

	void FixedUpdate() {

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


	bool checkOutside(float x, float y){
		if (x == -1) {
			if (gamePosition.x == 0)
				return true;
		} else if (x == 1) {
			if (gamePosition.x == level - 1 || gamePosition.y%2 == 1)
				return true;
		} else if (y == -1) {
			if (gamePosition.y == 0)
				return true;
			//if(fields [(int)gamePosition.x, (int)gamePosition.y - 1].GetComponent<MeshRenderer> ().material.color == Color.black)
				//return true;
		} else if (y == 1) {
			if (gamePosition.y == height - 1)
				return true;
			//if(fields [(int)gamePosition.x, (int)gamePosition.y + 1].GetComponent<MeshRenderer> ().material.color == Color.black)
				//return true;

		}
		return false;
	}


	public static void collision(){
		//Field field = gamefield.getField ((int)platePos.x, (int)platePos.y);
		//fields[(int)gamePosition.x,(int)gamePosition.y].GetComponent<MeshRenderer> ().material.color = Color.yellow;
		if (gamePosition.y % 2 != 1) {
			levelText.text = "Level: " + (gamePosition.x + 1);
			worldText.text = "World: " + ((int)(gamePosition.y/2 + 1));

			if (fields [(int)gamePosition.x, (int)gamePosition.y].GetComponent<MeshRenderer> ().material.color == finishedColor) {
				statusText.text = "Level finished";
			} else if (fields [(int)gamePosition.x, (int)gamePosition.y].GetComponent<MeshRenderer> ().material.color == currentColor) {
				statusText.text = "Level current";
			} else {
				statusText.text = "Level blocked";
			}
				
			levelText.GetComponent<Text> ().enabled = true;
			worldText.GetComponent<Text> ().enabled = true;
			statusText.GetComponent<Text> ().enabled = true;
		
			//print ("World:" + LevelPlay.fields[(int)LevelPlay.gamePosition.y].name + ", Level:" + (LevelPlay.gamePosition.x + 1));

		} else {
			levelText.GetComponent<Text> ().enabled = false;
			worldText.GetComponent<Text> ().enabled = false;
			statusText.GetComponent<Text> ().enabled = false;
		}
	}

	public void arrayTo1dArray(){
		for(int i = 0; i < height; i++){
			for(int j = 0; j < level; j++){
				Color color;
				color = fieldColor[j,i];
				saveColor1D[i*height+j] = color;

			}
		}
	}


}
