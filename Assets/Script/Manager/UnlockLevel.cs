/************************************************
 * HAMOTO Production 2016						*
 * Project: HexWalk								*
 * Authors: Tolga, Mohamed, Dursun, Hassen		*
 * Year: 2016									*
 *************************************************/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UnlockLevel : MonoBehaviour {

	public Button levelBtn;
	public GameObject levelBtnObj;

	public Sprite levelFinishedImage;
	public Sprite levelLockedImage;
	public Sprite levelCurrentImage;



	// Use this for initialization
	void Awake () {
		UnlockLevels ();

	}
	
	public void UnlockLevels(){
		if (PlayerPrefs.HasKey ("level") == false) {
			PlayerPrefs.SetInt ("level", 1);

		} 

		levelBtnObj = GameObject.Find ("Level"+PlayerPrefs.GetInt("world")+"_"+PlayerPrefs.GetInt("level"));
		levelBtn = levelBtnObj.GetComponent<Button>();
		levelBtn.image.overrideSprite = levelCurrentImage;
		levelBtn.GetComponent<Button>().interactable = true;

		for (int a = 1; a < PlayerPrefs.GetInt("level"); a++) {

			levelBtnObj = GameObject.Find ("Level"+PlayerPrefs.GetInt ("world")+"_"+a.ToString());
			levelBtn = levelBtnObj.GetComponent<Button> ();
			levelBtn.image.overrideSprite = levelFinishedImage;
			levelBtn.GetComponent<Button> ().interactable = true;

		}

		if (LevelManager.worldUp == true) {
			levelBtnObj = GameObject.Find ("Level"+PlayerPrefs.GetInt("world")+"_"+LevelManager.levelMax);
			levelBtn = levelBtnObj.GetComponent<Button>();
			levelBtn.image.overrideSprite = levelFinishedImage;

			PlayerPrefs.SetInt("level", 1);
		}

		PlayerPrefs.Save();

	}
}
