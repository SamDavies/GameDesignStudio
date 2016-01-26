using UnityEngine;
using System.Collections;

public class SwoopingActivation : MonoBehaviour {

	public float radius;
	public Vector3 randomness;
	public float moveSpeed;

	private Vector3 startPosition;
	private Vector3 endPosition;
	private float startTime;
	private float journeyLength;

	// Use this for initialization
	void Start () {
		endPosition = transform.localPosition;

		// create a random start position which is limited between the min and max positions
		float randX = Random.Range(-randomness.x, randomness.x);
		float randY = Random.Range(-randomness.y, randomness.y);
		float randZ = Random.Range(-randomness.z, randomness.z);
		startPosition = radius * new Vector3(randX, randY, randZ).normalized;

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
}
