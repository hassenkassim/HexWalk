using UnityEngine;
using System.Collections;

public class PathfinderController : MonoBehaviour {

	private bool tmp;

	// Use this for initialization
	void Start () {
		tmp = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Gameplay.pathfinder != null && Gameplay.pathfinder.coloring == true) {
			StartCoroutine (colorWay (Color.yellow, 0.1f));
		} else if (Gameplay.pathfinder != null && Gameplay.pathfinder.coloringWhite == true) {
			StartCoroutine (colorWay (Color.white, 0.1f));
		}
	}


	IEnumerator colorWay(Color color, float timebetweenfields){
		for (int i = 0; i < Gameplay.pathfinder.path.Count; i++) {
			Gameplay.gamefield.getField ((int)Gameplay.pathfinder.path [i].x, (int)Gameplay.pathfinder.path [i].y).setColor (color);
			yield return new WaitForSeconds (timebetweenfields);
		}
		Gameplay.pathfinder.coloring = false;

		print ("coloring finished!");
		yield return 0;
	}
}
