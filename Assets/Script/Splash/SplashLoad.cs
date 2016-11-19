using UnityEngine;
using System.Collections;

public class SplashLoad : MonoBehaviour {
	public static PrefabsManagerSplashScene prefabsMgr;

	public GameObject splash;
	public static Camera cam;
	public static GameObject pointLight;
	public static GameObject playerobj;
	public static GameObject gameName;

	private Vector3 beginLight =new Vector3 (-5.0f, 10.0f, -1.0f);
	private Vector3 endLight =new Vector3 (5.0f, 10.0f, -1.0f);

	// Use this for initialization
	void Start () {
		Fade.StartFadeIn(2.0f);
		//Call Prefab Manager constructor
		prefabsMgr = (PrefabsManagerSplashScene)GameObject.Find ("System").GetComponent <PrefabsManagerSplashScene> ();

		splash = GameObject.Find ("Splash");

		initLight ();

		DontDestroyOnLoad (splash);

		//Setup Camera
		cam = Camera.main;
	
		BackgroundManager.loadSkybox (cam);
		BackgroundManager.setParticleSystem (cam);
		//Load the Player
		loadPlayer ();

		cam.transform.position =new Vector3 (0.0f, 9.4f, -4.0f);
		cam.transform.rotation= Quaternion.Euler(0.0f, 0.0f, 0.0f);

		//Disable Input
		InputManager.active = false;

		// for the first splash; dursun
		splash.GetComponent<Splash> ().initSplash();


		if (splash.GetComponent<Splash> ().getSplashShown () == 0)
			Fade.StartFadeIn(2.0f);

		//start music
		//if (splash.GetComponent<Splash> ().getSplashShown () != 0) {
			SoundManager.playMenuMusic ();
		//} 
			
	
		//sound
		if (PlayerPrefs.GetInt ("SoundIsOn", 1) == 1) {
			//			soundOnButton.image.sprite = soundOnImage; 
			AudioListener.pause = false;
		} else {
			//			soundOnButton.image.sprite = soundOffImage;
			AudioListener.pause = true;
		}
	}

	// Update is called once per frame
	void Update () {
		pointLight.transform.position = Vector3.Lerp (beginLight, endLight, Mathf.PingPong (Time.time*1.0f, 1.0f));
	}


	public void loadPlayer(){
		//Create Player
		playerobj = prefabsMgr.generateObjectFromPrefab ("cube0");
		playerobj.AddComponent<MeshRenderer> ().material = Materials.glanz;
		playerobj.GetComponent<MeshRenderer>().material.SetColor("_Color",Col.WEISS);
		playerobj.transform.localScale = new Vector3 (0.3f, 0.3f, 0.3f);
	}

	private void initLight(){
		pointLight = new GameObject();
		pointLight.name = "pointLight";
		pointLight.AddComponent<Light> ();
		pointLight.GetComponent<Light> ().color = Color.white;
		pointLight.transform.position = beginLight;
	}
		
}
