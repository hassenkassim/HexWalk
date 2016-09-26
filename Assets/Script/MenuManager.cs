using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;

public class MenuManager : MonoBehaviour {

	public Canvas MainMenu;
	public Canvas OptionsMenu;

	public GameObject SoundOnBtn;
	public GameObject SoundOffBtn;

	public bool soundIsOn = true;


	//Initial function
	void Awake(){


		MainMenu.enabled = true;
		OptionsMenu.enabled = false;

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
	public void onStartGame()
	{
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
