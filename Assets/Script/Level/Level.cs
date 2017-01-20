using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/*
 * Verwaltet die Eigenschaften von einem Level
 * */

public class Level {

	int world;
	int level;
	int height;
	int width;
	int colorCount;

	int completed;

	public Level(int height, int width, int colorCount, int world, int level){
		this.height = height;
		this.width = width;
		this.colorCount = colorCount;
		this.world = world;
		this.level = level;
	
		//load completed Value from Pref with the help of world and level
		completed = PlayerPrefs.GetInt(LevelManager.COMPLETEDPREF + LevelManager.WORLDPREF + world + LevelManager.LEVELPREF + level, 0); 
	}

	public int getHeight(){
		return height;
	}

	public int getWidth(){
		return width;
	}

	public int getColorCount(){
		return colorCount;
	}

	public int getCompleted(){
		return completed;
	}

	public void setCompleted(){
		completed = 1;
		PlayerPrefs.SetInt (LevelManager.COMPLETEDPREF + LevelManager.WORLDPREF + world + LevelManager.LEVELPREF + level, 1);
	}

	public static void setAllLevelCompleted(){
		for (int i = 0; i < 12; i++) {
			for (int j = 0; j < 12; j++) {
				PlayerPrefs.SetInt (LevelManager.COMPLETEDPREF + LevelManager.WORLDPREF + i + LevelManager.LEVELPREF + j, 1);
			}
		}

	}

	public int getWorld(){
		return world;
	}

	public int getLevel(){
		return level;
	}


}
