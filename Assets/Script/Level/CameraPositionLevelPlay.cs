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

	object transitionLock = new object();

	public Vector3 offsetPlayerCam = new Vector3 (0.0f, 3.0f, -4.0f);
	public Vector3 rotationPlayerCam = new Vector3 (36.5f, 0.0f, 0.0f);

	public static Vector3 startCamPos = new Vector3 (0.0f, 0.0f, -1.5f);
	public Vector3 startCamRotation = new Vector3 (0f, 0.0f, 0.0f);

	private Vector3 currentAngle;

	public static GameObject walkLogo;
	public static GameObject cubeLogo;

	public GameObject splash;

	public float posX;
	public float cubePosY;
	public float walkPosY;
	public float posZ;



	// Use this for initialization
	void Start () {
		splash = GameObject.Find ("Splash");
		if (splash.GetComponent<Splash> ().getSplashShown () == 0) {
			posX = 0;
			walkPosY = 1.299f;
			cubePosY = 1.36f;
			posZ = -0.303f;

			SoundManager.playSplashMusic ();

			walkLogo = GameObject.Find ("walkText");
			cubeLogo = GameObject.Find ("cubeText");

			walkLogo.transform.position = new Vector3 (posX, walkPosY, posZ);
			cubeLogo.transform.position = new Vector3 (posX, cubePosY, posZ);
			setPosition (walkLogo.transform.position + startCamPos);
			setRotation (startCamRotation);
		} else {
			InputManager.active = true;
			setPosition (LevelPlay.playerobj.transform.position + offsetPlayerCam);
			setRotation (rotationPlayerCam);
		}
	}

	void Update () {
		if (splash.GetComponent<Splash> ().getSplashShown () == 1) {
			setPosition (LevelPlay.playerobj.transform.position + offsetPlayerCam);
			setRotation (rotationPlayerCam);
			return;
		}

		//initial setup of camera position and rotation
		if (LevelPlayerController.showID == 0) {
			setPosition (walkLogo.transform.position + startCamPos);
			setRotation (startCamRotation);
		} else if(LevelPlayerController.showID == 10) {
			setPosition (LevelPlay.playerobj.transform.position + offsetPlayerCam);
			setRotation (rotationPlayerCam);
		}
	}

	public static void disableSplash(){
		walkLogo.SetActive (false);
		cubeLogo.SetActive (false);
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
			newPosition = LevelPlay.playerobj.transform.position + offsetPlayerCam;
			Vector3 newRotation = rotationPlayerCam;
			Vector3 startingPos = startCamPos + walkLogo.transform.position;
			currentAngle = transform.eulerAngles;

			while (t < 2.0f) {

				t += Time.deltaTime * (Time.timeScale / lerpSpeed);

				transform.position = new Vector3(
					Mathf.SmoothStep(startingPos.x, newPosition.x, t),
					Mathf.SmoothStep(startingPos.y, newPosition.y, t),
					Mathf.SmoothStep(startingPos.z, newPosition.z, t));

				currentAngle = new Vector3 (
					Mathf.SmoothStep (startCamRotation.x, newRotation.x, t),
					Mathf.SmoothStep (startCamRotation.y, newRotation.y, t),
					Mathf.SmoothStep (startCamRotation.z, newRotation.z, t));

				transform.eulerAngles = currentAngle;

				yield return 0;
			}

			splash.GetComponent<Splash> ().setSplashShown (1);
			LevelPlayerController.showID = 10;
			//enable Input
			InputManager.active = true;

			SoundManager.playMenuMusic ();
	}

	public Transform getTransform(){
		return LevelPlay.playerobj.transform;
	}
		
}