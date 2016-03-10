using UnityEngine;
using System.Collections;

public class TurnZActivator : AbstractTurnActivator {

	public float showZ;
	public float hideZ;

	// Use this for initialization
	void Start () {
		showRot = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, showZ);
		hideRot = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, hideZ);
		this.setUp();
	}
}
