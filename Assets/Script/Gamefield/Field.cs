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
	public GameObject field;
	public int version;

	public Field(string name, int version){
		this.version = version;


		switch (version) {
		case 0:
			field = GameObject.CreatePrimitive (PrimitiveType.Cube);
			setScale (new Vector3 (0.8f, 0.1f, 0.8f));
			break;
		case 1:
			field = Gameplay.prefabsMgr.generateObjectFromPrefab ("plate3");
			setScale (new Vector3 (0.4f, 0.4f, 0.1f));
			break;
		}
			
		//TODO: Instead of creating a primitive Cube, we should use a 3D model with rounded corners

		field.name = name;
		field.tag = "Field";
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

	public void activateRigidbody(){
		if (field.GetComponent<Rigidbody> () == null) {
			Rigidbody fieldRigidBody = field.AddComponent<Rigidbody> (); // Add the rigidbody
			fieldRigidBody.mass = 0.5f;
			fieldRigidBody.angularDrag = 0.05f;
			fieldRigidBody.useGravity = true;
		}
	}


	public Transform getTransform(){
		return field.transform;
	}

	public Color getColor(){
		return field.GetComponent<MeshRenderer> ().material.color;
	}

	public GameObject getGameobject(){
		return field;
	}



}
