/************************************************
 * HAMOTO Production 2016						*
 * Project: HexWalk								*
 * Authors: Tolga, Mohamed, Dursun, Hassen		*
 * Year: 2016									*
 *************************************************/

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 * This class manages the gyroscope control of the game.
 * Only supports y-axis for now. Can be easily modified though.
 * */
public class GyroController : MonoBehaviour {

	Button butt;

	public readonly static string ICON1STR = "gyroIcon1";
	public readonly static string ICON2STR = "gyroIcon2";
	public readonly static string ICON3STR = "gyroIcon3";

	public bool firstStart = true;
	private bool gyroEnabled = false;
	public bool gyroactive = false;

	/* reference for the axes we want to detect changes 
 	*/
	private float y;

	//offset rotation sensibility
	float sensitivity = 15.0f;

	//IconObjects
	GameObject Icon1;
	GameObject Icon2;
	GameObject Icon3;

	Quaternion curIconRot1;
	Quaternion curIconRot2;
	Quaternion curIconRot3;
	Quaternion curCamRot;
	Quaternion resetCamRot;

	void Awake(){
		resetCamRot = Quaternion.Euler (new Vector3 (29f, 0.0f, 0.0f));
		//get icon gameobjects
		Icon1 = GameObject.Find("gyroIcon1");
		Icon2 = GameObject.Find("gyroIcon2");
		Icon3 = GameObject.Find("gyroIcon3");

		butt = GameObject.Find("GyroIcon").GetComponent<Button>();

	}

	void Start(){
		InitGyro ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!gyroactive)
			return;

		//if gyroscope is supported start GyroRotation
		if (gyroEnabled) {
			// Rotate Camera
			GyroRotation ();
			//Rotate icon
			rotateIcon ();
		}
	}

	public void InitGyro() {
		setEnableGyro (false);

		//Setup Buttons
		butt.GetComponent<Button>().onClick.RemoveAllListeners();
		butt.onClick.AddListener(delegate{toogleGyro();});


		//check wether Gyroscope exists
		if (HasGyroscope) {
			Input.gyro.enabled = true;              // enable the gyroscope
			Input.gyro.updateInterval = 0.0167f;    // set the update interval to it's highest value (60 Hz)
		} else {
			// show a error message for the devices without a gyroscope
		}

		//Check wether we are in the right Scenes, to activate or deactivate Icons
		setIconActive();
	}



	void GyroRotation(){
		//get y-value from the gyroscope
		y = Input.gyro.rotationRate.y;

		//map the y rotationRate for continuos rotation when the device is moving
		float yFiltered = FilterGyroValues(y);
		RotateRightLeft(yFiltered);
	}

	void rotateIcon(){
		Icon1.gameObject.transform.Rotate (-5, 5, 5);
		Icon2.gameObject.transform.Rotate (-5, -5, -5);
		Icon3.gameObject.transform.Rotate (5, 5, -5);
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
		Camera.main.transform.RotateAround(Camera.main.transform.position, Vector3.up, -axis * Time.deltaTime * sensitivity);
	}

	public void setEnableGyro(bool enabled){
		gyroEnabled = enabled;
	}

	private void toogleGyro(){
		if (gyroEnabled == false) {
			gyroEnabled = true;
		} else {
			gyroEnabled = false;
			resetRotation ();
		}
	}

	/*private void toogleGyro(){
//		DebugConsole.Log("TOOGLE");
		//butt1.GetComponent<Button>().onClick.RemoveAllListeners();
		//butt1.onClick.AddListener(delegate{toogleGyro();});
		if (gyroEnabled == false) {
			gyroEnabled = true;
		} else {
			gyroEnabled = false;
			resetRotation ();
		}
	}*/


	public void setIconActive(){
		if (getIconActivateInScene () && Input.gyro.enabled) {
			Icon1.gameObject.SetActive (true);
			Icon2.gameObject.SetActive (true);
			Icon3.gameObject.SetActive (true);
			gyroactive = true;
		} else {
			Icon1.gameObject.SetActive (false);
			Icon2.gameObject.SetActive (false);
			Icon3.gameObject.SetActive (false);
			gyroactive = false;
		}
	}

	private bool getIconActivateInScene(){
		string SceneName = ScenesManager.getCurrentSceneName ();
		//DebugConsole.Log ("SCENE: " + SceneName);

		switch (SceneName) {
		case ScenesManager.SCENE_SPLASH:
			return false;
		case ScenesManager.SCENE_GAME:
			return true;
		case ScenesManager.SCENE_LEVEL:
			return true;
		case ScenesManager.SCENE_LEVEL2:
			return true;
		default:

			return false;
		}
	}	

	public bool getGyroEnabled(){
		return gyroEnabled;
	}

	static bool HasGyroscope {
		get {
			return SystemInfo.supportsGyroscope;
		}
	}

	private void resetRotation(){
		curIconRot1 = Icon1.transform.rotation;
		curIconRot2 = Icon2.transform.rotation;
		curIconRot3 = Icon3.transform.rotation;
		curCamRot = Camera.main.transform.rotation;
		StartCoroutine (resetRot(0.5f));
	}

	IEnumerator resetRot(float lerpSpeed)
	{    
		float t = 0.0f;

		while (t < 1.0f)
		{
			t += Time.deltaTime * (Time.timeScale / lerpSpeed);

			Quaternion curIconRot1_ = Quaternion.Euler(new Vector3(
				Mathf.LerpAngle(curIconRot1.eulerAngles.x, 0.0f, t),
				Mathf.LerpAngle(curIconRot1.eulerAngles.y, 0.0f, t),
				Mathf.LerpAngle(curIconRot1.eulerAngles.z, 0.0f, t)));

			Quaternion curIconRot2_ = Quaternion.Euler(new Vector3(
				Mathf.LerpAngle(curIconRot2.eulerAngles.x, 0.0f, t),
				Mathf.LerpAngle(curIconRot2.eulerAngles.y, 0.0f, t),
				Mathf.LerpAngle(curIconRot2.eulerAngles.z, 0.0f, t)));

			Quaternion curIconRot3_ = Quaternion.Euler(new Vector3(
				Mathf.LerpAngle(curIconRot3.eulerAngles.x, 0.0f, t),
				Mathf.LerpAngle(curIconRot3.eulerAngles.y, 0.0f, t),
				Mathf.LerpAngle(curIconRot3.eulerAngles.z, 0.0f,t)));

			Icon1.transform.rotation = curIconRot1_;
			Icon2.transform.rotation = curIconRot2_;
			Icon3.transform.rotation = curIconRot3_;


			//lerp Cam
			Camera.main.transform.rotation = Quaternion.Slerp(curCamRot, resetCamRot, t);

			//float curCamRot_Y = Mathf.SmoothStep(curCamRot.eulerAngles.y, 0.0f, t);
			//Camera.main.transform.rotation = Quaternion.Euler(new Vector3(29.0f, 0.0f, 0.0f));

			yield return 0;
		}
	}



}