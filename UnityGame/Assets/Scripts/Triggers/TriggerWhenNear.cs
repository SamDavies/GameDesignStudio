using UnityEngine;
using System.Collections;

public class TriggerWhenNear : MonoBehaviour {

	// the tag to trigger for
	public string triggerTag;

	// the activator to activate when the object is too close
	public AbstractActivator activator;

	// Use this for initialization
	void Start () {
	}

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == this.triggerTag){
			activator.activate();
		}
	}

	void OnTriggerExit(Collider other) {
		if(other.gameObject.tag == this.triggerTag){
			activator.deactivate();
		}
	}
}
