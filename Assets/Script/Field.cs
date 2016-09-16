using UnityEngine;
using System.Collections;

public class Field {
	GameObject field;

	public Field(string name){
		field = GameObject.CreatePrimitive(PrimitiveType.Cube);
		field.name = name;
	}

	public Field(string name, System.Type[] comp, Color col){
		field = GameObject.CreatePrimitive(PrimitiveType.Cube);
		field.GetComponent<MeshRenderer> ().material.color = col;
	}

	public void changeColor(Color col){
		field.GetComponent<MeshRenderer> ().material.color = col;
	}

	public void changePosition(float x, float y){
		field.transform.position = new Vector3 (x, 0, y);
	}

}
