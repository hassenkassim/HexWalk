using UnityEngine;
using System.Collections;


/*
 * This class organizes the gamefield
 * */
public class Gamefield {
	public static Field[,] fields;
	public static int width;
	public static int height;


	public Gamefield(int w, int h){
		width = w;
		height = h;
		fields = new Field[width, height];
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				fields [i,j] = new Field ("" + i + "_" + j);
				fields [i, j].setPosition (i, j);
			}
		}
	}

	public Field getField(int row, int column){
		return fields[row,column];
	}

}