using System.Collections;
using System.IO;
using System;
using UnityEngine;



public class FileUtils {

	public static Level[,] loadLevelsFromFile(string filepath, int worldAnzahl, int levelAnzahl){
		Level[,] levels = new Level[worldAnzahl, levelAnzahl];

		TextAsset file = Resources.Load("levelListe") as TextAsset;
		string[] lines = file.text.Split(new [] { '\r', '\n' });
		for (int i = 0; i < lines.Length; i++) {
			string[] line = lines [i].Split (';');
			int world = Convert.ToInt16(line[0]) - 1;
			int level = Convert.ToInt16(line[1]) - 1;
			Level l = new Level(Convert.ToInt16(line[3]), Convert.ToInt16(line[2]), Convert.ToInt16(line[4]), world, level);
			levels[world, level] = l;
		}
		return levels;
	}
}
