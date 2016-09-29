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
			paintColorPath (0.1f);
		} else if (Gameplay.pathfinder != null && Gameplay.pathfinder.coloringWhite == true) {
			Gameplay.pathfinder.coloringWhite = false;
			paintWhitePath (0.1f);
		}
	}

	public void paintWhitePath(float timebetweenfields){
		for (int i = 1; i < Gameplay.pathfinder.path.Count-1; i++) {
			StartCoroutine(Gameplay.pathfinder.paintField(Color.white,i*timebetweenfields,i));
		}
	}

	public void paintColorPath(float timebetweenfields){
		for (int i = 0; i < Gameplay.pathfinder.path.Count-1; i++) {
			StartCoroutine(Gameplay.pathfinder.paintField(Gameplay.pathfinder.pathcolor[i],i*timebetweenfields,i));
		}
	}





}
