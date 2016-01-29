using UnityEngine;
using System.Collections;

public class AbstractMoveActivator : AbstractActivator {

	public float moveSpeed;
	protected Vector3 showPos;
	protected Vector3 hidePos;

	private Vector3 startPosition;
	private Vector3 endPosition;
	private float startTime;
	private float journeyLength;

	// initialise the variable used to calculate the postition lerping
	protected void setUp() {
		startPosition = showPos;
		endPosition = hidePos;

		startTime = Time.time;
		journeyLength = Vector3.Distance(startPosition, endPosition);
	}
	
	// Update is called once per frame
	void Update () {
		// keep moving towards the endPosition
		float distCovered = (Time.time - startTime) * moveSpeed;
		float fracJourney = distCovered / journeyLength;
		transform.localPosition = Vector3.Lerp(startPosition, endPosition, fracJourney);
	}

	public override void onActivate(){
		// begin moving to the show position
		startPosition = transform.localPosition;
		endPosition = showPos;
		startTime = Time.time;
	}

	public override void onDeactivate(){
		// begin moving to the hide position
		startPosition = transform.localPosition;
		endPosition = hidePos;
		startTime = Time.time;
	}
}
