using UnityEngine;
using System.Collections;

public class SavePlayerPrefs {
	public SavePlayerPrefs(){
	}

	public void saveStarsPerLevel(int lvl, int stars){
		PlayerPrefs.SetInt ("level"+lvl, stars );
		PlayerPrefs.Save ();
	}


}
