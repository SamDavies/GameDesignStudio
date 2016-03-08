using UnityEngine;
using System.Collections;

public class TurnYActivator : AbstractTurnActivator {

	public float showY;
	public float hideY;

	// Use this for initialization
	void Start () {
		showRot = new Vector3(transform.localEulerAngles.x, showY, transform.localEulerAngles.z);
		hideRot = new Vector3(transform.localEulerAngles.x, hideY, transform.localEulerAngles.z);
		this.setUp();
	}
}
