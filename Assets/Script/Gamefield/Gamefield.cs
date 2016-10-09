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
	public Field[,] fields;
	public int width;
	public int height;


	public Gamefield(int w, int h, int version){
		width = w;
		height = h;
		fields = new Field[width, height];
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				fields [i,j] = new Field (i + "_" + j, version );
				fields [i, j].setPosition (i, j);
			}
		}
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