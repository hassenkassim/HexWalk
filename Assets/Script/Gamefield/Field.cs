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
	public int fieldVersion;

	public Field(string name, int fieldVersion){
		this.fieldVersion = fieldVersion;

		//TODO: VERSION MODULO RAUS

		switch (fieldVersion) {
		case 0:
			field = Gameplay.prefabsMgr.generateObjectFromPrefab ("platte0");
			setScale (new Vector3 (0.4f, 0.4f, 0.05f));
			break;
		case 1:
			field = Gameplay.prefabsMgr.generateObjectFromPrefab ("platte1");
			setScale (new Vector3 (0.4f, 0.4f, 0.05f));
			break;
		case 2:
			field = Gameplay.prefabsMgr.generateObjectFromPrefab ("platte2");
			setScale (new Vector3 (0.4f, 0.4f, 0.05f));
			break;
		case 3:
			field = Gameplay.prefabsMgr.generateObjectFromPrefab ("platte3");
			setScale (new Vector3 (0.4f, 0.4f, 0.05f));
			break;
		case 4:
			field = Gameplay.prefabsMgr.generateObjectFromPrefab ("platte4");
			setScale (new Vector3 (0.4f, 0.4f, 0.05f));
			break;
		case 5:
			field = Gameplay.prefabsMgr.generateObjectFromPrefab ("platte5");
			setScale (new Vector3 (0.4f, 0.4f, 0.05f));
			break;
		case 6:
			field = Gameplay.prefabsMgr.generateObjectFromPrefab ("platte6");
			setScale (new Vector3 (0.4f, 0.4f, 0.05f));
			break;
		case 7:
			field = Gameplay.prefabsMgr.generateObjectFromPrefab ("platte7");
			setScale (new Vector3 (0.4f, 0.4f, 0.05f));
			break;
		case 8:
			field = Gameplay.prefabsMgr.generateObjectFromPrefab ("platte8");
			setScale (new Vector3 (0.4f, 0.4f, 0.05f));
			break;
		case 9:
			field = Gameplay.prefabsMgr.generateObjectFromPrefab ("platte1");
			setScale (new Vector3 (0.4f, 0.4f, 0.05f));
			break;
		case 10:
			field = Gameplay.prefabsMgr.generateObjectFromPrefab ("platte10");
			setScale (new Vector3 (0.4f, 0.4f, 0.05f));
			break;
		case 11:
			field = Gameplay.prefabsMgr.generateObjectFromPrefab ("platte11");
			setScale (new Vector3 (0.4f, 0.4f, 0.05f));
			break;
		case 12:
			field = Gameplay.prefabsMgr.generateObjectFromPrefab ("platte12");
			setScale (new Vector3 (0.4f, 0.4f, 0.05f));
			break;
		}

		field.AddComponent<MeshRenderer> ().material = Materials.glanz;

		setColor (Col.STANDARDFIELDCOLOR);
		field.name = name;
		field.tag = "Field";


	}

	public Field(string name, int fieldVersion, bool lvlplay){
		this.fieldVersion = fieldVersion;

		//TODO: VERSION MODULO RAUS

		switch (fieldVersion) {
		case 0:
			field = LevelPlay.prefabsMgr.generateObjectFromPrefab ("platte0");
			setScale (new Vector3 (0.4f, 0.4f, 0.05f));
			break;
		case 1:
			field = LevelPlay.prefabsMgr.generateObjectFromPrefab ("platte1");
			setScale (new Vector3 (0.4f, 0.4f, 0.05f));
			break;
		case 2:
			field = LevelPlay.prefabsMgr.generateObjectFromPrefab ("platte2");
			setScale (new Vector3 (0.4f, 0.4f, 0.05f));
			break;
		case 3:
			field = LevelPlay.prefabsMgr.generateObjectFromPrefab ("platte3");
			setScale (new Vector3 (0.4f, 0.4f, 0.05f));
			break;
		case 4:
			field = LevelPlay.prefabsMgr.generateObjectFromPrefab ("platte4");
			setScale (new Vector3 (0.4f, 0.4f, 0.05f));
			break;
		case 5:
			field = LevelPlay.prefabsMgr.generateObjectFromPrefab ("platte5");
			setScale (new Vector3 (0.4f, 0.4f, 0.05f));
			break;
		case 6:
			field = LevelPlay.prefabsMgr.generateObjectFromPrefab ("platte6");
			setScale (new Vector3 (0.4f, 0.4f, 0.05f));
			break;
		case 7:
			field = LevelPlay.prefabsMgr.generateObjectFromPrefab ("platte7");
			setScale (new Vector3 (0.4f, 0.4f, 0.05f));
			break;
		case 8:
			field = LevelPlay.prefabsMgr.generateObjectFromPrefab ("platte8");
			setScale (new Vector3 (0.4f, 0.4f, 0.05f));
			break;
		case 9:
			field = LevelPlay.prefabsMgr.generateObjectFromPrefab ("platte9");
			setScale (new Vector3 (0.4f, 0.4f, 0.05f));
			break;
		case 10:
			field = LevelPlay.prefabsMgr.generateObjectFromPrefab ("platte10");
			setScale (new Vector3 (0.4f, 0.4f, 0.05f));
			break;
		case 11:
			field = LevelPlay.prefabsMgr.generateObjectFromPrefab ("platte11");
			setScale (new Vector3 (0.4f, 0.4f, 0.05f));
			break;
		case 12:
			field = LevelPlay.prefabsMgr.generateObjectFromPrefab ("platte12");
			setScale (new Vector3 (0.4f, 0.4f, 0.05f));
			break;
		}
			
		field.AddComponent<MeshRenderer> ().material = Materials.glanz;


		field.name = name;
		field.tag = "Field";


	}

	public Field(string name, System.Type[] comp, Color col){
		field = GameObject.CreatePrimitive(PrimitiveType.Cube);
		field.AddComponent<MeshRenderer> ().material = Materials.glanz;
		field.GetComponent<MeshRenderer> ().material.SetColor("_Color", col);
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
		} 
	}



	public void setScale(Vector3 scale){
		field.transform.localScale = scale;
	}

	public void setColor(Color col){
		field.GetComponent<MeshRenderer> ().material.SetColor("_Color", col);
	}
		
	public void setTexture(string file){
		Material mat = Resources.Load<Material> ("flag");
		field.GetComponent<MeshRenderer> ().material = mat;
	}

	public void setPosition(float x, float y){
		field.transform.position = new Vector3 (x, 1, y);
	}

	public void setPosition(float x, float y, float z){
		field.transform.position = new Vector3 (x, z, y);
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
		return field.GetComponent<MeshRenderer> ().material.GetColor("_Color");
	}

	public GameObject getGameobject(){
		return field;
	}

	public bool blocked(){
		Color col = field.GetComponent<MeshRenderer> ().material.GetColor("_Color");
		if(col.Equals(Col.BLOCKEDCOLOR) || col.Equals(Col.WORLDBLOCKCOLOR)){
			return true;
		} else{
			return false;
		}
	}

	public void setTag(string tag){
		field.tag = tag;
	}

	public string getName(){
		return field.name;
	}



}
