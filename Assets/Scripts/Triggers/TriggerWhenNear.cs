using UnityEngine;
using System.Collections;

public class TriggerWhenNear : MonoBehaviour {

	// the maximum distance to trigger at
	public float activationDistance;

	// the object to measure the distance to
	public GameObject triggerObject;

	// the activator to activate when the object is too close
	public AbstractActivator activator;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float distance = Vector3.Distance(triggerObject.transform.position, transform.position);

		// activate when the target is too close
		if(distance < activationDistance) {
			activator.activate();
		} else {
			activator.deactivate();
		}
	}
}
