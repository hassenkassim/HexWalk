using UnityEngine;
using System.Collections;

public class GamefieldController : MonoBehaviour {

	public static bool transitionActive = false;

	public static Field[,] fields;
	public static int width;
	public static int height;

	public static Vector3 oldPositionOffset;
	public static Vector3 newPositionOffset;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (transitionActive == true) {
			transitionActive = false;
			//Start Coroutine
			StartCoroutine (GamefieldTransition(2f));
		}
		
	}

	IEnumerator GamefieldTransition(float lerpSpeed)
	{    
		float t = 0.0f;

		while (t < 1.0f)
		{
			t += Time.deltaTime * (Time.timeScale / lerpSpeed);

			//lerp for offset and set all fields with this offset!
			Vector3 offsetlerp = new Vector3 (
				Mathf.SmoothStep (oldPositionOffset.x, newPositionOffset.x, t),
				Mathf.SmoothStep (oldPositionOffset.y, newPositionOffset.y, t),
				Mathf.SmoothStep (oldPositionOffset.z, newPositionOffset.z, t));

			//set position for all new fields
			for (int i = 0; i < width; i++) {
				for (int j = 0; j < height; j++) {
					fields [i, j].setPosition (offsetlerp.x + i, offsetlerp.y + j, offsetlerp.z);
				}
			}

			yield return 0;
		}

		GameplayController.resetGameplayController ();
		yield return 0;
	}
}
