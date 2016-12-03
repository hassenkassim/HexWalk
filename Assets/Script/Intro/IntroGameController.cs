using UnityEngine;
using System.Collections;

public class IntroGameController : MonoBehaviour {
	bool colision = false;
	public static bool blue=false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		colision = IntroGame.collision;
		if (IntroGame.explode==true) {
			Vector3 tmp = new Vector3 (IntroGame.playerobj.transform.position.x,IntroGame.playerobj.transform.position.y,IntroGame.playerobj.transform.position.z);
			foreach (Collider col in Physics.OverlapSphere(transform.position,IntroGame.radius)) {
				Rigidbody rb = col.GetComponent<Rigidbody>();
				tmp.y = tmp.y - 0.03f;
				if (rb != null) {
					rb.AddExplosionForce (0.01f, tmp, IntroGame.radius);
				}
			}
			IntroGame.explode = false;
		}
	}

	void OnCollisionEnter(Collision coll)
	{
		GameObject tmp;
		if (colision && coll.collider.gameObject.name=="field0_0") {
			IntroGame.startShowPath ();
			IntroGame.playerobj.GetComponent<MeshRenderer> ().material.SetColor("_Color", Col.GRUEN);
		}
		if (colision && coll.collider.gameObject.CompareTag("Field")) {
			CameraPositionIntro.CamID = 1;

			tmp = coll.collider.gameObject;
			coll.collider.gameObject.GetComponent<MeshRenderer> ().material.SetColor("_Color", Col.GRUEN);
		}
		if (blue && colision && coll.collider.gameObject.CompareTag("Field")) {
			tmp = coll.collider.gameObject;
			coll.collider.gameObject.GetComponent<MeshRenderer> ().material.SetColor("_Color", Col.BLAU);
		}
		if (!colision && coll.collider.gameObject.CompareTag ("Field")) {
			CameraPositionIntro.CamID = 2;

			IntroGame.fractureCube (0.125f, coll.collider.gameObject);
			IntroGame.explode = true;
		}
	}

	void OnCollisionExit() {
//		tmp.GetComponent<MeshRenderer> ().material.SetColor("_Color", Col.WEISS);
	}

}
