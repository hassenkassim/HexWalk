/************************************************
 * HAMOTO Production 2016						*
 * Project: HexWalk								*
 * Authors: Tolga, Mohamed, Dursun, Hassen		*
 * Year: 2016									*
 *************************************************/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

	public static Text highscoreText;
	public static Text scoreText;
	public static Canvas gameOverCanvas;
	public static int highscore;


	// Use this for initialization
	void Awake(){
		scoreText = GameObject.Find("Score1Text").GetComponent<Text>();
		highscoreText = GameObject.Find ("HighscoreText").GetComponent<Text> ();
		GameObject tempObject = GameObject.Find("GameOverCanvas");
		if(tempObject != null){
			//If we found the object , get the Canvas component from it.
			gameOverCanvas = tempObject.GetComponent<Canvas>();
			if(gameOverCanvas == null){
				Debug.Log("Could not locate Canvas component on " + tempObject.name);
			}
		}
		gameOverCanvas.enabled = false;


	}

	public static void gameover(){
		
	}



	public static void displayGameover(){

		/*gameOverCanvas.enabled = true;

		Time.timeScale = 0;

		if (PlayerPrefs.HasKey ("highscore") == false) {
			PlayerPrefs.SetInt ("highscore", 0);
		}

		if(PlayerController.getScore() > highscore){
			PlayerPrefs.SetInt ("highscore", PlayerController.getScore ());
			highscore = PlayerController.getScore();
		}

		highscore = PlayerPrefs.GetInt ("highscore");

		scoreText.text = "Score: " + PlayerController.getScore ().ToString ();
		highscoreText.text = "Highscore: " + highscore.ToString ();

		LevelManager.levelReset ();
	*/
	}

	public void onShare(){
//		Share.IntentShareText ("This game is awesome! " + PlayerController.getScore ().ToString() + ". Get the game from play store: balblaLink!");
		
	}
				
}
