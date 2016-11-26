using UnityEngine;
using System.Collections;

public class Splash : MonoBehaviour {

	private int splashShown=0; // 0= showSplash; 1= endSplash; 2= nothing
	public static Vector2 pos;
	public float splashOffset = 8.2f;
	// Use this for initialization
	void Start () {
		splashShown = 0;
		pos= new Vector2(PlayerPrefs.GetInt(LevelManager.NEXTLEVEL,0), PlayerPrefs.GetInt(LevelManager.NEXTWORLD,0));
	}

	void Update (){

	}

	public int getSplashShown(){
		return splashShown;
	}

	public void setSplashShown(int splashShown){
		this.splashShown = splashShown;
	}


	public void initSplash(){
		//dursun
		SplashLoad.playerobj.GetComponent<Rigidbody>().useGravity=false;


		SplashLoad.playerobj.transform.position =  new Vector3(pos.x, 1.39f + splashOffset, pos.y*2); 
		SplashLoad.gameName = SplashLoad.prefabsMgr.generateObjectFromPrefab("gameName");
		SplashLoad.gameName.GetComponent<Rigidbody>().useGravity=false;
		SplashLoad.gameName.transform.position = new Vector3 (pos.x+-0.8f,1.39f + splashOffset+0.4f,pos.y*2);
		SplashLoad.gameName.AddComponent<Splash> ();
		SplashLoad.gameName.SetActive (true);
		setSplashShown (1); 


		SplashLoad.cam.transform.position =new Vector3 (0.0f, 1.39f + splashOffset, pos.y*2-5);
		SplashLoad.cam.transform.rotation= Quaternion.Euler(0.0f, 0.0f, 0.0f);

		ProgressBar.loading=false;
	}

	public float getSplashOffset(){
		return splashOffset;
	}

	public void setSplashOffset(float splashOffset){
		this.splashOffset = splashOffset;
	}

}
