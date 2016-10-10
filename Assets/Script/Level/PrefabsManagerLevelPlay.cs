using UnityEngine;
using System.Collections;

public class PrefabsManagerLevelPlay : MonoBehaviour {
	
		// Use this for initialization
		void Start () {
		}

		// Update is called once per frame
		void Update () {
		}

		public GameObject generateObjectFromPrefab(string prefabName){
			return (GameObject)Instantiate(Resources.Load(prefabName));
		}

}
