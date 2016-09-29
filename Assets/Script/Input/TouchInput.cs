using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TouchInput : MonoBehaviour {

	public LayerMask touchInputMask;

	private List<GameObject> touchList = new List<GameObject>();
	private GameObject[] touchesOld;
	private RaycastHit hit;

	void Update () {

		#if UNITY_EDITOR
		if (Input.GetMouseButton (0) || Input.GetMouseButtonDown (0) || Input.GetMouseButtonUp (0)) {

			if (Input.GetMouseButtonUp (0))
				Gameplay.player.setColor (Col.nextColor (Gameplay.player.getColor ()));

			touchesOld = new GameObject[touchList.Count];
			touchList.CopyTo (touchesOld);
			touchList.Clear ();

			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

			if (Physics.Raycast (ray, out hit, touchInputMask)) {
				GameObject recipient = hit.transform.gameObject;

				touchList.Add (recipient);

				if (Input.GetMouseButtonDown (0)) {
					recipient.SendMessage ("OnTouchDown", hit.point, SendMessageOptions.DontRequireReceiver);
				}
				if (Input.GetMouseButtonUp (0)) {
					recipient.SendMessage ("OnTouchUp", hit.point, SendMessageOptions.DontRequireReceiver);
				}
				if (Input.GetMouseButton (0)) {
					recipient.SendMessage ("OnTouchStay", hit.point, SendMessageOptions.DontRequireReceiver);
				}
			}
			
			foreach (GameObject g in touchesOld) {
				if (!touchList.Contains (g)) {
					g.SendMessage ("OnTouchExit", hit.point, SendMessageOptions.DontRequireReceiver);
				}
			}
		}
		#endif

		#if UNITY_ANDROID || UNITY_IPHONE

			
		#endif
	}
}
