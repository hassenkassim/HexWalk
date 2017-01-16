using UnityEngine;
using System.Collections;

public class SplashLoad : MonoBehaviour {
	public static PrefabsManagerSplashScene prefabsMgr;

	public GameObject splash;
	public static Camera cam;
	public static GameObject pointLight;
	public static GameObject staticLight;
	public static GameObject playerobj;

	public static GameObject gameName;
	public static string cubeName;

	private Vector3 beginLight = new Vector3 (-4f, 10.0f, 0.0f);
	private Vector3 endLight = new Vector3 (4f, 10.0f, 0.0f);

	// Use this for initialization
	void Start () {

		//Disable Input
		InputManager.active = false;

		Fade.StartFadeIn(2.0f);
		//Call Prefab Manager constructor
		prefabsMgr = (PrefabsManagerSplashScene)GameObject.Find ("System").GetComponent <PrefabsManagerSplashScene> ();

		initLight ();

		splash = GameObject.Find ("Splash");
		DontDestroyOnLoad (splash);

		//Setup Camera
		cam = Camera.main;

		BackgroundManager.loadSkybox (cam);
		BackgroundManager.setParticleSystem (cam);

		setCubeName ("cube0");

		//Load the Player
		loadPlayer ();

		// for the first splash; dursun
		splash.GetComponent<Splash> ().initSplash();

		if (splash.GetComponent<Splash> ().getSplashShown () == 0)
			Fade.StartFadeIn(2.0f);

		//sound
		if (PlayerPrefs.GetInt ("SoundIsOn", 1) == 1) {
			AudioListener.pause = false;
		} else {
			AudioListener.pause = true;
		}
	}

	// Update is called once per frame
	void Update () {
		moveLight ();
	}
		
	public void loadPlayer(){
		//Create Player
		playerobj = prefabsMgr.generateObjectFromPrefab (cubeName);
		playerobj.AddComponent<MeshRenderer> ().material = Materials.glanz;
		playerobj.GetComponent<MeshRenderer>().material.SetColor("_Color",Col.WEISS);
		playerobj.transform.localScale = new Vector3 (0.3f, 0.3f, 0.3f);
	}

	private void initLight(){
		pointLight = new GameObject();
		pointLight.name = "pointLight";
		pointLight.AddComponent<Light> ();
		pointLight.GetComponent<Light> ().range = 2.0f;
		pointLight.GetComponent<Light> ().intensity = 3.6f;
		pointLight.GetComponent<Light> ().color = Color.white;
		pointLight.transform.position = beginLight;

		//HASSEN: Additional faint Light to see CubeWalk
		staticLight = new GameObject();
		staticLight.name = "staticLight";
		staticLight.AddComponent<Light> ();
		staticLight.GetComponent<Light> ().color = Color.white;
		staticLight.GetComponent<Light> ().type = LightType.Directional;
		staticLight.GetComponent<Light> ().intensity = 0.4f;
		staticLight.transform.rotation = Quaternion.Euler(new Vector3(200.0f, 180.0f, 10.0f));
	}
		
	private void moveLight(){

		pointLight.transform.position = Vector3.Lerp (beginLight, endLight, Mathf.PingPong (Time.time*1.0f, 1.0f));

	}

	public static void setCubeName(string name){
		cubeName = name;
	}
	public static string getCubeName(){
		return cubeName;
	}
}
