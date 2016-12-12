using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/*
 * Wrapper Class for Scene management
 * */

public class ScenesManager : MonoBehaviour {
	public const string SCENE_SPLASH = "SplashLoadingScene";
	public const string SCENE_LEVEL = "LevelScene";
	public const string SCENE_LEVEL2 = "Scene/LevelScene";
	public const string SCENE_GAME = "GameScene";


	public void loadScene(string name){
		SceneManager.LoadScene (name);
	}

	public static string getCurrentSceneName(){
		return SceneManager.GetActiveScene ().name;
	}
}
