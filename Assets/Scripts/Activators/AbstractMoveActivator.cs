using UnityEngine;
using System.Collections;

public class AbstractMoveActivator : AbstractActivator {

	public float showMoveSpeed;
	public float hideMoveSpeed;
	protected Vector3 showPos;
	protected Vector3 hidePos;

	private float currentMoveSpeed;
	private Vector3 startPosition;
	private Vector3 endPosition;
	private float startTime;
	private float journeyLength;

	// initialise the variable used to calculate the postition lerping
	protected void setUp() {
		// start by hiding
		startPosition = showPos;
		endPosition = hidePos;
		startTime = Time.time;
		currentMoveSpeed = hideMoveSpeed;

		journeyLength = Vector3.Distance(startPosition, endPosition);
		if(journeyLength <= 0){
			Debug.logger.Log("Your start position and endposition are the same");
		}
	}
	
	// Update is called once per frame
	void Update () {
		// keep moving towards the endPosition
		float distCovered = (Time.time - startTime) * currentMoveSpeed;
		float fracJourney = distCovered / journeyLength;
		transform.localPosition = Vector3.Lerp(startPosition, endPosition, fracJourney);
	}

	public override void onActivate(){
		// begin moving to the show position
		startPosition = transform.localPosition;
		endPosition = showPos;
		startTime = Time.time;
		currentMoveSpeed = showMoveSpeed;
	}

	public override void onDeactivate(){
		// begin moving to the hide position
		startPosition = transform.localPosition;
		endPosition = hidePos;
		startTime = Time.time;
		currentMoveSpeed = hideMoveSpeed;
	}
}
