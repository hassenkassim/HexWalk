using System.Collections;
using System.IO;
using System;
using UnityEngine;



public class FileUtils {

	public static Level[,] loadLevelsFromFile(string filepath, int worldAnzahl, int levelAnzahl){


		Level[, ] levels = new Level[worldAnzahl, levelAnzahl];

		try
		{ 
			// Open the text file using a stream reader.
			StreamReader sr = new StreamReader(Application.dataPath + "/Resources/levelListe.csv");

			String line;
			String[] lineSplit;

			while((line = sr.ReadLine()) != null){
				lineSplit = line.Split (';');

				int world = Convert.ToInt16(lineSplit[0]) - 1;
				int level = Convert.ToInt16(lineSplit[1]) - 1;
				Level l = new Level(Convert.ToInt16(lineSplit[2]), Convert.ToInt16(lineSplit[3]), Convert.ToInt16(lineSplit[4]), world, level);
				levels[world, level] = l;
			}
		}
		catch (Exception e)
		{
			Console.WriteLine("The file could not be read:");
			Console.WriteLine(e.Message);
		}
		return levels;
	}
}
