using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour {
	public void loadScene(string name){
		SceneManager.LoadScene (name);
	}
}
