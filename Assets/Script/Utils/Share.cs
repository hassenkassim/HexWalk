/************************************************
 * HAMOTO Production 2016						*
 * Project: HexWalk								*
 * Authors: Tolga, Mohamed, Dursun, Hassen		*
 * Year: 2016									*
 *************************************************/

using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms;

public class Share : MonoBehaviour {

	// share function (for ANDROID!!!!!)
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

		if (Application.platform == RuntimePlatform.Android) {
			if (!IsInitialized)
				Initialize ();

			sendIntent.Call<AndroidJavaObject> ("setAction", IntentClass.GetStatic<string> ("ACTION_SEND"));
			sendIntent.Call<AndroidJavaObject> ("putExtra", IntentClass.GetStatic<string> ("EXTRA_TEXT"), text);
			sendIntent.Call<AndroidJavaObject> ("setType", "text/plain");
			currentActivity.Call("startActivity", sendIntent);

		} else if (Application.platform == RuntimePlatform.IPhonePlayer) {
			Application.OpenURL ("itms-apps:itunes.apple.com/"); //TODO: Verlinken
			//Application.OpenURL ("itms-apps:itunes.apple.com/app/crossy-road-endless-arcade/id924373886");
		} 

	}


}
