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


	public bool follow;
	public Vector3 offsetPlayerCam = new Vector3 (0.0f, 3.0f, -4.0f);
	public Vector3 rotationPlayerCam = new Vector3 (36.5f, 0.0f, 0.0f);

	public static Vector3 startCamPos = new Vector3 (0.0f, 0.0f, -0.5f);
	public Vector3 startCamRotation = new Vector3 (0f, 0.0f, 0.0f);

	private Vector3 currentAngle;

	public static GameObject walkLogo;
	public static GameObject cubeLogo;

	public float posX;
	public float cubePosY;
	public float walkPosY;
	public float posZ;

	public static bool splashShown;

	// Use this for initialization
	void Start () {
		
		posX = 0;
		walkPosY = 1.299f;
		cubePosY = 1.36f;
		posZ = -0.303f;
		splashShown = false;

		SoundManager.playSplashMusic ();

		walkLogo = GameObject.Find ("walkText");
		cubeLogo = GameObject.Find ("cubeText");

		walkLogo.transform.position = new Vector3 (posX, walkPosY, posZ);
		cubeLogo.transform.position = new Vector3 (posX, cubePosY, posZ);
		setPosition (walkLogo.transform.position + startCamPos);
		setRotation (startCamRotation);
	}

	void Update () {
		//initial setup of camera position and rotation
		if (LevelPlayerController.showID == 0) {
			setPosition (walkLogo.transform.position + startCamPos);
			setRotation (startCamRotation);
		} else {
			if (follow == true) {
				setPosition (LevelPlay.playerobj.transform.position + offsetPlayerCam);
				setRotation (rotationPlayerCam);
			}
		}
		//FadeOut

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
		lastTask ();
	}


	IEnumerator TransitionGamefieldPlayer(float lerpSpeed)
	{    
		lock (transitionLock) {
			float t = 0.0f;
			Vector3 newPosition;
			Vector3 newRotation = rotationPlayerCam;
			Vector3 startingPos = startCamPos + walkLogo.transform.position;
			currentAngle = transform.eulerAngles;

			while (t < 2.0f) {

				//get Position on the fly
				newPosition = getTransform ().position + offsetPlayerCam;

				t += Time.deltaTime * (Time.timeScale / lerpSpeed);

				transform.position = Vector3.Lerp (startingPos, newPosition, t);

				currentAngle = new Vector3 (
					Mathf.LerpAngle (startCamRotation.x, newRotation.x, t),
					Mathf.LerpAngle (startCamRotation.y, newRotation.y, t),
					Mathf.LerpAngle (startCamRotation.z, newRotation.z, t));

				transform.eulerAngles = currentAngle;

				yield return 0;
			}
		}
	}

	void lastTask()
	{
		lock(transitionLock)
		{
			follow = true;

			//enable Input
			InputManager.active = true;

			SoundManager.playMenuMusic ();
		}
	}

	public Transform getTransform(){
		return LevelPlay.playerobj.transform;
	}
		
}