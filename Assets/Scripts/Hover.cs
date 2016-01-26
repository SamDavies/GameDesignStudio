using UnityEngine;
using System.Collections;

public class Hover : MonoBehaviour {

	public float aplitude;
	public float moveSpeed;

	private float randomOffset;

	// Use this for initialization
	void Start () {
		randomOffset = Random.Range(0, 10);
	}
		

	// Update is called once per frame
	void Update () {
		transform.localPosition = new Vector3(
			transform.localPosition.x, 
			Mathf.Sin(randomOffset + (Time.time * moveSpeed)) * aplitude, 
			transform.localPosition.z
		);
	}
}
