/************************************************
 * HAMOTO Production 2016						*
 * Project: HexWalk								*
 * Authors: Tolga, Mohamed, Dursun, Hassen		*
 * Year: 2016									*
 *************************************************/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UnlockWorld : MonoBehaviour {

	public Button worldBtn;
	public GameObject worldBtnObj;

	public Sprite worldFinishedImage;
	public Sprite worldLockedImage;
	public Sprite worldCurrentImage;

	public void  Start (){
		
		UnlockWorlds(); 

	}

	//function to lock the levels
	public void UnlockWorlds (){
		PlayerPrefs.SetInt ("world", 1);
		if (PlayerPrefs.HasKey ("world") == false) {
			PlayerPrefs.SetInt ("world", 1);
	
		}
			
		worldBtnObj = GameObject.Find ("World" +PlayerPrefs.GetInt("world"));
		worldBtn = worldBtnObj.GetComponent<Button>();
		worldBtn.image.overrideSprite = worldCurrentImage;
		worldBtn.GetComponent<Button>().interactable = true;


		for (int a = 1; a < PlayerPrefs.GetInt("world"); a++) {
			
				worldBtnObj = GameObject.Find ("World"+a);
				worldBtn = worldBtnObj.GetComponent<Button>();
				worldBtn.image.overrideSprite = worldFinishedImage;
				worldBtn.GetComponent<Button>().interactable = true;
	
		}


	}


}

