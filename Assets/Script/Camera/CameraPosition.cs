using UnityEngine;
using System.Collections;

/*
 * This class should keep track of the camera position
 * */
public class CameraPosition : MonoBehaviour {

	private Vector3 offsetPlayerCam;
	private Vector3 rotationPlayerCam;
	private Vector3 offsetGamefieldCam;
	private Vector3 rotationGamefieldCam;

	private bool changing;
	private bool follow;

	/*
	 * CamID == 0: PlayerCam
	 * CamID == 1: GamefieldCam
	 * */
	private int CamID;



	// Use this for initialization
	void Start () {
		changing = false;
		follow = false;
		CamID = 1;

		offsetPlayerCam = new Vector3 (0.0f, 3.0f, -4.0f);
		rotationPlayerCam = new Vector3 (30.0f, 0.0f, 0.0f);

		/* analyze best camera position and rotation for Gamefield view
		 * Position: 	x: in the middle of Gamefield.width
		 * 				y: 15.0f
		 * 				z: -3.0f
		 * Rotation:	x: depends on Gamefield.height
		 * */
		offsetGamefieldCam = new Vector3 (((float)Gameplay.gamefield.width-1)/2, 2.0f + ((float)Gameplay.gamefield.getFieldHeight() * ((float)9/(float)5)), ((float)Gameplay.gamefield.height-1)/2);
		rotationGamefieldCam = new Vector3 (90.0f, 0.0f, 0.0f);


		setPosition (offsetGamefieldCam);
		setRotation (rotationGamefieldCam);

		//initial setup of camera position and rotation
		//transform.position = Gameplay.player.getTransform().position + offsetPlayerCam;
		//transform.rotation = Quaternion.Euler(rotationPlayerCam.x, rotationPlayerCam.y, rotationPlayerCam.z);
	}

	// Update is called once per frame
	void LateUpdate () {
		if (follow) {
			if (CamID == 0) {
				setPosition (Gameplay.player.getTransform ().position + offsetPlayerCam);
				setRotation (rotationPlayerCam);
			} else if (CamID == 1) {
				setPosition (offsetGamefieldCam);
				setRotation (rotationGamefieldCam);
			}	
		}

	}


	public void setPosition(Vector3 pos){
		transform.position = pos;
	}

	public void setRotation(Vector3 rot){
		transform.rotation = Quaternion.Euler(rot.x, rot.y, rot.z);
	}
		

	public void startTransition(){
		CamID = 0;
		changing = true;
		StartCoroutine(TransitionGamefieldPlayer(2, Gameplay.player.getTransform ().position + offsetPlayerCam, rotationPlayerCam)); 	
	}


	private Vector3 currentAngle;


	IEnumerator TransitionGamefieldPlayer(float lerpSpeed, Vector3 newPosition, Vector3 newRotation)
	{    

		float t = 0.0f;
		Vector3 startingPos = transform.position;
		currentAngle = transform.eulerAngles;

		while (t < 1.0f)
		{
			t += Time.deltaTime * (Time.timeScale / lerpSpeed);

			transform.position = Vector3.Lerp(startingPos, newPosition, t);

			currentAngle = new Vector3(
				Mathf.LerpAngle(currentAngle.x, newRotation.x, t/20),
				Mathf.LerpAngle(currentAngle.y, newRotation.y, t/20),
				Mathf.LerpAngle(currentAngle.z, newRotation.z, t/20));

			transform.eulerAngles = currentAngle;
				
			yield return 0;
		}

		Debug.Log ("Camera 2 reached!");

		changing = false;
		follow = true;

		yield return 0;
	}


	
}





