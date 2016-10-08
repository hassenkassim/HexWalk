/************************************************
 * HAMOTO Production 2016						*
 * Project: HexWalk								*
 * Authors: Tolga, Mohamed, Dursun, Hassen		*
 * Year: 2016									*
 *************************************************/

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;

public class MenuManager : MonoBehaviour {

	public Canvas MainMenu;
	public Canvas OptionsMenu;
	public Canvas [] levelCanvas;
	public Canvas worldCanvas;
	public int activeCanvasControll;

	public GameObject SoundOnBtn;
	public GameObject SoundOffBtn;

	public GameObject worldBtn;
	public GameObject[] worldCurrentBtn;
	public GameObject[] worldLockedBtn;

	public bool soundIsOn = true;



	//Initial function
	void Awake(){

		disableAllLevelCanvas ();
		MainMenu.enabled = true;
		OptionsMenu.enabled = false;
		worldCanvas.enabled = false;
		activeCanvasControll = 99;

		if (PlayerPrefs.HasKey ("soundIsOn") == false) {
			SoundOffBtn.gameObject.SetActive (false);
			AudioListener.pause = false;
		
		} else {
			if (GetBool ("soundIsOn") == true) {
				SoundOffBtn.gameObject.SetActive (false);
				AudioListener.pause = false;
			} else {
				SoundOnBtn.gameObject.SetActive (false);
			}
		}

	}

	//Button function
	public void onPlay()
	{
		MainMenu.enabled = false;
		worldCanvas.enabled = true;

	}

	public void onResetPlayerPrefs(){
		PlayerPrefs.DeleteAll ();
	}

	public void onStartGame(){
		SceneManager.LoadScene ("GameScene");
	}



	public void onGameMode()
	{
		
	}

	public void onOptions()
	{
		MainMenu.enabled = false;
		OptionsMenu.enabled = true;
	}

	public void onShare()
	{
		Share.IntentShareText ("This game is awesome! Get the game from play store: balblaLink!");
	}
		
	public void onLeaderboard()
	{
		Social.ShowLeaderboardUI();
	}

	public void onExit()
	{
		Application.Quit ();
	}

	public void onMainMenu()
	{
		MainMenu.enabled = true;
		OptionsMenu.enabled = false;
	}

	public void onSoundOn()
	{
		SoundOnBtn.gameObject.SetActive (false);
		SoundOffBtn.gameObject.SetActive (true);

		SetBool("soundIsOn", false);
		soundIsOn = GetBool ("soundIsOn");

		//PlayerPrefs.Save ();
		Debug.Log(soundIsOn);
		PlayerPrefs.Save();

		AudioListener.pause = true;
	}

	public void onSoundOff()
	{
		SoundOnBtn.gameObject.SetActive (true);
		SoundOffBtn.gameObject.SetActive (false);

		SetBool ("soundIsOn", true);
		soundIsOn = GetBool ("soundIsOn");
		//PlayerPrefs.Save ();
		Debug.Log(soundIsOn);
		PlayerPrefs.Save();

		AudioListener.pause = false;
	}

	public void onNextLevel(){
		
	}

	public void onWorld1(){
		worldCanvas.enabled = false;
		levelCanvas[0].gameObject.SetActive(true);
		activeCanvasControll = 0;
	}

	public void onWorld2(){
		worldCanvas.enabled = false;
		levelCanvas[1].gameObject.SetActive(true);
		activeCanvasControll = 1;
	}

	public void onWorld3(){
		worldCanvas.enabled = false;
		levelCanvas[2].gameObject.SetActive(true);
		activeCanvasControll = 2;
	}

	public void onWorld4(){
		worldCanvas.enabled = false;
		levelCanvas[3].gameObject.SetActive(true);
		activeCanvasControll = 3;
	}

	public void onWorld5(){
		worldCanvas.enabled = false;
		levelCanvas[4].gameObject.SetActive(true);
		activeCanvasControll = 4;
	}

	public void onWorld6(){
		worldCanvas.enabled = false;
		levelCanvas[5].gameObject.SetActive(true);
		activeCanvasControll = 5;
	}

	public void onWorld7(){
		worldCanvas.enabled = false;
		levelCanvas[6].enabled = true;
		activeCanvasControll = 6;
	}

	public void onWorld8(){
		worldCanvas.enabled = false;
		levelCanvas[7].gameObject.SetActive(true);
		activeCanvasControll = 7;
	}

	public void onWorld9(){
		worldCanvas.enabled = false;
		levelCanvas[8].gameObject.SetActive(true);
		activeCanvasControll = 8;
	}

	public void onWorld10(){
		worldCanvas.enabled = false;
		levelCanvas[9].gameObject.SetActive(true);
		activeCanvasControll = 9;
	}
		
	public void onBackWorldCanvas(){
		worldCanvas.enabled = false;
		MainMenu.enabled = true;
	}

	public void onBackLevelCanvas (){
		disableActiveLevelCanvas ();
		worldCanvas.enabled = true;
	}

	//canvasControll und initialize
	public void disableAllLevelCanvas(){
		for (int i = 0; i < 10; i++) {
			 {
				levelCanvas [i] = GameObject.Find ("levelCanvas" + (i + 1)).GetComponent<Canvas> ();
				levelCanvas [i].gameObject.SetActive (false);

			}
		}
	}

	public void disableActiveLevelCanvas(){
			levelCanvas [activeCanvasControll].gameObject.SetActive (false);
	}
		


	// Boolean for PlayerPrefs
	public static void SetBool(string name, bool booleanValue) 
	{
		PlayerPrefs.SetInt(name, booleanValue ? 1 : 0);
	}

	public static bool GetBool(string name)  
	{
		return PlayerPrefs.GetInt(name) == 1 ? true : false;
	}

	public static bool GetBool(string name, bool defaultValue)
	{
		if(PlayerPrefs.HasKey(name)) 
		{
			return GetBool(name);
		}

			return defaultValue;
	}

	//Leaderboard function
	ILeaderboard m_Leaderboard;

	void DoLeaderboard () {
		m_Leaderboard = Social.CreateLeaderboard();
		m_Leaderboard.id = "Leaderboard01";
		m_Leaderboard.LoadScores(result => DidLoadLeaderboard(result));
	}

	void DidLoadLeaderboard (bool result) {
		Debug.Log("Received " + m_Leaderboard.scores.Length + " scores");
		foreach (IScore score in m_Leaderboard.scores)
			Debug.Log(score);
	}

}
