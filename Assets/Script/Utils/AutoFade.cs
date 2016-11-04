/*
	FadeObjectInOut.cs
 	Hayden Scott-Baron (Dock) - http://starfruitgames.com
 	6 Dec 2012 
 
	This allows you to easily fade an object and its children. 
	If an object is already partially faded it will continue from there. 
	If you choose a different speed, it will use the new speed. 
 
	NOTE: Requires materials with a shader that allows transparency through color.  
*/

using UnityEngine;
﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class AutoFade : MonoBehaviour
{

	// publically editable speed
	public static float fadeDelay = 0.0f; 
	public static float fadeTime = 0.5f; 
	public static bool fadeInOnStart = false; 
	public static bool fadeOutOnStart = false;
	private static bool logInitialFadeSequence = false; 


	// store colours
	private static Color[] colors; 

	// allow automatic fading on the start of the scene
	IEnumerator Start ()
	{
		//yield return null; 
		yield return new WaitForSeconds (fadeDelay); 

		if (fadeInOnStart)
		{
			logInitialFadeSequence = true; 
			FadeIn (); 
		}

		if (fadeOutOnStart)
		{
			FadeOut (fadeTime); 
		}
	}




	// check the alpha value of most opaque object
	public float MaxAlpha()
	{
		float maxAlpha = 0.0f; 
		Renderer[] rendererObjects = GetComponentsInChildren<Renderer>(); 
		foreach (Renderer item in rendererObjects)
		{
			maxAlpha = Mathf.Max (maxAlpha, item.material.color.a); 
		}
		return maxAlpha; 
	}

	// fade sequence
	IEnumerator FadeSequence (float fadingOutTime)
	{
		// log fading direction, then precalculate fading speed as a multiplier
		bool fadingOut = (fadingOutTime < 0.0f);
		float fadingOutSpeed = 1.0f / fadingOutTime; 

		// grab all child objects
		Renderer[] rendererObjects = GetComponentsInChildren<Renderer>(); 
		if (colors == null)
		{
			//create a cache of colors if necessary
			colors = new Color[rendererObjects.Length]; 

			// store the original colours for all child objects
			for (int i = 0; i < rendererObjects.Length; i++)
			{
				colors[i] = rendererObjects[i].material.color; 
			}
		}

		// make all objects visible
		for (int i = 0; i < rendererObjects.Length; i++)
		{
			rendererObjects[i].enabled = true;
		}


		// get current max alpha
		float alphaValue = MaxAlpha();  


		// This is a special case for objects that are set to fade in on start. 
		// it will treat them as alpha 0, despite them not being so. 
		if (logInitialFadeSequence && !fadingOut)
		{
			alphaValue = 0.0f; 
			logInitialFadeSequence = false; 
		}

		// iterate to change alpha value 
		while ( (alphaValue >= 0.0f && fadingOut) || (alphaValue <= 1.0f && !fadingOut)) 
		{
			alphaValue += Time.deltaTime * fadingOutSpeed; 

			for (int i = 0; i < rendererObjects.Length; i++)
			{
				Color newColor = (colors != null ? colors[i] : rendererObjects[i].material.color);
				newColor.a = Mathf.Min ( newColor.a, alphaValue ); 
				newColor.a = Mathf.Clamp (newColor.a, 0.0f, 1.0f); 				
				rendererObjects[i].material.SetColor("_Color", newColor) ; 
			}

			yield return null; 
		}

		// turn objects off after fading out
		if (fadingOut)
		{
			for (int i = 0; i < rendererObjects.Length; i++)
			{
				rendererObjects[i].enabled = false; 
			}
		}


		Debug.Log ("fade sequence end : " + fadingOut); 

	}


	void FadeIn ()
	{
		FadeIn (fadeTime); 
	}

	void FadeOut ()
	{
		FadeOut (fadeTime); 		
	}

	void FadeIn (float newFadeTime)
	{
		StopAllCoroutines(); 
		StartCoroutine("FadeSequence", newFadeTime); 
	}

	void FadeOut (float newFadeTime)
	{
		StopAllCoroutines(); 
		StartCoroutine("FadeSequence", -newFadeTime); 
	}


	// These are for testing only. 
	//		void Update()
	//		{
	//			if (Input.GetKeyDown (KeyCode.Alpha0) )
	//			{
	//				FadeIn();
	//			}
	//			if (Input.GetKeyDown (KeyCode.Alpha9) )
	//			{
	//				FadeOut(); 
	//			}
	//		}


}

/*
public class AutoFade{

	//public static GameObject tmp;
	public static float fadeTime=0.5f; 
	public static PrefabsManagerLevelPlay prefabsMgr;
	public  GUITexture overlay;
	public bool fadeIn;



	public AutoFade(GameObject overlay1)
	{
		this.fadeIn = false;
		this.overlay=overlay1.GetComponent<GUITexture> ();
	}


	public IEnumerator FadeIn(){
		//fade to black:
		overlay.gameObject.SetActive (true);
		overlay.color = Color.clear;
		InputManager.active=false;

		float rate = 1.5f/fadeTime;
		float progress = 0.0f;
		while (progress<1.0f) {
			overlay.color = Color.Lerp (Color.clear, Color.black, progress);
			progress += rate * Time.deltaTime;
			yield return null;
		}
		overlay.color = Color.black;

		// change the world here ....
		yield return null;
		LevelPlay.changeWorld = true;

		//fade to clear:
		this.overlay.color = Color.black;

		progress = 0.0f;
		while (progress<1.0f) {
			this.overlay.color = Color.Lerp (Color.black, Color.clear, progress);
			progress += rate * Time.deltaTime;
			yield return null;
		}
		this.overlay.color = Color.clear;
		this.overlay.gameObject.SetActive (false);
		LevelPlay.fading.fadeIn = false;
		InputManager.active=true;

	}
}   
*/
