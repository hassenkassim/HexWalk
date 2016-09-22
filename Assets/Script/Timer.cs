using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

	public float timer;
	bool pathHided = false;

	// Use this for initialization
	void Start () {
		timer = 4;
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
		int a = Gameplay.gamefield.getFieldWidth() - 1;
		int b = Gameplay.gamefield.getFieldHeight() - 1;
		for (int i = 0; i <= a; i++) {
			for (int j = 0; j <= b; j++){
				Gameplay.gamefield.getField (i, j).setColor (Color.white);
			}
				
		}
		pathHided = true;
	}
}

