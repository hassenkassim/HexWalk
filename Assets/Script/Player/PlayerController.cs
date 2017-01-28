/************************************************
 * HAMOTO Production 2016						*
 * Project: HexWalk								*
 * Authors: Tolga, Mohamed, Dursun, Hassen		*
 * Year: 2016									*
 *************************************************/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;



/*
 * This class controls the player and the collisions with other gameobjects
 * */
public class PlayerController : MonoBehaviour {

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




	// Use this for initialization
	void Start () {
		radius = sideLength * Mathf.Sqrt (2f) / 2f;

	
	}

	// Update is called once per frame
	void Update () {
		Vector2 input = InputManager.getInput ();

		if (input.x == 0 && input.y == 0) {
			if (InputManager.getClickTouchInput ()) {
				Gameplay.player.setNextColor ();
				return;
			} else
				return;
		}

		//check if move is allowed
		if (!moveAllowed ((int)input.x, (int)input.y)) {
			return;
		}


		if ((input.x != 0 || input.y != 0) && !isRotate) {
			rotatePlayer (input.x, input.y);										
		}
	}
		
	void FixedUpdate() {
		rotation ();
	}

	private void rotatePlayer(float x, float y){
		if (x != 0) {
			Gameplay.player.setGamePosition (new Vector2 (Gameplay.player.getGamePosition ().x + x, Gameplay.player.getGamePosition ().y));
		} else if (y != 0) {
			Gameplay.player.setGamePosition (new Vector2 (Gameplay.player.getGamePosition ().x, Gameplay.player.getGamePosition ().y + y));
		}

		directionX = -x;															
		directionZ = y;																
		startPos = transform.position;												
		fromRotation = transform.rotation;											
		transform.Rotate (directionZ * 90, 0, directionX * 90, Space.World);		
		toRotation = transform.rotation;											
		transform.rotation = fromRotation;											
		rotationTime = 0;															
		isRotate = true;
	}

	private void rotation(){
		if (isRotate) {

			rotationTime += Time.fixedDeltaTime;									
			float ratio = Mathf.Lerp(0, 1, rotationTime / rotationPeriod);			

			float thetaRad = Mathf.Lerp(0, Mathf.PI / 2f, ratio);					
			float distanceX = -directionX * radius * (Mathf.Cos (45f * Mathf.Deg2Rad) - Mathf.Cos (45f * Mathf.Deg2Rad + thetaRad));		
			float distanceY = radius * (Mathf.Sin(45f * Mathf.Deg2Rad + thetaRad) - Mathf.Sin (45f * Mathf.Deg2Rad));						
			float distanceZ = directionZ * radius * (Mathf.Cos (45f * Mathf.Deg2Rad) - Mathf.Cos (45f * Mathf.Deg2Rad + thetaRad));			
			transform.position = new Vector3(startPos.x + distanceX, startPos.y + distanceY, startPos.z + distanceZ);						

			transform.rotation = Quaternion.Lerp(fromRotation, toRotation, ratio);	

			if (ratio == 1) {
				isRotate = false;
				directionX = 0;
				directionZ = 0;
				rotationTime = 0;
			}
		}
	}


	//TODO: Gamover Check transfer to Gameover class
	void OnCollisionEnter(Collision coll)
	{
		if (coll.collider.gameObject.CompareTag("Field")) {
			Gameplay.collision ();
		}
	}


	void OnCollisionExit(Collision collisionInfo) {
		
	}

	private bool moveAllowed(int x, int y){
		return inGamefield (x, y);
	}

	private bool inGamefield(int x, int y){
		int maxwidth = Gameplay.gamefield.fields.GetLength (0);
		int maxheight = Gameplay.gamefield.fields.GetLength (1);

		if ((int)Gameplay.player.getGamePosition ().x + x >= maxwidth|| (int)Gameplay.player.getGamePosition ().x + x < 0
			|| (int)Gameplay.player.getGamePosition ().y + y >= maxheight || (int)Gameplay.player.getGamePosition ().y + y < 0) {
			return false;
		} 
		return true;
	}

	public static IEnumerator startswitchingcolor(float timetowait){
		while (true) {
			if ((LevelPlay.levelmgr.curLevel.getWorld () % 2) == 0) {
				break;
			}
			Gameplay.player.setNextColor ();
			yield return new WaitForSeconds (timetowait);
		}
	}
}