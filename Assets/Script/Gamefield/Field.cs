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


	//fracture objects:
	public void fractureCube(float scaling, Field field){
		GameObject fracture1=null;
		for (int numFrac = 0; numFrac < field.getTransform().localScale.x/scaling; numFrac++) {
			for (int zAxis = 0; zAxis < field.getTransform ().localScale.z / scaling; zAxis++) {
				fracture1 = GameObject.CreatePrimitive (PrimitiveType.Cube);
				fracture1.transform.position = new Vector3(
					field.getTransform().position.x+(float)numFrac*scaling-field.getTransform().localScale.x/2,
					field.getTransform().position.y,
					field.getTransform().position.z-field.getTransform().localScale.z/2+zAxis*scaling);
				fracture1.transform.localScale= new Vector3 (scaling, 0.1f, scaling	);
				fracture1.AddComponent<Rigidbody> ();
				fracture1.GetComponent<Rigidbody> ().mass = 0.0f;
				fracture1.GetComponent<Rigidbody> ().useGravity = true;
				fracture1.name = "fracture"+numFrac+zAxis;
			}
		}  // muessen wir die fractures destroyen ??

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
