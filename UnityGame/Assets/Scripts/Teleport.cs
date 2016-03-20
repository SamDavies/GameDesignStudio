using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour {

	public Transform destination;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			other.transform.position = destination.position;
		}
	}
}
