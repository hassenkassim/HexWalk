using UnityEngine;
using System.Collections;

public class SkyBoxChoosing : MonoBehaviour {

	Camera cam;
	float world;

	// Use this for initialization
	void Start () {
		cam = Camera.main;
		cam.gameObject.AddComponent<Skybox> ();
		world = 0;
		cam.GetComponent<Skybox> ().material = Resources.Load<Material> ("skybox/skybox" + world);
	}
	
	// Update is called once per frame
	void Update () {
		InputManager.active = true;
		float y = InputManager.getHorizontalInput ();
		if (y != 0) {
			world += y;
			cam.GetComponent<Skybox> ().material = Resources.Load<Material> ("skybox/skybox" + world);
			//DebugConsole.Log ("Skybox: " + world);
		}
	}
}
