/************************************************
 * HAMOTO Production 2016						*
 * Project: HexWalk								*
 * Authors: Tolga, Mohamed, Dursun, Hassen		*
 * Year: 2016									*
 *************************************************/
using UnityEngine;
public class BackgroundManager : MonoBehaviour 
{
	public const string CURWORLD = "CURWORLD";

	void Start(){
	}

	public static void loadSkybox(Camera cam){ // if true = from GamePlay class, if false = from LevelPlay class
		int world = PlayerPrefs.GetInt(CURWORLD, 0);
		cam.gameObject.AddComponent<Skybox> ();
		cam.GetComponent<Skybox> ().material = Resources.Load<Material> ("skybox/skybox" + world);
	}	

	public static void setParticleSystem(Camera cam){
		if (cam.gameObject.GetComponent <ParticleSystem> () == null) {		
			cam.gameObject.AddComponent <ParticleSystem> ();
			ParticleSystem ps = cam.gameObject.GetComponent<ParticleSystem> ();

//TODO: crashes on ios debug --> auskommentiert wird kein particlesystem angezeigt obwohl eig standard angezeigt werden sollte
			Material psMat = Resources.Load ("ParticleGlow",typeof(Material)) as Material;
			if (psMat == null) {
				DebugConsole.Log ("Material == NULL!");
				DebugConsole.Log ("Material == NULL!");
				DebugConsole.Log ("Material == NULL!");
				DebugConsole.Log ("Material == NULL!");
				DebugConsole.Log ("Material == NULL!");
				DebugConsole.Log ("Material == NULL!");
			} else {
				DebugConsole.Log ("Material != NULL!");
				DebugConsole.Log ("Material != NULL!");
				DebugConsole.Log ("Material != NULL!");
				DebugConsole.Log ("Material != NULL!");
				DebugConsole.Log ("Material != NULL!");
				DebugConsole.Log ("Material != NULL!");
				DebugConsole.Log ("Material != NULL!");
				DebugConsole.Log ("Material != NULL!");
				DebugConsole.Log ("Material != NULL!");
				DebugConsole.Log ("Material != NULL!");
				DebugConsole.Log ("Material != NULL!");
				DebugConsole.Log ("Material != NULL!");
				DebugConsole.Log (psMat.name);


			}

			ps.GetComponent<Renderer> ().material = psMat;

			//DebugConsole.Log (ps.GetComponent<ParticleRenderer> ().material.name);
						//ps.GetComponent<ParticleRenderer>().material = (Material)Resources.Load ("ParticleGlow");
//variante2:			ps.GetComponent<Renderer>().material = Resources.Load ("ParticleGlow",typeof(Material)) as Material;

			ps.maxParticles = 1000;
			ps.startSize = 0.05f;
			var sh = ps.shape;
			sh.enabled = true;
			sh.shapeType = ParticleSystemShapeType.Cone;
			sh.angle = 15;
		}
	}
}