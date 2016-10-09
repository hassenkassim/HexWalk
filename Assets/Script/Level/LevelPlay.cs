using UnityEngine;
using System.Collections;

public class LevelPlay : MonoBehaviour {

	public static Player player;

	public static PlayerInfo playerInfo;
	public static SavePlayerPrefs savePlayerPrefs;

	public static Camera cam;
	public static Gamefield gamefield;
	public static GameObject playerobj;
	Vector2 gamePosition;
	public GameObject[,] fields;
	public int level;
	public int world;
	public int height;
	public bool blackStoneActive;

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
		InputManager.active = true;

		radius = sideLength * Mathf.Sqrt (2f) / 2f;


		//Call Prefab Manager constructor
		prefabsMgr = (PrefabsManagerLevelPlay)GameObject.Find("System").GetComponent <PrefabsManagerLevelPlay>();


		//Create Player
		playerobj = LevelPlay.prefabsMgr.generateObjectFromPrefab ("cubeEckig");
		playerobj.transform.localScale = new Vector3 (0.3f, 0.3f, 0.3f);
		playerobj.name = "PlayerDynamic";
		//playerobj.AddComponent <PlayerController>();
		playerobj.transform.position = new Vector3(0, 3, 0);
		playerobj.transform.rotation = Quaternion.Euler(0, 0, 0);
		playerobj.tag = "Player";
		gamePosition = new Vector2 (0, 0);
		Rigidbody playerRigidBody = playerobj.AddComponent<Rigidbody>(); // Add the rigidbody
		playerRigidBody.mass = 0.5f;
		playerRigidBody.angularDrag = 0.05f;
		playerRigidBody.useGravity = true;

		//Create Gamefield
		//gamefield = new Gamefield (PlayerPrefs.GetInt("gameFieldWidth"), PlayerPrefs.GetInt("gameFieldHeight"), version);
		level = 10;
		world = 10;

		height = world * 2 - 1; //für blockierende Steine zwischen den Welten
		fields = new GameObject[level, height];
		for (int i = 0; i < level; i++) {
			for (int j = 0; j < height; j++) {
				if (j % 2 == 1) {
					fields[i,j] = LevelPlay.prefabsMgr.generateObjectFromPrefab ("plate3");
					fields[i,j].transform.localScale = new Vector3 (0.4f, 0.4f, 0.2f);
					fields[i,j].transform.position = new Vector3 (i, 1, j);
					fields[i,j].name = i + "_" + j;
					fields[i,j].GetComponent<MeshRenderer> ().material.color = Color.black;
					if (i > 0) {
						fields [i, j].SetActive (false);
					}
				}else{

				fields[i,j] = LevelPlay.prefabsMgr.generateObjectFromPrefab ("plate3");
				fields[i,j].transform.localScale = new Vector3 (0.4f, 0.4f, 0.05f);
				fields[i,j].transform.position = new Vector3 (i, 1, j);
				fields[i,j].name = i + "_" + j;
				//fields[i,j].tag = "LevelField";

				}
			}
		}




		//Setup Camera
		cam = Camera.main;
		cam.gameObject.AddComponent <CameraPositionLevelPlay>();

	}

	void Update () {
		if (InputManager.getClickTouchInput ()) {
			//Gameplay.player.setNextColor();
			//return;
		}


		float x = InputManager.getHorizontalInput();
		float y = 0;
		if(x==0) y = InputManager.getVerticalInput();

		print (x + "");


		//check if move is allowed
		if (checkOutside (x,y)) {
			Debug.Log ("Player out of Gamefield, No Rotation possible!");
			return;
		}


		if ((x != 0 || y != 0) && !isRotate) {

			if (x != 0) {
				gamePosition = new Vector2 (gamePosition.x + x, gamePosition.y);
			} else if (y != 0) {
				gamePosition = new Vector2(gamePosition.x, gamePosition.y+y);
			}
			Debug.Log ("Level Position: " + gamePosition);

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
	/*
	//TODO: Gamover Check transfer to Gameover class
	void OnCollisionEnter(Collision coll)
	{
		if (coll.collider.gameObject.CompareTag("Field")) {
			Gameplay.collision ();
		}
	}


	void OnCollisionExit(Collision collisionInfo) {
	}
	*/

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
		} else if (y == 1) {
			if (gamePosition.y == height - 1)
				return true;
		}
		return false;
	}

}
