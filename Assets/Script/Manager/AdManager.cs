﻿using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour {

	public static string adID ="videoAd";

	public static void showAd(){
		
		if(Advertisement.IsReady ()){
			/*InputManager.active = false;
			Advertisement.Show (adID, new ShowOptions(){resultCallback = handleAdResult});*/
		}


	}

	/*private static void handleAdResult(ShowResult result){
	/*	switch (result) {
		case ShowResult.Finished:
			Debug.Log ("Ad was finished!");
			InputManager.active = true;
			break;
		case ShowResult.Skipped:
			Debug.Log ("Ad was skipped!");
			InputManager.active = true;
			break;
		case ShowResult.Failed:
			Debug.Log ("Ad failed!");
			InputManager.active = true;
			break;
		}
	}*/


}
