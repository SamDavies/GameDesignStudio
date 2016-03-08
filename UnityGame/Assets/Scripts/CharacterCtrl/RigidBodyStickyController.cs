using UnityEngine;
using System.Collections;

public class RigidBodyStickyController : RigidbodyFirstPersonControllerPublic {

	private float distGround; // distance from character position to ground
	private float gravity = 10; // gravity acceleration
	private bool isGrounded;
	private Vector3 surfaceNormal; // current surface normal
	private float deltaGround = 0.2f; // character is grounded up to this distance
	private float lerpSpeed = 10; // smoothing speed
	private float moveSpeed = 6; // move speed

	private Vector3 myNormal; // character normal
	private Transform myTransform;

	// Use this for initialization
	void Start () {
		base.Start();
		myNormal = transform.up; // normal starts as character up direction
		myTransform = transform;
		m_RigidBody.freezeRotation = true; // disable physics rotation
		// distance from transform.position to ground
		distGround = m_Capsule.height/ 2.0f;
	}

	void FixedUpdate() {
		base.FixedUpdate();
		m_RigidBody.AddForce(-gravity*m_RigidBody.mass*myNormal);
	}
	
	// Update is called once per frame
	void Update () {
		base.Update();
		Ray ray;
		RaycastHit hit;
		// update surface normal and isGrounded:
		ray = new Ray(myTransform.position, -myNormal); // cast ray downwards
		if (Physics.Raycast(ray, out hit)){ // use it to update myNormal and isGrounded
			isGrounded = hit.distance <= distGround + deltaGround;
			surfaceNormal = hit.normal;
		}
		else {
			isGrounded = false;
			// assume usual ground normal to avoid "falling forever"
			surfaceNormal = Vector3.up;
		}
		myNormal = Vector3.Lerp(myNormal, surfaceNormal, lerpSpeed*Time.deltaTime);
		// find forward direction with new myNormal
		Vector3 myForward = Vector3.Cross(myTransform.right, myNormal);
		Debug.Log(myForward);
		// align character to the new myNormal while keeping the forward direction:
		Quaternion targetRot = Quaternion.LookRotation(myForward, myNormal);
		myTransform.rotation = Quaternion.Lerp(myTransform.rotation, targetRot, lerpSpeed*Time.deltaTime);
	}
}
