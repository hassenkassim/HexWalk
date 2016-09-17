using UnityEngine;
using System.Collections;

/*
 * This class should keep track of the camera position
 * */
public class CameraPosition : MonoBehaviour {

	private Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = transform.position - Gameplay.player.getPosition ();

		//Debug.Log ("Camera position: " + transform.position);
		//Debug.Log ("Player position: " + Gameplay.player.getPosition ());
	}

	// Update is called once per frame
	void LateUpdate () {
		transform.position = Gameplay.player.getPosition () + offset;
	}
}
