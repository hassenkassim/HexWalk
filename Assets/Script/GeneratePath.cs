using UnityEngine;
using System.Collections;
using System.Linq;

/*
 * This class should generate a path randomly in the future
 * */
public class GeneratePath : MonoBehaviour {

	private GameObject[] objs;

	private void Start(){
		
		objs = GameObject.FindGameObjectsWithTag ("Button").OrderBy(go=>go.name).ToArray();// 40 elements in array

		objs [1].GetComponent<MeshRenderer> ().material.color = Color.white;
		//PauseAndResume ();
		//objs [11].GetComponent<MeshRenderer> ().material.color = Color.white;
		//PauseAndResume ();

		for (int i = 0; i < objs.Length; i++) {
			//print (objs[i].name);
			objs [i].GetComponent<MeshRenderer> ().material.color = Color.white;


		}
			

	}


	void PauseAndResume()
	{
		Time.timeScale = 0;
		StartCoroutine (waitForSomeSec());
	}

	private IEnumerator waitForSomeSec(){
		Time.timeScale = 0.1f;
		float pauseEndTime = Time.realtimeSinceStartup + 1;

		while (Time.realtimeSinceStartup < pauseEndTime)
			yield return 0;
		Time.timeScale = 1;
	}
}
