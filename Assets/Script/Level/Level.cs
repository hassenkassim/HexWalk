using UnityEngine;


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
	int stars;

	public Level(int height, int width, int colorCount, int world, int level){
		this.height = height;
		this.width = width;
		this.colorCount = colorCount;
		this.world = world;
		this.level = level;
	
		//load completed Value from Pref with the help of world and level
		completed = PlayerPrefs.GetInt(LevelManager.COMPLETEDPREF + LevelManager.WORLDPREF + world + LevelManager.LEVELPREF + level, 0); 
		stars = PlayerPrefs.GetInt(LevelManager.STARSPREF + LevelManager.WORLDPREF + world + LevelManager.LEVELPREF + level, 0); 
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
}
