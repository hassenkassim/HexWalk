/************************************************
 * HAMOTO Production 2016						*
 * Project: HexWalk								*
 * Authors: Tolga, Mohamed, Dursun, Hassen		*
 * Year: 2016									*
 *************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;


/*
 * This class is handling the player
 * */
public class Player {

	public GameObject playerobj;
	Vector2 gamePosition;
	Color curColor;
	int colorCount;
	int version;

	public Player(int colorCount, int version){
		this.version = version;

		switch (version) {
		case 0:
			playerobj = Gameplay.prefabsMgr.generateObjectFromPrefab ("cube0");
			setScale (new Vector3 (0.3f, 0.3f, 0.3f));
			break;
		case 1:
			playerobj = Gameplay.prefabsMgr.generateObjectFromPrefab ("cube3");
			setScale (new Vector3 (0.3f, 0.3f, 0.3f));
			break;
		case 2:
			playerobj = Gameplay.prefabsMgr.generateObjectFromPrefab ("cube5");
			setScale (new Vector3 (0.3f, 0.3f, 0.3f));
			break;
		case 3:
			playerobj = Gameplay.prefabsMgr.generateObjectFromPrefab ("cube7");
			setScale (new Vector3 (0.3f, 0.3f, 0.3f));
			break;
		case 4:
			playerobj = Gameplay.prefabsMgr.generateObjectFromPrefab ("cube7");
			setScale (new Vector3 (0.3f, 0.3f, 0.3f));
			break;
		case 5:
			playerobj = Gameplay.prefabsMgr.generateObjectFromPrefab ("cube6");
			setScale (new Vector3 (0.3f, 0.3f, 0.3f));
			break;
		case 6:
			playerobj = Gameplay.prefabsMgr.generateObjectFromPrefab ("cube5");
			setScale (new Vector3 (0.3f, 0.3f, 0.3f));
			break;
		case 7:
			playerobj = Gameplay.prefabsMgr.generateObjectFromPrefab ("cube2");
			setScale (new Vector3 (0.3f, 0.3f, 0.3f));
			break;
		case 8:
			playerobj = Gameplay.prefabsMgr.generateObjectFromPrefab ("cube8");
			setScale (new Vector3 (0.3f, 0.3f, 0.3f));
			break;
		case 9:
			playerobj = Gameplay.prefabsMgr.generateObjectFromPrefab ("cube6");
			setScale (new Vector3 (0.3f, 0.3f, 0.3f));
			break;
		case 10:
			playerobj = Gameplay.prefabsMgr.generateObjectFromPrefab ("cube1");
			setScale (new Vector3 (0.3f, 0.3f, 0.3f));
			break;
		case 11:
			playerobj = Gameplay.prefabsMgr.generateObjectFromPrefab ("cube6");
			setScale (new Vector3 (0.3f, 0.3f, 0.3f));
			break;
		case 12:
			playerobj = Gameplay.prefabsMgr.generateObjectFromPrefab ("cube3");
			setScale (new Vector3 (0.3f, 0.3f, 0.3f));
			break;
		}

		playerobj.name = "PlayerDynamic";
		playerobj.AddComponent <PlayerController>();
		playerobj.transform.position = new Vector3(0, 3, 0);
		playerobj.transform.rotation = Quaternion.Euler(0, 0, 0);
		playerobj.tag = "Player";
<<<<<<< HEAD
		playerobj.GetComponent<MeshCollider> ().convex = true; //dursun
		playerobj.AddComponent<Rigidbody>();	//dursun
		playerobj.GetComponent<Rigidbody> ().useGravity = false;	//dursun
		playerobj.GetComponent<Rigidbody> ().mass = 0.1f;	//dursun
=======
		//playerobj.AddComponent<BoxCollider> ();
		//playerobj.GetComponent<MeshCollider> ().convex = true; //dursun

		playerobj.AddComponent<MeshRenderer> ().material = Materials.glanz;

>>>>>>> 8a26bf810e1e6cd21c731dff3256658821f24bce

		this.colorCount = colorCount;
		curColor = Col.GRUEN;

		setColor (curColor);

		setGamePosition (new Vector2 (0, 0));


	}
		

	public void changeColorRandom(){
		int i = Random.Range(0, 3);
		setColor (Col.colors [i]);
	}
		
	public void setName(string name){
		playerobj.name = name;
	}

	public void setPosition(Vector3 pos){
		playerobj.transform.position = pos;
	}

	public void setColorCount(int count){
		colorCount = count;
	}

	//sets actual transform position by coordinates
	public void setPositionByGamePosition(Vector2 pos){
		playerobj.transform.position = new Vector3(pos.x, 3, pos.y);
		setGamePosition (pos);
	}

	public void setGamePosition(Vector2 pos){
		gamePosition = pos;
	}

	public void setRotation(Vector3 rot){
		playerobj.transform.rotation = Quaternion.Euler(rot.x, rot.y, rot.z);
	}

	public void setScale(Vector3 scale){
		playerobj.transform.localScale = scale;
	}


	public void setColor(Color col){
		curColor = col;
		playerobj.GetComponent<MeshRenderer> ().material.SetColor("_DiffuseColor", col);
	}

	public void setNextColor(){
		Color nextColor = Col.nextColor (getColor (), colorCount);
		setColor (nextColor);
	}

	public Vector2 getGamePosition(){
		return gamePosition;
	}

	public string getName(){
		return playerobj.name;
	}

	public Vector3 getPosition(){
		return playerobj.transform.position;
	}

	public Quaternion getRotation(){
		return playerobj.transform.rotation;
	}
		
	public Color getColor(){
		return curColor;
	}

	public Transform getTransform(){
		return playerobj.transform;
	}

}