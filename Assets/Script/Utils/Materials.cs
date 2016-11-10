using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Materials : MonoBehaviour {

	public static Material glanz;

	void Awake () {
		glanz = Resources.Load ("Material/GlanzMaterial", typeof(Material)) as Material;
	}
}
