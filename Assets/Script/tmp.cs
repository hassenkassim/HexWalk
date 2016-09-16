using UnityEngine;
using System.Collections;

public class tmp : MonoBehaviour {
	public GameObject test;

	public GameObject [,] tmpArray;

	// Use this for initialization
	void Start () {
		tmpArray = new GameObject[2, 2];
		for (int i = 0; i < 1; i++) {
			for (int j = 0; j < 1; j++) {
				tmpArray[i,j]=(GameObject)Instantiate(test,new Vector3(i,j,0),Quaternion.identity) ;
			}		
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
