using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class InputManager : MonoBehaviour{

	static EventSystem eventsystem;

	public static bool active = false;

	public void Update(){
		IsPointerOverUIObject ();
	}

	public static float getHorizontalInput(){
		if (active) {
			#if UNITY_ANDROID || UNITY_IPHONE
			if (SwipeManager.IsSwipingLeft ()) {
				//DebugConsole.Log("IsSwipingLeft");
				return -1;

			} else if (SwipeManager.IsSwipingRight ()) {
				//DebugConsole.Log("IsSwipingRight");
				return 1;
			}
			#endif
			
			return Input.GetAxisRaw ("Horizontal");
		}
	return 0;
	}
		
	public static float getVerticalInput(){
		if (active) {
			#if UNITY_ANDROID || UNITY_IPHONE
				if (SwipeManager.IsSwipingDown()) {
//					DebugConsole.Log("IsSwipingDown");
					return -1;
				} else if(SwipeManager.IsSwipingUp()) {
//					DebugConsole.Log("IsSwipingUp");
					return 1;
				}
			#endif
			return Input.GetAxisRaw ("Vertical");
		}
		return 0;
	}


	public static bool getClickTouchInput(){
		if (active) {

			#if UNITY_ANDROID || UNITY_IPHONE
			if(Input.touchCount > 0 && IsPointerOverUIObject() == false){
				if (Input.touches[0].phase == TouchPhase.Ended) {
					//DebugConsole.Log("Touched");
					return true;
				}else{
					return false;
				}
			}


				
			#endif

			if (Input.GetKeyUp (KeyCode.Space) || Input.GetMouseButtonUp (0)) {
				return true;
			} else {
				return false;
			}
		} else {
			return false;
		}

	}


	public static Vector2 getInput(){
		float x = 0;
		float y = 0;
		x = InputManager.getHorizontalInput ();
		if (x == 0) y = InputManager.getVerticalInput ();

		return new Vector2 ((int)x, (int)y);
	}

	private static bool IsPointerOverUIObject() {
		PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
		eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		List<RaycastResult> results = new List<RaycastResult>();
		EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
		return results.Count > 0;
	}
}
