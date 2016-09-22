using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

	public float timer = 6;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (timer > -6) {
			timer -= Time.deltaTime;
		}
	}

	void OnGUI(){

		Font myFont = (Font)Resources.Load("fontTimer", typeof(Font));
		GUIStyle myStyle = new GUIStyle ();
		myStyle.font = myFont;
		myStyle.normal.textColor = Color.black;
		myStyle.fontSize = 32;


		if (timer > 0) {
			GUI.Label (new Rect (300, 80, 250, 50), "Remaining time: "+ timer.ToString ("0"), myStyle);
		} else {
			if (timer > -3) {
				GUI.Label (new Rect (300, 80, 250, 50), "Let's go!!!", myStyle);
			}
		}


	}
}

