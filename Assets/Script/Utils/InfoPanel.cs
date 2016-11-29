using UnityEngine;
using System.Collections;

public class InfoPanel : MonoBehaviour {

	public static InfoPanel instance;
	public static CanvasGroup canvasGroup;
	public static GameObject panel;

	void Awake() {
		
		instance = this;
		canvasGroup = GetComponent<CanvasGroup> ();
		canvasGroup.alpha = 0;
		canvasGroup.interactable = false;
		canvasGroup.gameObject.SetActive (false);	
	}

	public static void FadeInPanel(){
		canvasGroup.gameObject.SetActive (true);
		instance.StartCoroutine (DoFadeIn());
	}

	static IEnumerator DoFadeIn(){
		while (canvasGroup.alpha < 1) {
			canvasGroup.alpha += Time.deltaTime * 5;
			yield return null;
		}
		canvasGroup.interactable = true;
	}

	public static void FadeOutPanel(){
		instance.StartCoroutine (DoFadeOut());
	}

	static IEnumerator DoFadeOut(){
		while (canvasGroup.alpha > 0) {
			canvasGroup.alpha -= Time.deltaTime * 5;
			yield return null;
		}
		canvasGroup.interactable = false;
		canvasGroup.gameObject.SetActive (false);	
	}
}
