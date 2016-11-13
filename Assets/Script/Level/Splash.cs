using UnityEngine;
using System.Collections;

public class Splash : MonoBehaviour {

	private int splashShown;

	// Use this for initialization
	void Start () {
		splashShown = 0;
	}
		
	void OnCollisionEnter(Collision coll)
	{
		if (coll.collider.gameObject.name=="PlayerDynamic") {
			LevelPlay.gameName.SetActive (false);
			LevelPlay.gameNameDestroyed.transform.position = new Vector3 (-0.2f, 1.75f, 0.0f);
			
			LevelPlay.gameNameDestroyed.SetActive (true);
			explosionSplash ();
		}
	}
		
	public int getSplashShown(){
		return splashShown;
	}

	public void setSplashShown(int splashShown){
		this.splashShown = splashShown;
	}

	// dursun 
	static void explosionSplash(){
		LevelPlay.gameName.SetActive (false);
		LevelPlay.gameNameDestroyed.SetActive (true);
		LevelPlayerController.explode = true;
	}

	public static void destroyGameNames(){
		Destroy(LevelPlay.gameNameDestroyed);
		Destroy (LevelPlay.gameName);
	}
}
