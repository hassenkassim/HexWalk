/************************************************
 * HAMOTO Production 2016						*
 * Project: HexWalk								*
 * Authors: Tolga, Mohamed, Dursun, Hassen		*
 * Year: 2016									*
 *************************************************/

using UnityEngine;
using System.Collections;

/*
 * This class should keep track of the camera position
 * */
public class CameraPosition : MonoBehaviour {

	readonly private Vector3 offsetPlayerCam = new Vector3 (0.0f, 3.0f, -4.0f);
	readonly private Vector3 rotationPlayerCam = new Vector3 (36.5f, 0.0f, 0.0f);
	private Vector3 offsetGamefieldCam;
	readonly private Vector3 rotationGamefieldCam = new Vector3 (90.0f, 0.0f, 0.0f);
	private Vector3 currentAngle;

	private int Cam2Counter = 0;

	private bool follow;

	/*
	 * CamID == 0: PlayerCam
	 * CamID == 1: GamefieldCam
	 * CamID == 2: PlayerCam - Just Rotation!
	 * */
	private int CamID;

	// Use this for initialization
	void Start () {
		follow = false;
		CamID = 1;

		/* analyze best camera position and rotation for Gamefield view
		 * Position: 	x: in the middle of Gamefield.width
		 * 				y: 15.0f
		 * 				z: -3.0f
		 * Rotation:	x: depends on Gamefield.height
		 * */
		offsetGamefieldCam = new Vector3 (((float)Gameplay.gamefield.width-1)/2, 2.0f + ((float)Gameplay.gamefield.getFieldHeight() * ((float)9/(float)5)), ((float)Gameplay.gamefield.height-1)/2);;

		//initial setup of camera position and rotation
		setPosition (offsetGamefieldCam);
		setRotation (rotationGamefieldCam);

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
			} else if(CamID == 2){
				float zaehler = (transform.position.z - Gameplay.player.playerobj.transform.position.z);
				float nenner = (transform.position.y - Gameplay.player.playerobj.transform.position.y);
				float rot = 90.0f - Mathf.Atan(zaehler/nenner) * Mathf.Rad2Deg * -1;
				setRotation (new Vector3(rot,0,0));
				if (Cam2Counter < 50) {
					Vector3 pos = getPosition ();
					setPosition (new Vector3(pos.x, pos.y-0.1f, pos.z));
					Cam2Counter++;
				}
			}	
		}

	}


	public Vector3 getPosition(){
		return transform.position;
	}

	public void setPosition(Vector3 pos){
		transform.position = pos;
	}

	public void setRotation(Vector3 rot){
		transform.rotation = Quaternion.Euler(rot.x, rot.y, rot.z);
	}
		

	public void startTransition(){
		CamID = 0;
		StartCoroutine(TransitionGamefieldPlayer(2)); 	
	}

	public void setToFollowPlayerByRotation(){
		CamID = 2;
	}
		
	IEnumerator TransitionGamefieldPlayer(float lerpSpeed)
	{    
		
		float t = 0.0f;
		Vector3 newPosition;
		Vector3 newRotation = rotationPlayerCam;
		Vector3 startingPos = transform.position;
		currentAngle = transform.eulerAngles;

		while (t < 1.0f)
		{

			//get Position on the fly
			newPosition = Gameplay.player.getTransform ().position + offsetPlayerCam;


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


		//dursun
		BackgroundManager.setParticleSystem(Gameplay.cam);

		//enable Input
		InputManager.active = true;

		follow = true;

		yield return 0;
	}
}