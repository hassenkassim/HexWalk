using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

	public float timer = 2;
	bool pathHided = false;

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
				if (pathHided == false) {
					hidePath ();
				}
			}
		}
	}

	void hidePath(){
		int a = Gameplay.gamefield.getFieldWidth();
		int b = Gameplay.gamefield.getFieldHeight();
		for (int i = 1; i <= a; i++) {
			for (int j = 1; j <= b; j++){
				Gameplay.gamefield.getField (i - 1, j - 1).setColor (Color.white);
			}
				
		}
		pathHided = true;
	}
}

