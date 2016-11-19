/************************************************
 * HAMOTO Production 2016						*
 * Project: HexWalk								*
 * Authors: Tolga, Mohamed, Dursun, Hassen		*
 * Year: 2016									*
 *************************************************/

using UnityEngine;
using System.Collections;

/*
 * This class manages the gyroscope control of the game.
 * Only supports y-axis for now. Can be easily modified though.
 * */
public class GyroController : MonoBehaviour {

	private bool gyroEnabled = false;

	/* reference for the axes we want to detect changes 
 	*/
	private float y;

	//offset rotation sensibility
	static float sensitivity = 15.0f;

	void Awake(){
		InitGyro();
	}
	
	// Update is called once per frame
	void Update () {

		//Wait for the Camera to reach starting point
		if (CameraPosition.CamID != 0)
			return;
		
		//if gyroscope is supported start GyroRotation
		if(gyroEnabled){
			GyroRotation();
		}
	}


	void InitGyro() {
		if (HasGyroscope) {
			Input.gyro.enabled = true;              // enable the gyroscope
			Input.gyro.updateInterval = 0.0167f;    // set the update interval to it's highest value (60 Hz)
			gyroEnabled = true;
		} else {
			// show a error message for the devices without a gyroscope
			DebugConsole.Log("The device's gyroscope can't be detected");
		}
	}

	bool HasGyroscope {
		get {
			return SystemInfo.supportsGyroscope;
		}
	}

	void GyroRotation(){
		//get y-value from the gyroscope
		y = Input.gyro.rotationRate.y;

		//map the y rotationRate for continuos rotation when the device is moving
		float yFiltered = FilterGyroValues(y);
		RotateRightLeft(yFiltered);
	}
		
	//to prevent "shaking" camera
	float FilterGyroValues(float axis) {
		if(axis < -0.1 || axis > 0.1){
			return axis;
		} else {
			return 0.0f;
		}
	}

	//rotate the camera rigt and left (y rotation)
	void RotateRightLeft(float axis){
		transform.RotateAround(transform.position, Vector3.up, -axis * Time.deltaTime * sensitivity);
	}

}
