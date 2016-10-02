using UnityEngine;
using System.Collections;

public class newBackground : MonoBehaviour {

	private Vector3 startPosition;

	void Start ()
	{
		startPosition = transform.position;
	}

	void Update ()
	{
		float newPosition = Mathf.Repeat(Time.time * 0.5f, 3);
		transform.position = startPosition + Vector3.forward * newPosition;
	}
}
