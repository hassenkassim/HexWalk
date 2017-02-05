using UnityEngine;
using System.Collections;

public class flag : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		Texture2D tex = Resources.Load("chess.png") as Texture2D;
		GetComponent<Renderer> ().material.mainTexture = tex;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	
	}
}

