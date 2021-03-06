﻿using UnityEngine;
using System.Collections;
#if (UNITY_ANDROID || UNITY_IPHONE) && !UNITY_EDITOR && !UNITY_STANDALONE

	using UnityEngine.Advertisements;

	public class AdManager : MonoBehaviour {

	public static string videoID = "video";
	public static string rewardedVideoID = "rewardedVideo";
	public static int adFrequence = 5;
	public static GameObject adManager;
	public static int loseCounter = 0;

	public void Start(){
		
		adManager = GameObject.Find ("AdManager");
		DontDestroyOnLoad (adManager);

	}

	public static void showVideo(){
		
		if (loseCounter % adFrequence == 0) {
			
			if (Advertisement.IsReady ()) {
				
				InputManager.active = false;
				Advertisement.Show (videoID, new ShowOptions (){ resultCallback = handleAdResult });

			}
		}

	}

	public static void showRewardedVideo(){

		if (Advertisement.IsReady ()) {

			InputManager.active = false;
			Advertisement.Show (rewardedVideoID, new ShowOptions (){ resultCallback = handleAdResult });

		}
	}

	private static void handleAdResult(ShowResult result){
		switch (result) {
		case ShowResult.Finished:
			//Debug.Log ("Ad was finished!");
			InputManager.active = true;
			break;
		case ShowResult.Skipped:
			//Debug.Log ("Ad was skipped!");
			InputManager.active = true;
			break;
		case ShowResult.Failed:
			//Debug.Log ("Ad failed!");
			InputManager.active = true;
			break;
		}
	}



}
#elif UNITY_EDITOR || UNITY_STANDALONE

	public class AdManager : MonoBehaviour {
		public static string videoID = "video";
		public static string rewardedVideoID = "rewardedVideo";
		public static int adFrequence = 5;
		public static GameObject adManager;
		public static int loseCounter = 0;

		public void Start(){

		}

		public static void showVideo(){

		}

		public static void showRewardedVideo(){

		}
	}
#endif