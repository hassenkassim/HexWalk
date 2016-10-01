using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

	public static float getHorizontalInput(){
		#if UNITY_EDITOR
			return Input.GetAxisRaw ("Horizontal");
		#endif

		#if UNITY_ANDROID || UNITY_IPHONE
			if (SwipeManager.IsSwipingLeft()) {
				return -1;
			} else if(SwipeManager.IsSwipingRight()) {
				return 1;
			}
			return 0
		#endif
	}

	public static float getVerticalInput(){
		#if UNITY_EDITOR
			return Input.GetAxisRaw ("Vertical");
		#endif

		#if UNITY_ANDROID || UNITY_IPHONE
			if (SwipeManager.IsSwipingDown()) {
			return -1;
			} else if(SwipeManager.IsSwipingUp()) {
			return 1;
			}
			return 0
		#endif
	}


	public static bool getClickTouchInput(){
		#if UNITY_EDITOR
			if(Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0)){
				return true;
			} else {
				return false;
			}
		#endif

		#if UNITY_ANDROID || UNITY_IPHONE
			if(Input.touchCount > 0){
				if (Input.touches[0].phase == TouchPhase.Ended) {
					return true;
				}
			}
		#endif
	}
}
