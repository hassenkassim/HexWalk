/************************************************
 * HAMOTO Production 2016						*
 * Project: HexWalk								*
 * Authors: Tolga, Mohamed, Dursun, Hassen		*
 * Year: 2016									*
 *************************************************/

using UnityEngine;

public class BackgroundManager : MonoBehaviour 
{

	public static void loadSkybox(Camera cam){ // if true = from GamePlay class, if false = from LevelPlay class
			
		cam.gameObject.AddComponent<Skybox> ();

		cam.GetComponent<Skybox> ().material = Resources.Load<Material> ("skybox/skybox" + (((int)((LevelPlay.gamePosition.y) / 2 + 1)) - 1));
	}

	public static void setParticleSystem(Camera cam){ // if true = from GamePlay class, if false = from LevelPlay class
		if (cam.gameObject.GetComponent <ParticleSystem> () != null) {		
			cam.gameObject.AddComponent <ParticleSystem> ();
			ParticleSystem ps = cam.gameObject.GetComponent<ParticleSystem> ();
			ps.maxParticles = 1000;
			ps.startSize = 0.05f;
			var sh = ps.shape;
			sh.enabled = true;
			sh.shapeType = ParticleSystemShapeType.Cone;
			sh.angle = 15;
		}
	}
}