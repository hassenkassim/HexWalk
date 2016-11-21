using UnityEngine;
using System.Collections;

public class SplashLoad : MonoBehaviour {
	public static PrefabsManagerSplashScene prefabsMgr;

	public GameObject splash;
	public static Camera cam;
	public static GameObject pointLight;
	public static GameObject playerobj;
	public static GameObject gameName;
	public static string cubeName;

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

		setCubeName ("cube0");

		//Load the Player
		loadPlayer ();

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

	public static void setCubeName(string name){
		cubeName = name;
	}
	public static string getCubeName(){
		return cubeName;
	}

	public void loadPlayer(){
		//Create Player
		playerobj = prefabsMgr.generateObjectFromPrefab (cubeName);
		playerobj.AddComponent<MeshRenderer> ().material = Materials.glanz;
		playerobj.GetComponent<MeshRenderer>().material.SetColor("_DiffuseColor",Col.WEISS);
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
