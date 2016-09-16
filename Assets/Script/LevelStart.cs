using UnityEngine;
using System.Collections;

public class LevelStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Gamefield gamefield = new Gamefield (5, 5);
		gamefield.getField (1, 1).changeColor (Color.cyan);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
