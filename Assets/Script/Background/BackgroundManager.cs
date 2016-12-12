/************************************************
 * HAMOTO Production 2016						*
 * Project: HexWalk								*
 * Authors: Tolga, Mohamed, Dursun, Hassen		*
 * Year: 2016									*
 *************************************************/
using UnityEngine;
public class BackgroundManager : MonoBehaviour 
{
	private static GameObject particleSys;
	public const string CURWORLD = "CURWORLD";

	void Start(){
	}

	public static void loadSkybox(Camera cam){ // if true = from GamePlay class, if false = from LevelPlay class
		int world = PlayerPrefs.GetInt(CURWORLD, 0);
		cam.gameObject.AddComponent<Skybox> ();
		cam.GetComponent<Skybox> ().material = Resources.Load<Material> ("skybox/skybox" + world);
	}	

	public static void setParticleSystem(Camera cam){
		string SceneName = ScenesManager.getCurrentSceneName ();
		switch (SceneName) {
		case ScenesManager.SCENE_SPLASH:
			particleSys=SplashLoad.prefabsMgr.generateObjectFromPrefab ("ParticleSystem");

			break;
		case ScenesManager.SCENE_GAME:
			particleSys=Gameplay.prefabsMgr.generateObjectFromPrefab ("ParticleSystem");
			break;
		case ScenesManager.SCENE_LEVEL:
			particleSys=LevelPlay.prefabsMgr.generateObjectFromPrefab ("ParticleSystem");
			break;
		case ScenesManager.SCENE_LEVEL2:
			particleSys=LevelPlay.prefabsMgr.generateObjectFromPrefab ("ParticleSystem");
			break;
		}
	}
}