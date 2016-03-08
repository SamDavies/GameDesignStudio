using UnityEngine;
using System.Collections;

public class AbstractTurnActivator : AbstractActivator {

	public float showMoveSpeed;
	public float hideMoveSpeed;
	protected Vector3 showRot;
	protected Vector3 hideRot;

	private float currentMoveSpeed;
	private Quaternion startRotation;
	private Quaternion endRotation;
	private float startTime;
	private float journeyLength;

	// initialise the variable used to calculate the postition lerping
	protected void setUp() {
		// start by hiding
		startRotation = Quaternion.Euler(showRot);
		endRotation = Quaternion.Euler(hideRot);
		startTime = Time.time;
		currentMoveSpeed = hideMoveSpeed;

		journeyLength = Vector3.Distance(showRot, hideRot);
		if(journeyLength <= 0){
			Debug.logger.Log("Your start position and endposition are the same");
		}
	}
	
	// Update is called once per frame
	void Update () {
		// keep moving towards the endPosition
		float distCovered = (Time.time - startTime) * currentMoveSpeed;
		float fracJourney = distCovered / journeyLength;
		transform.localRotation = Quaternion.Lerp(startRotation, endRotation, fracJourney);
	}

	public override void onActivate(){
		// begin moving to the show position
		startRotation = transform.localRotation;
		endRotation = Quaternion.Euler(showRot);
		startTime = Time.time;
		currentMoveSpeed = showMoveSpeed;
	}

	public override void onDeactivate(){
		// begin moving to the hide position
		startRotation = transform.localRotation;
		endRotation = Quaternion.Euler(hideRot);
		startTime = Time.time;
		currentMoveSpeed = hideMoveSpeed;
	}
}
