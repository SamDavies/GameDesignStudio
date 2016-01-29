using UnityEngine;
using System.Collections;

public class SmartCube : MonoBehaviour {

	public float moveSpeed;
	public Vector3 startPosition;
	public Vector3 endPosition;

	private float startTime;
	private float journeyLength;

	// Use this for initialization
	void Start () {
		startTime = Time.time;
		journeyLength = Vector3.Distance(startPosition, endPosition);
	}
	
	// Update is called once per frame
	void Update () {
		// keep moving towards the endPosition
		float distCovered = (Time.time - startTime) * moveSpeed;
		float fracJourney = distCovered / journeyLength;
		transform.localPosition = Vector3.Lerp(startPosition, endPosition, fracJourney);

		// restart when the cube reaches the end
		if(transform.localPosition == endPosition){
			startTime = Time.time;
			transform.localPosition = startPosition;
		}
	}
}
