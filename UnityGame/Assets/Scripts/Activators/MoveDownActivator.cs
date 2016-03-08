using UnityEngine;
using System.Collections;

public class MoveDownActivator : AbstractMoveActivator {

	public float showY;
	public float hideY;

	// Use this for initialization
	void Start () {
		showPos = new Vector3(transform.localPosition.x, showY, transform.localPosition.z);
		hidePos = new Vector3(transform.localPosition.x, hideY, transform.localPosition.z);
		this.setUp();
	}
}
