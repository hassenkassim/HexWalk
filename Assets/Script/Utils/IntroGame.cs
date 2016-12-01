using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IntroGame : MonoBehaviour {

	public static GameObject playerobj;

	//public static Field[,] fields;
	//public static Gamefield gamefield;

	public static Camera cam;
	public static GameObject splash;

	public static Text introText;

	// Use this for initialization
	void Start () {
		InputManager.active = false;

		splash = GameObject.Find ("Splash");

		cam = Camera.main;
//		cam.gameObject.AddComponent <CameraPositionLevelPlay> ();

		initBackground();

		loadPlayer ();

		introText = GameObject.Find ("IntroText").GetComponent<Text> ();
		introText.text = "Welcome to the tutorial!";
		introText.fontSize = 20;

	}

	// Update is called once per frame
	void Update () {

	}

	private void initBackground(){
		BackgroundManager.loadSkybox(cam);
		cam.gameObject.AddComponent<Light> ();
		cam.GetComponent<Light> ().type = LightType.Directional;
		cam.GetComponent<Light> ().transform.eulerAngles= new Vector3 (200.0f,140.0f,15.0f);
		cam.GetComponent<Light> ().intensity = 0.8f;
	}

	public void loadPlayer(){
		//Create Player

		playerobj = GameObject.Find ("cube0");//LevelPlay.prefabsMgr.generateObjectFromPrefab (SplashLoad.getCubeName());
//		playerobj.AddComponent<MeshRenderer> ().material = Materials.glanz;
		playerobj.GetComponent<MeshRenderer>().material.SetColor("_Color",Col.WEISS);
		playerobj.transform.localScale = new Vector3 (0.3f, 0.3f, 0.3f);
		playerobj.name = "PlayerDynamic";
//		playerobj.AddComponent <LevelPlayerController>();
		setColor (playerobj, Col.ENABLEDCOLOR);

		Vector2 pos = new Vector2(PlayerPrefs.GetInt(LevelManager.NEXTLEVEL,0), PlayerPrefs.GetInt(LevelManager.NEXTWORLD,0));


		playerobj.transform.position =  new Vector3(pos.x, 1.39f +splash.GetComponent<Splash>().getSplashOffset(), pos.y*2); //new Vector3 (0.0f,9.4f,0.0f);


		splash.GetComponent<Splash>().setSplashOffset(20.0f);

		playerobj.transform.rotation = Quaternion.Euler(0, 0, 0);
		playerobj.tag = "Player";
	}

	private static void setColor(GameObject g, Color col){
		g.GetComponent<MeshRenderer> ().material.SetColor("_Color",col);
	}
}
