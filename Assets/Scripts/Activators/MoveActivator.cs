using UnityEngine;
using System.Collections;

public class MoveActivator : AbstractMoveActivator {

	public Vector3 showPosition;
	public Vector3 hidePosition;

	// Use this for initialization
	void Start () {
		showPos = showPosition;
		hidePos = hidePosition;
		this.setUp();
	}
}
