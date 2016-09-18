using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;

public class MenuManager : MonoBehaviour {

	public Canvas MainMenu;
	public Canvas OptionsMenu;

	public GameObject SoundOnBtn;
	public GameObject SoundOffBtn;

	public bool soundIsOn = true;


	//INITIAL FUNCTION
	//INITIAL FUNCTION
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
	//INITIAL FUNCTION
	//INITIAL FUNCTION



	//BUTTON FUNCTION
	//BUTTON FUNCTION
	public void onStartGame()
	{
		Application.LoadLevel ("GameScene");
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
		int score = 1;
		IntentShareText ("This game is awesome! I scored" + score.ToString() + ". Get the game from play store: balblaLink!");
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
	//BUTTON FUNCTION
	//BUTTON FUNCTION


	// SHARE FUNCTION (for ANDROID!!!!!)
	// SHARE FUNCTION (for ANDROID!!!!!)
	static AndroidJavaClass IntentClass;
	static AndroidJavaObject sendIntent;
	static AndroidJavaClass UnityPlayer;
	static AndroidJavaObject currentActivity;


	static bool IsInitialized = false;

	static void Initialize(){
		if (IsInitialized)
			return;
		IsInitialized = true;

		string className = "android.content.Intent";
		IntentClass = new AndroidJavaClass (className);
		sendIntent = new AndroidJavaObject (className);

		UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"); 
		// jni.FindClass("com.unity3d.player.UnityPlayer"); 
		currentActivity = UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity"); 
	}

	public static void IntentShareText(string text){

		if (Application.platform != RuntimePlatform.Android)
			return;
		
		//#if UNITY_ANDROID


		if (!IsInitialized)
			Initialize ();

		sendIntent.Call<AndroidJavaObject> ("setAction", IntentClass.GetStatic<string> ("ACTION_SEND"));
		sendIntent.Call<AndroidJavaObject> ("putExtra", IntentClass.GetStatic<string> ("EXTRA_TEXT"), text);
		sendIntent.Call<AndroidJavaObject> ("setType", "text/plain");


		currentActivity.Call("startActivity", sendIntent);
	}
	// SHARE FUNCTION (FOR ANDROID!!!!!)
	// SHARE FUNCTION (FOR ANDROID!!!!!)



	// BOOLEAN FOR PLAYERPREFS
	// BOOLEAN FOR PLAYERPREFS
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
	// BOOLEAN FOR PLAYERPREFS
	// BOOLEAN FOR PLAYERPREFS



	//LEADERBOARD FUNCTION
	//LEADERBOARD FUNCTION
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
	//LEADERBOARD FUNCTION
	//LEADERBOARD FUNCTION
}
