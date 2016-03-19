using UnityEngine;
using System.Collections;

public class YakEyes: MonoBehaviour {

	// the tag to trigger for
	public string triggerTag;
	public AIYak yak;

	private bool isPlayerSeen;
	private Vector3 lastKnownPlayerPos = new Vector3(0, 0, 0);

	// Use this for initialization
	void Start () {
	}

	public bool getIsPlayerSeen() {
		return this.isPlayerSeen;
	}

	public Vector3 getLastKnownPlayerPos () {
		return this.lastKnownPlayerPos;
	}

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == this.triggerTag){
			lastKnownPlayerPos = other.transform.position;
			this.isPlayerSeen = true;
		}
	}

	void OnTriggerExit(Collider other) {
		if(other.gameObject.tag == this.triggerTag){
			this.isPlayerSeen = false;
		}
	}
}
