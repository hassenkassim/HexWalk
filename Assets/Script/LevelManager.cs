using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public static int initialWidth;
	public static int initialHeight;

	// Use this for initialization
	void Start () {
		//Create Gamefield
		initialWidth = 4;
		initialHeight = 5;


		if (PlayerPrefs.HasKey ("gameFieldWidth") == false) {
			PlayerPrefs.SetInt ("gameFieldWidth", initialWidth);
		}

		if (PlayerPrefs.HasKey ("gameFieldHeight") == false) {
			PlayerPrefs.SetInt ("gameFieldHeight", initialHeight);
		}
	}

	//Setting difficulty grade up and loading a new Scene
	public static void levelUp(){

		PlayerPrefs.SetInt ("gameFieldWidth", PlayerPrefs.GetInt("gameFieldWidth") + 1);
		PlayerPrefs.SetInt ("gameFieldHeight", PlayerPrefs.GetInt("gameFieldHeight") + 1);
		SceneManager.LoadScene ("GameScene");
	}

	//Setting difficulty grade down
	public static void levelReset(){

		PlayerPrefs.SetInt ("gameFieldWidth", initialWidth);
		PlayerPrefs.SetInt ("gameFieldHeight", initialHeight);
	}



	
	// Update is called once per frame
	void Update () {
	
	}
}
