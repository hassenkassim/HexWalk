using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

	//Gameover Canvas
	public Canvas gameOverCanvas;
	public static Text highscoreText;
	public static Text scoreText;

	// Use this for initialization
	void Start () {
		scoreText = GameObject.Find("Score1Text").GetComponent<Text>();
		highscoreText = GameObject.Find("HighscoreText").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static void displayGameover(){
		if (PlayerPrefs.HasKey ("highscore") == false) {
			PlayerPrefs.SetInt ("highscore", 0);
		}

		if(PlayerController.getScore() > PlayerPrefs.GetInt("highscore")){
			PlayerPrefs.SetInt ("highscore", PlayerController.getScore ());
		}

		scoreText.text = "Score: " + PlayerController.getScore().ToString ();
		highscoreText.text = "Highscore: " + PlayerPrefs.GetInt("highscore").ToString ();
	
	}
			
}
