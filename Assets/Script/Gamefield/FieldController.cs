using UnityEngine;
using System.Collections;


/*
 * This class uses MonoBehavior functions to initiate a Gameobject from a prefab
 */
public class FieldController : MonoBehaviour {

	public GameObject obj;

	// Use this for initialization
	void Start () {
		obj = (GameObject)Instantiate(Resources.Load("plate3"));
	}

	public GameObject getObject(){
		return obj;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
