using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using System;
using UnityStandardAssets.Characters.FirstPerson;

/// <summary>
/// C# translation from http://answers.unity3d.com/questions/155907/basic-movement-walking-on-walls.html
/// Author: UA @aldonaletto 
/// </summary>
 
// Prequisites: create an empty GameObject, attach to it a Rigidbody w/ UseGravity unchecked
// To empty GO also add BoxCollider and this script. Makes this the parent of the Player
// Size BoxCollider to fit around Player model.
 
public class walOnWalls : MonoBehaviour
{

	[Serializable]
	public class MovementSettings
	{
		public float ForwardSpeed = 8.0f;   // Speed when walking forward
		public float BackwardSpeed = 4.0f;  // Speed when walking backwards
		public float StrafeSpeed = 4.0f;    // Speed when walking sideways
		public float RunMultiplier = 2.0f;   // Speed when sprinting
		public KeyCode RunKey = KeyCode.LeftShift;
		public float JumpForce = 30f;
		public AnimationCurve SlopeCurveModifier = new AnimationCurve(new Keyframe(-90.0f, 1.0f), new Keyframe(0.0f, 1.0f), new Keyframe(90.0f, 0.0f));
		[HideInInspector] public float CurrentTargetSpeed = 8f;

		#if !MOBILE_INPUT
		private bool m_Running;
		#endif

		public void UpdateDesiredTargetSpeed(Vector2 input)
		{
			if (input == Vector2.zero) return;
			if (input.x > 0 || input.x < 0)
			{
				//strafe
				CurrentTargetSpeed = StrafeSpeed;
			}
			if (input.y < 0)
			{
				//backwards
				CurrentTargetSpeed = BackwardSpeed;
			}
			if (input.y > 0)
			{
				//forwards
				//handled last as if strafing and moving forward at the same time forwards speed should take precedence
				CurrentTargetSpeed = ForwardSpeed;
			}
			#if !MOBILE_INPUT
			if (Input.GetKey(RunKey))
			{
				CurrentTargetSpeed *= RunMultiplier;
				m_Running = true;
			}
			else
			{
				m_Running = false;
			}
			#endif
		}

		#if !MOBILE_INPUT
		public bool Running
		{
			get { return m_Running; }
		}
		#endif
	}

	public MovementSettings movementSettings = new MovementSettings();
 
	private float moveSpeed = 50;
	// move speed
	private float turnSpeed = 90;
	// turning speed (degrees/second)
	private float lerpSpeed = 10;
	// smoothing speed
	private float gravity = 10;
	// gravity acceleration
	private bool isGrounded;
	private float deltaGround = 0.2f;
	// character is grounded up to this distance
	private float jumpSpeed = 10;
	// vertical jump initial speed
	private float jumpRange = 10;
	// range to detect target wall
	private Vector3 surfaceNormal;
	// current surface normal
	private Vector3 myNormal;
	// character normal
	private float distGround;
	// distance from character position to ground
	private bool jumping = false;
	// flag &quot;I'm jumping to wall&quot;
	private float vertSpeed = 0;
	// vertical jump current speed
 
	private Transform myTransform;
	public BoxCollider boxCollider;
	// drag BoxCollider ref in editor
	public Rigidbody rigidbody;
	public Camera cam;
	public MouseLook mouseLook = new MouseLook();

	private void Start ()
	{
		mouseLook.Init (transform, cam.transform);
		myNormal = transform.up; // normal starts as character up direction
		myTransform = transform;
		rigidbody.freezeRotation = true; // disable physics rotation
		// distance from transform.position to ground
		distGround = boxCollider.extents.y - boxCollider.center.y;
 
	}

	private void FixedUpdate ()
	{
		// apply constant weight force according to character normal:
		rigidbody.AddForce (-gravity * rigidbody.mass * myNormal);
	}

	private void Update ()
	{
		// jump code - jump to wall or simple jump
		if (jumping)
			return; // abort Update while jumping to a wall
 
		Ray ray;
		RaycastHit hit;
 
		if (Input.GetButtonDown ("Jump")) { // jump pressed:
			ray = new Ray (myTransform.position, myTransform.forward);
			if (Physics.Raycast (ray, out hit, jumpRange)) { // wall ahead?
				JumpToWall (hit.point, hit.normal); // yes: jump to the wall
			} else if (isGrounded) { // no: if grounded, jump up
				rigidbody.velocity += jumpSpeed * myNormal;
			}
		}

		myTransform.Rotate(0, Input.GetAxis("Horizontal")*turnSpeed*Time.deltaTime, 0);
		// update surface normal and isGrounded:
		ray = new Ray (myTransform.position, -myNormal); // cast ray downwards
		if (Physics.Raycast (ray, out hit)) { // use it to update myNormal and isGrounded
			isGrounded = hit.distance <= distGround + deltaGround;
			surfaceNormal = hit.normal;
		} else {
			isGrounded = false;
			// assume usual ground normal to avoid "falling forever"
			surfaceNormal = Vector3.up;
		}
		myNormal = Vector3.Lerp (myNormal, surfaceNormal, lerpSpeed * Time.deltaTime);
		// find forward direction with new myNormal:
		Vector3 myForward = Vector3.Cross (myTransform.right, myNormal);
		// align character to the new myNormal while keeping the forward direction:
		Quaternion targetRot = Quaternion.LookRotation (myForward, myNormal);
		myTransform.rotation = Quaternion.Lerp (myTransform.rotation, targetRot, lerpSpeed * Time.deltaTime);
		// move the character forth/back with Vertical axis:
		myTransform.Translate (0, 0, Input.GetAxis ("Vertical") * moveSpeed * Time.deltaTime);
	}

	private void JumpToWall (Vector3 point, Vector3 normal)
	{
		// jump to wall
		jumping = true; // signal it's jumping to wall
		rigidbody.isKinematic = true; // disable physics while jumping
		Vector3 orgPos = myTransform.position;
		Quaternion orgRot = myTransform.rotation;
		Vector3 dstPos = point + normal * (distGround + 0.5f); // will jump to 0.5 above wall
		Vector3 myForward = Vector3.Cross (myTransform.right, normal);
		Quaternion dstRot = Quaternion.LookRotation (myForward, normal);
     
		StartCoroutine (jumpTime (orgPos, orgRot, dstPos, dstRot, normal));
		//jumptime
	}

	private IEnumerator jumpTime (Vector3 orgPos, Quaternion orgRot, Vector3 dstPos, Quaternion dstRot, Vector3 normal)
	{
		for (float t = 0.0f; t < 1.0f;) {
			t += Time.deltaTime;
			myTransform.position = Vector3.Lerp (orgPos, dstPos, t);
			myTransform.rotation = Quaternion.Slerp (orgRot, dstRot, t);
			yield return null; // return here next frame
		}
		myNormal = normal; // update myNormal
		rigidbody.isKinematic = false; // enable physics
		jumping = false; // jumping to wall finished
 
	}
 
	private Vector2 GetInput()
	{

		Vector2 input = new Vector2
		{
			x = CrossPlatformInputManager.GetAxis("Horizontal"),
			y = CrossPlatformInputManager.GetAxis("Vertical")
		};
		movementSettings.UpdateDesiredTargetSpeed(input);
		return input;
	}
}