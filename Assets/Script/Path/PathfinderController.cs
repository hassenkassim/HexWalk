using UnityEngine;
using System.Collections;

public class PathfinderController : MonoBehaviour {


	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		if (Gameplay.pathfinder != null && Gameplay.pathfinder.coloring == true) {
			Gameplay.pathfinder.coloring = false;
			paintPath (Color.yellow, 0.1f);
		} else if (Gameplay.pathfinder != null && Gameplay.pathfinder.coloringWhite == true) {
			Gameplay.pathfinder.coloringWhite = false;
			paintPath (Color.white, 0.1f);
		}
	}

	public void paintPath(Color col, float timebetweenfields){
		for (int i = 0; i < Gameplay.pathfinder.path.Count-1; i++) {
			StartCoroutine(Gameplay.pathfinder.paintField(col,i*timebetweenfields,i));
		}
	}





}
