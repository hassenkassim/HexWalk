using UnityEngine;
using System.Collections;


/*
 * This class controls the Gameplay
 * */
public class GameplayController : MonoBehaviour {

	const int SHOWREADY = 0;
	const int SHOWREMAIN = 1;
	const int SHOWNOTHING = 2;
	const int SHOWNOTHING2 = 3;
	int showID;

	public float timer1;
	public Font myFont;
	public GUIStyle myStyle;
	public string txt;

	// Use this for initialization
	void Start () {
		myFont = (Font)Resources.Load("fontTimer", typeof(Font));
		myStyle = new GUIStyle ();
		myStyle.font = myFont;
		myStyle.normal.textColor = Color.black;
		myStyle.fontSize = 32;

		//setup all timer here
		timer1 = 2;
		showID = SHOWREADY;

	}

	// Update is called once per frame
	void Update () {
		switch(showID){
		case SHOWREADY:
			txt = "Ready?";
			if (timer1 < 0) {
				showID = SHOWREMAIN;
				timer1 = 2;
			}
			break;
		case SHOWREMAIN:
			txt = "Remaining time: " + (int)timer1;
			if (Gameplay.pathfinder != null)
				Gameplay.pathfinder.coloring = true;
			if (timer1 < 0) {
				showID = SHOWNOTHING;
				Gameplay.cam.GetComponent<CameraPosition> ().startTransition ();
			}
			break;
		case SHOWNOTHING:
			if (Gameplay.pathfinder != null) {
				Gameplay.pathfinder.coloringWhite = true;
				showID = SHOWNOTHING2;
			}
			txt = "";
			break;
		case SHOWNOTHING2:
			break;
		}

		if (timer1 > 0) {

			timer1 -= Time.deltaTime;

		}
	}

	void LateUpdate() {
		
	}

	void OnGUI(){
			GUI.Label (new Rect (300, 80, 250, 50), txt, myStyle);
	}

}
