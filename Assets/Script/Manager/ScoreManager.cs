using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ScoreManager {
	int score;
	Text scoreText;

	public ScoreManager(){
		score = 0;
		scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
	}

	public int getScore(){
		return score;
	}

	public void setScore(int i){
		score = i;
		displayScore ();
	}

	public void incScore(){
		setScore(++score);
	}
		
	public void displayScore(){
		//Debug.Log (score.ToString ());
		scoreText.text = "Score: " + score.ToString ();
	}

}
