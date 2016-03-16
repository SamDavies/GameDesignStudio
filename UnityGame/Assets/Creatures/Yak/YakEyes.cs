using UnityEngine;
using System.Collections;

public class YakEyes : MonoBehaviour {

	// the tag to trigger for
	public string triggerTag;
	public AIYak yak;

	// Use this for initialization
	void Start () {
	}

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == this.triggerTag){
			yak.lastKnownPlayerPos = other.transform.position;
			yak.setRunAway();
		}
	}

	void OnTriggerExit(Collider other) {
		if(other.gameObject.tag == this.triggerTag){
			yak.setFollowHerd();
		}
	}
}
