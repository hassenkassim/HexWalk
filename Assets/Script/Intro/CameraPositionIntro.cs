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
public class CameraPositionIntro : MonoBehaviour {

	readonly private Vector3 offsetPlayerCam = new Vector3 (0.0f, 3.0f, -5.0f);
	readonly private Vector3 rotationPlayerCam = new Vector3 (29f, 0.0f, 0.0f);
	private Vector3 offsetGamefieldCam;
	readonly private Vector3 rotationGamefieldCam = new Vector3 (0.0f, 0.0f, 0.0f);

	private Vector3 currentAngle;
	public static int CamID = 0;

	// Use this for initialization
	void Start () {
		offsetGamefieldCam = new Vector3 (0.0f, 9.59f, -5.0f);// -IntroGame.playerobj.transform.localScale.x / 2

		//initial setup of camera position and rotation
		setPosition (offsetGamefieldCam);
		setRotation (rotationGamefieldCam);

	}

	void LateUpdate () {
		if (CamID == 0) {
			setPosition (offsetGamefieldCam);
			setRotation (rotationGamefieldCam);
		}	else if (CamID == 1) {
			setPosition (IntroGame.playerobj.transform.position + offsetPlayerCam);
		} else if (CamID == 2) {
			Vector3 relativePos = IntroGame.playerobj.transform.position - transform.position;
			transform.rotation = Quaternion.LookRotation (relativePos);
		}
	}

	public void setToFollowPlayerByRotation(){
		CamID = 2;
		PathfinderController.camID = true;
	}

	public void setPosition(Vector3 pos){
		transform.position = pos;
	}

	public void setRotation(Vector3 rot){
		transform.rotation = Quaternion.Euler(rot.x, rot.y, rot.z);
	}

	public void startTransition(){
		StartCoroutine(TransitionGamefieldPlayer(2)); 	
	}
		

	IEnumerator TransitionGamefieldPlayer(float lerpSpeed)
	{    
		float t = 0.0f;
		Vector3 newPosition;
		//get Position on the fly
		newPosition = IntroGame.playerobj.transform.position + offsetPlayerCam;
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
	}
}