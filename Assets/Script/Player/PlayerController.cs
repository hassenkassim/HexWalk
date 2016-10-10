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


		if (InputManager.getClickTouchInput ()) {
			Gameplay.player.setNextColor();
			return;
		}


		float x = InputManager.getHorizontalInput();
		float y = 0;
		if(x==0) y = InputManager.getVerticalInput();


		//check if move is allowed
		if (checkOutside (x,y)) {
			Debug.Log ("Player out of Gamefield, No Rotation possible!");
			return;
		}


		if ((x != 0 || y != 0) && !isRotate) {

			if (x != 0) {
				Gameplay.player.setGamePosition (new Vector2 (Gameplay.player.getGamePosition ().x + x, Gameplay.player.getGamePosition ().y));
			} else if (y != 0) {
				Gameplay.player.setGamePosition (new Vector2 (Gameplay.player.getGamePosition ().x, Gameplay.player.getGamePosition ().y + y));
			}
			Debug.Log ("Player Position: " + Gameplay.player.getGamePosition());

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

	}
		
	void FixedUpdate() {

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


	bool checkOutside(float x, float y){
		if (x == -1) {
			if (Gameplay.player.getGamePosition ().x == 0)
				return true;
		} else if (x == 1) {
			if (Gameplay.player.getGamePosition ().x == Gameplay.gamefield.width - 1)
				return true;
		} else if (y == -1) {
			if (Gameplay.player.getGamePosition ().y == 0)
				return true;
		} else if (y == 1) {
			if (Gameplay.player.getGamePosition ().y == Gameplay.gamefield.height - 1)
				return true;
		}
		return false;
	}


}
