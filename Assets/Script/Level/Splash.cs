using UnityEngine;
using System.Collections;

public class Splash : MonoBehaviour {

	private int splashShown;

	// Use this for initialization
	void Start () {
		splashShown = 0;
	}

	public int getSplashShown(){
		return splashShown;
	}

	public void setSplashShown(int splashShown){
		this.splashShown = splashShown;
	}
}
