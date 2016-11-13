/************************************************
 * HAMOTO Production 2016						*
 * Project: HexWalk								*
 * Authors: Tolga, Mohamed, Dursun, Hassen		*
 * Year: 2016									*
 *************************************************/

using UnityEngine;
using System.Collections;



/*
 * This class organizes the gamefield
 * */
public class Gamefield {

	public const int SLIDEFROMBOTTOM= 0;
	public const int SLIDEFROMUP = 1;
	public const int SLIDEFROMBACK = 2;

	public Field[,] fields;
	public int width;
	public int height;

	Vector3 oldPositionOffset;
	Vector3 newPositionOffset;


	public Gamefield(int w, int h, int version){
		width = w;
		height = h;
		fields = new Field[width, height];
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				fields [i,j] = new Field (i + "_" + j, version);
				fields [i, j].setPosition (i, j);
			}
		}
	}

	public Gamefield(int w, int h, int version, int offsetY, int slideVersion){
		width = w;
		height = h;
		fields = new Field[width, height];
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				fields [i,j] = new Field (i + "_" + (j + offsetY), version);
			}
		}

		//set location properly (depending on slideVersion)

		if (slideVersion == SLIDEFROMBOTTOM) {
			oldPositionOffset = new Vector3 (0, (0 + offsetY), -10);
		} else if(slideVersion == SLIDEFROMUP){
			oldPositionOffset = new Vector3 (0, (0 + offsetY), 10);
		} else if(slideVersion == SLIDEFROMBACK){
			oldPositionOffset = new Vector3 (0, (0 + offsetY + 10), 1);
		}
		newPositionOffset = new Vector3 (0, (0 + offsetY), 1);


		GamefieldController.fields = fields;
		GamefieldController.width = width;
		GamefieldController.height = height;
		GamefieldController.oldPositionOffset = oldPositionOffset;
		GamefieldController.newPositionOffset = newPositionOffset;
		//start animation by setting the active variable to true
		GamefieldController.transitionActive = true;

	}

	public Field getField(int row, int column){
		return fields[row,column];
	}

	public int getFieldWidth(){
		return width;
	}

	public int getFieldHeight(){
		return height;
	}


}