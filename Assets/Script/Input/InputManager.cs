using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class InputManager {

	static EventSystem eventsystem;

	public static bool active = false;

	public static float getHorizontalInput(){
		if (active) {
			#if UNITY_ANDROID || UNITY_IPHONE
			if (SwipeManager.IsSwipingLeft ()) {
//				DebugConsole.Log("IsSwipingLeft");
				return -1;
			} else if (SwipeManager.IsSwipingRight ()) {
//				DebugConsole.Log("IsSwipingRight");
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
			if(Input.touchCount > 0 ){ //&& !eventsystem.IsPointerOverGameObject (Input.GetTouch (0).fingerId)){
				if (Input.touches[0].phase == TouchPhase.Ended) {
							DebugConsole.Log("Touched");
							return true;
						}else{
							return false;
						}
				}


			#endif

			if (Input.GetKeyUp (KeyCode.Space) || Input.GetMouseButtonUp (0) && !EventSystem.current.IsPointerOverGameObject ()) {
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
}
