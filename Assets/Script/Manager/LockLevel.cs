using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LockLevel : MonoBehaviour {

	public Button []worldBtn;

	public GameObject[] worldBtnObj;
	public Sprite worldImage;
	public Sprite worldLockedImage;
	public Sprite worldCurrentImage;

	public void  Start (){
		//worldImage = Resources.Load<Sprite>("WorldImage");
		//worldLockedImage = Resources.Load<Sprite>("WorldLockedImage");
		//worldCurrentImage = Resources.Load<Sprite>("WorldLockedImage");

		LockLevels();   //call function LockLevels
	}

	//function to lock the levels
	public void LockLevels ()
	{
		PlayerPrefs.SetInt ("world", 3);
		if (PlayerPrefs.HasKey ("world") == false || PlayerPrefs.HasKey ("level") == false) {
			PlayerPrefs.SetInt ("world", 1);
			PlayerPrefs.SetInt ("level", 1);
		}
			
		//loop from curworld/level to worldmax/levelmax
		for (int i = PlayerPrefs.GetInt ("world"); i < LevelManager.worldMax + 1; i++) {
			for (int j = PlayerPrefs.GetInt ("level"); j < LevelManager.levelMax + 1; j++) {
				worldBtnObj [i] = GameObject.Find ("WorldLocked " + (i + 1));
				worldBtn [i] = worldBtnObj [i].GetComponent<Button>();
				worldBtn[i].image.overrideSprite = worldLockedImage;	
				worldBtn [PlayerPrefs.GetInt ("world") - 1].image.overrideSprite = worldCurrentImage;
				worldBtn[i].GetComponent<Button>().interactable = false;



			}
		}

		for (int a = 0; a < PlayerPrefs.GetInt ("world"); a++) {
			for (int b = 1; b < PlayerPrefs.GetInt ("level") + 1; b++) {
				worldBtnObj [a] = GameObject.Find ("WorldLocked " + (a + 1));
				worldBtn [a] = worldBtnObj [a].GetComponent<Button>();
				worldBtn [a].image.overrideSprite = worldImage;
				worldBtn[a].GetComponent<Button>().interactable = true;
			}
		}

	}
}

