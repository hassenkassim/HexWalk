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
				setScale (new Vector3 (0.4f, 0.4f, 0.05f));
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


	//fracture objects:
	public void fractureCube(float scaling, Field field){
		GameObject fracture1=null;

		Gameplay.explode=true;

		for (int numFrac = 0; numFrac < field.getTransform().localScale.x/scaling+1; numFrac++) {
			for (int yAxis = 0; yAxis < field.getTransform ().localScale.y / scaling+1; yAxis++) {
				fracture1 = GameObject.CreatePrimitive (PrimitiveType.Cube);
				fracture1.transform.position = new Vector3(
					field.getTransform().position.x+(float)numFrac*scaling-field.getTransform().localScale.x/2,
					field.getTransform().position.y,
					field.getTransform().position.z-field.getTransform().localScale.z/2+yAxis*scaling);
				fracture1.transform.localScale= new Vector3 (scaling+Random.Range(-0.02f,0.2f), 0.1f, scaling+Random.Range(-0.02f,0.2f));
				fracture1.AddComponent<Rigidbody> ();
				fracture1.GetComponent<Rigidbody> ().mass = 0.0001f;
				fracture1.GetComponent<Rigidbody> ().useGravity = true;
				fracture1.name = "fracture"+numFrac+yAxis;

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
