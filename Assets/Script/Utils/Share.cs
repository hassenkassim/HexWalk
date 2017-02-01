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
			
		} 

	}

	#if UNITY_IOS
		public struct ConfigStruct
		{
			public string title;
			public string message;
		}

		[DllImport ("__Internal")] private static extern void showAlertMessage(ref ConfigStruct conf);

		public struct SocialSharingStruct
		{
			public string text;
			public string url;
			public string image;
			public string subject;
		}

		[DllImport ("__Internal")] private static extern void showSocialSharing(ref SocialSharingStruct conf);

		public static void CallSocialShare(string title, string message)
		{
			ConfigStruct conf = new ConfigStruct();
			conf.title  = title;
			conf.message = message;
			showAlertMessage(ref conf);
		}

		public static void CallSocialShareAdvanced(string defaultTxt, string subject, string url, string img)
		{
			SocialSharingStruct conf = new SocialSharingStruct();
			conf.text = defaultTxt; 
			conf.url = url;
			conf.image = img;
			conf.subject = subject;

			showSocialSharing(ref conf);
		}
	#endif


}
