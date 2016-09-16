using UnityEngine;
using System.Collections;

public class Gamefield {
	Field[,] fields;

	public Gamefield(int width, int height){
		fields = new Field[width, height];
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				fields [i,j] = new Field ("" + i + "_" + j);
				fields [i, j].changePosition (i, j);
			}
		}
	}

	public Field getField(int row, int column){
		return fields[row,column];
	}


}