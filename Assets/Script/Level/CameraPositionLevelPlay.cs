/************************************************
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
public class CameraPositionLevelPlay : MonoBehaviour {

	readonly private Vector3 offsetPlayerCam = new Vector3 (0.0f, 3.0f, -4.0f);
	readonly private Vector3 rotationPlayerCam = new Vector3 (36.5f, 0.0f, 0.0f);

	// Use this for initialization
	void Start () {
		//initial setup of camera position and rotation
		setPosition (LevelPlay.playerobj.transform.position + offsetPlayerCam);
		setRotation (rotationPlayerCam);
	}

	void Update () {
		//initial setup of camera position and rotation
		setPosition (LevelPlay.playerobj.transform.position + offsetPlayerCam);
		setRotation (rotationPlayerCam);
	}

	public void setPosition(Vector3 pos){
		transform.position = pos;
	}

	public void setRotation(Vector3 rot){
		transform.rotation = Quaternion.Euler(rot.x, rot.y, rot.z);
	}
		
}