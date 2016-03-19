using UnityEngine;
using System.Collections;

public class SpearThrow : MonoBehaviour {

	public Transform spearHolder;
	public Transform player;
	public Transform playerCam;
	public float throwPower;
	public string animalTag;

	private Rigidbody rigidbody;
	private CapsuleCollider collider;

	public Vector3 restPos;
	public Vector3 restRot;

	public Vector3 aimPos;
	public Vector3 aimRot;

	// Use this for initialization
	void Start () {
		rigidbody = GetComponent<Rigidbody>();
		collider = GetComponent<CapsuleCollider>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButton("Fire2")) {
			if(this.transform.parent == player) {
				raiseSpear();
			}else if(this.transform.parent == playerCam) {
				if(Input.GetButton("Fire1")) {
					throwSpear();
				}
			}
		}else if(this.transform.parent == playerCam) {
			lowerSpear();
		}else if(this.transform.parent != playerCam && this.transform.parent != player && Input.GetButton("Fire1")) {
			pickUpSpear();
		}
	}

	void throwSpear() {
		this.transform.parent = spearHolder;
		this.rigidbody.AddRelativeForce(Vector3.up * throwPower);
		this.rigidbody.useGravity = true;
		this.collider.enabled = true;
	}

	void raiseSpear() {
		Debug.Log("Aiming spear");
		this.transform.parent = playerCam;
		this.transform.localPosition = this.aimPos;
		this.transform.localEulerAngles = this.aimRot;
	}

	void lowerSpear() {
		Debug.Log("Lowering spear");
		this.transform.parent = player;
		this.transform.localPosition = this.restPos;
		this.transform.localEulerAngles = this.restRot;
	}

	void pickUpSpear() {
		Debug.Log("Picking up spear");
		// stop the spear
		rigidbody.velocity = Vector3.zero;
		rigidbody.angularVelocity = Vector3.zero;

		this.transform.parent = player;
		this.rigidbody.useGravity = false;
		this.collider.enabled = false;

		// reset the transform
		this.transform.localPosition = this.restPos;
		this.transform.localEulerAngles = this.restRot;
	}

	void OnCollisionEnter(Collision collision) {
		if(collision.collider.gameObject.tag == this.animalTag) {
			rigidbody.velocity = Vector3.zero;
			rigidbody.angularVelocity = Vector3.zero;

			this.rigidbody.useGravity = false;
			this.collider.enabled = false;

			this.transform.parent = collision.collider.gameObject.transform;
		}
	}

}
