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
			playerobj = GameObject.CreatePrimitive (PrimitiveType.Cube);
			setScale (new Vector3 (0.5f, 0.5f, 0.5f));
			break;
		case 1:
			playerobj = Gameplay.prefabsMgr.generateObjectFromPrefab ("cubeEckig");
			setScale (new Vector3 (0.3f, 0.3f, 0.3f));
			break;
		}

		playerobj.name = "PlayerDynamic";
		playerobj.AddComponent <PlayerController>();
		playerobj.transform.position = new Vector3(0, 3, 0);
		playerobj.transform.rotation = Quaternion.Euler(0, 0, 0);
		playerobj.tag = "Player";

		this.colorCount = colorCount;
		curColor = Col.GRUEN;
		setColor (curColor);

		setGamePosition (new Vector2 (0, 0));

		Rigidbody playerRigidBody = playerobj.AddComponent<Rigidbody>(); // Add the rigidbody
		playerRigidBody.mass = 0.5f;
		playerRigidBody.angularDrag = 0.05f;
		playerRigidBody.useGravity = true;


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
		playerobj.GetComponent<MeshRenderer> ().material.color = col;
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