/************************************************
 * HAMOTO Production 2016						*
 * Project: HexWalk								*
 * Authors: Tolga, Mohamed, Dursun, Hassen		*
 * Year: 2016									*
 *************************************************/

using UnityEngine;
using System.Collections;


/*
 * This class is used for each field in the gamefield
 * */
public class Field {
	GameObject field;

	public Field(string name){
		//TODO: Instead of creating a primitive Cube, we should use a 3D model with rounded corners
		field = GameObject.CreatePrimitive(PrimitiveType.Cube);
		field.name = name;
		field.tag = "Field";
		setScale (new Vector3 (0.8f, 0.1f, 0.8f));
	}

	public Field(string name, System.Type[] comp, Color col){
		field = GameObject.CreatePrimitive(PrimitiveType.Cube);
		field.GetComponent<MeshRenderer> ().material.color = col;
	}

	public void setScale(Vector3 scale){
		field.transform.localScale = scale;
	}

	public void setColor(Color col){
		field.GetComponent<MeshRenderer> ().material.color = col;
	}

	public void setPosition(float x, float y){
		field.transform.position = new Vector3 (x, 1, y);
	}

	public Transform getTransform(){
		return field.transform;
	}

	public Color getColor(){
		return field.GetComponent<MeshRenderer> ().material.color;
	}



}
