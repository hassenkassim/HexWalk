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

	public static LevelManager levelmanager;

	public Vector3 offsetPlayerCam = new Vector3 (0.0f, 3.0f, -4.0f);
	public Vector3 rotationPlayerCam = new Vector3 (29f, 0.0f, 0.0f);

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

	public static bool setPos=false;
	private static bool particleSet;
	private static bool firstFade;

	// Use this for initialization
	void Start () {
		splash = GameObject.Find ("Splash");
		levelmanager = new LevelManager ();

		firstFade = false;
		particleSet = false;

		//dursun 
		switch (splash.GetComponent<Splash> ().getSplashShown ()) {
		case 0:
			posX = 0;
			walkPosY = 1.299f;
			cubePosY = 1.36f;
			posZ = -2.0f;//-0.303f;

			setPosition (startCamPos);
			setRotation (startCamRotation);
			break;
		case 1:
			LevelPlay.playerobj.GetComponent<Rigidbody> ().useGravity = true;
			splash.GetComponent<Splash> ().setSplashShown (2);
			break;
		default:
//			InputManager.active = true;
			setPosition (LevelPlay.playerobj.transform.position + offsetPlayerCam);
			//DebugConsole.Log ("2");

			//setRotation (rotationPlayerCam);
			break;
		}
			
	}

	void Update () {
		if (splash.GetComponent<Splash> ().getSplashShown () == 1) {
			//DebugConsole.Log ("1");
			setPosition (LevelPlay.playerobj.transform.position + offsetPlayerCam);
			//setRotation (rotationPlayerCam);
			return;
		}

		//initial setup of camera position and rotation
		if (LevelPlayerController.showID == 0) {
			//setPosition (walkLogo.transform.position + startCamPos);

			setPosition (LevelPlay.playerobj.transform.position + startCamPos + Vector3.back * 3.5f);
			setRotation (startCamRotation);

		} else if (LevelPlayerController.showID == 10) {
			setPosition (LevelPlay.playerobj.transform.position + offsetPlayerCam);
			setRotation (rotationPlayerCam);

			//dursun
			if (!particleSet) {
				BackgroundManager.setParticleSystem (LevelPlay.cam);
				particleSet = true;
			}

		} else if (LevelPlayerController.showID == 9) {
			if (!firstFade) {
				Fade.StartFadeIn (2.0f);
				firstFade = true;
			}

			// here first time after gamescene: different rotation

			Vector3 relativePos = LevelPlay.playerobj.transform.position - transform.position;
			LevelPlay.cam.transform.rotation = Quaternion.LookRotation (relativePos);
			LevelPlay.cam.transform.position = new Vector3 (LevelPlay.playerobj.transform.position.x+offsetPlayerCam.x,1.35f+offsetPlayerCam.y,LevelPlay.playerobj.transform.position.z+offsetPlayerCam.z);
			if (!setPos) {
				setPos = true;
				setPosition (new Vector3(10.0f,4.35f,0.0f));
				setRotation (new Vector3(36.493f,0.0f,0.0f));
			}

				
			if (LevelPlay.playerobj.transform.position.y <= 1.5f) {
				LevelPlayerController.showID=10;
				setPosition (LevelPlay.playerobj.transform.position + offsetPlayerCam);
				setRotation (rotationPlayerCam);

				if (!particleSet) {
					BackgroundManager.setParticleSystem (LevelPlay.cam);
					particleSet = true;
					//LevelPlay.Gyro.GetComponent<GyroController>().setEnableGyro(true);
				}


			}
		} 
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

		Vector3 startingPos = new Vector3 (LevelPlay.playerobj.transform.position.x, LevelPlay.playerobj.transform.position.y, LevelPlay.playerobj.transform.position.z - 5);//startCamPos;// + walkLogo.transform.position;
			
		currentAngle = transform.eulerAngles;

		while (t < 1.0f) {

			t += Time.deltaTime * (Time.timeScale / lerpSpeed);

			transform.position = new Vector3 (
				Mathf.SmoothStep (startingPos.x, newPosition.x, t),
				Mathf.SmoothStep (startingPos.y, newPosition.y, t),
				Mathf.SmoothStep (startingPos.z, newPosition.z, t));

			currentAngle = new Vector3 (
				Mathf.SmoothStep (startCamRotation.x, newRotation.x, t),
				Mathf.SmoothStep (startCamRotation.y, newRotation.y, t),
				Mathf.SmoothStep (startCamRotation.z, newRotation.z, t));

			transform.eulerAngles = currentAngle;

			if (t > 0.7f) {
					
			}

			yield return 0;
		}

		splash.GetComponent<Splash> ().setSplashShown (1);
			
		LevelPlayerController.showID = 10;

		//dursun
		if (!particleSet) {
			BackgroundManager.setParticleSystem (LevelPlay.cam);
			particleSet = true;
		}

		//enable Input
		InputManager.active = true;
		SoundManager.playMenuMusic ();

		if (PlayerPrefs.GetInt ("SoundOn", 1) == 0) {
			AudioListener.pause = true;
		}

		//start Gyro
		//LevelPlay.Gyro.GetComponent<GyroController>().setEnableGyro(true);

		//SoundManager.playLevelMusic ((int)LevelPlay.playerobj.transform.position.z / 2 + 1);

		yield return 0;
	}

	public Transform getTransform(){
		return LevelPlay.playerobj.transform;
	}
}