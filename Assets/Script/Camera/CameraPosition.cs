﻿/************************************************
 * HAMOTO Production 2016						*
 * Project: HexWalk								*
 * Authors: Tolga, Mohamed, Dursun, Hassen		*
 * Year: 2016									*
 *************************************************/

using UnityEngine;
using System.Collections;

/*
 * This class should keep track of the camera position
 * */
public class CameraPosition : MonoBehaviour {

	readonly private Vector3 offsetPlayerCam = new Vector3 (0.0f, 3.0f, -5.0f);
	readonly private Vector3 rotationPlayerCam = new Vector3 (29f, 0.0f, 0.0f);
	private Vector3 offsetGamefieldCam;
	readonly private Vector3 rotationGamefieldCam = new Vector3 (90.0f, 0.0f, 0.0f);

	private Vector3 currentAngle;

	private bool follow;

	public bool startGyro = false;

	/*
	 * CamID == 0: PlayerCam
	 * CamID == 1: GamefieldCam
	 * CamID == 2: PlayerCam - Just Rotation!
	 * */
	public static int CamID = 1;

	// Use this for initialization
	void Start () {
		follow = false;
		CamID = 1;

		/* analyze best camera position and rotation for Gamefield view
		 * Position: 	x: in the middle of Gamefield.width
		 * 				y: 15.0f
		 * 				z: -3.0f
		 * Rotation:	x: depends on Gamefield.height
		 * */
		offsetGamefieldCam = new Vector3 (((float)Gameplay.gamefield.width-1)/2, 2.0f + ((float)Gameplay.gamefield.getFieldHeight() * ((float)9/(float)5)), ((float)Gameplay.gamefield.height-1)/2);;

		//initial setup of camera position and rotation
		setPosition (offsetGamefieldCam);
		setRotation (rotationGamefieldCam);

	}

	// Update is called once per frame
	void LateUpdate () {
		if (follow) {
			if (CamID == 0) {
				setPosition (Gameplay.player.getTransform ().position + offsetPlayerCam);
			} else if (CamID == 1) {
				setPosition (offsetGamefieldCam);
				setRotation (rotationGamefieldCam);
			} else if(CamID == 2){
				Vector3 relativePos = Gameplay.player.playerobj.transform.position - transform.position;
				transform.rotation = Quaternion.LookRotation (relativePos);
			}	
		}
	}


	public Vector3 getPosition(){
		return transform.position;
	}

	public void setPosition(Vector3 pos){
		transform.position = pos;
	}

	public void setRotation(Vector3 rot){
		transform.rotation = Quaternion.Euler(rot.x, rot.y, rot.z);
	}
		

	public void startTransition(){
		CamID = 0;
		StartCoroutine(TransitionGamefieldPlayer(2)); 	
	}

	public void setToFollowPlayerByRotation(){
		CamID = 2;
		//PathfinderController.camID = true;
		//InputManager.active = true;
	}
		
	IEnumerator TransitionGamefieldPlayer(float lerpSpeed)
	{
		float t = 0.0f;
		Vector3 newPosition;
		//get Position on the fly
		newPosition = Gameplay.player.getTransform ().position + offsetPlayerCam;
		Vector3 newRotation = rotationPlayerCam;
		Vector3 startingPos = transform.position;
		currentAngle = transform.eulerAngles;

		while (t < 1.0f)
		{
			t += Time.deltaTime * (Time.timeScale / lerpSpeed);

			transform.position = new Vector3(
				Mathf.SmoothStep(startingPos.x, newPosition.x, t),
				Mathf.SmoothStep(startingPos.y, newPosition.y, t),
				Mathf.SmoothStep(startingPos.z, newPosition.z, t));


			currentAngle = new Vector3(
				Mathf.SmoothStep(rotationGamefieldCam.x, newRotation.x, t),
				Mathf.SmoothStep(rotationGamefieldCam.y, newRotation.y, t),
				Mathf.SmoothStep(rotationGamefieldCam.z, newRotation.z, t));

			transform.eulerAngles = currentAngle;

			yield return 0;
		}


		//dursun
		BackgroundManager.setParticleSystem(Gameplay.cam);

		follow = true;

		InputManager.active = true;
		//enable Input
		PathfinderController.camID = true;

		//start Gyro
		Gameplay.Gyro.GetComponent<GyroController>().setCamOK(true);

		follow = true;

		yield return 0;
	}
}