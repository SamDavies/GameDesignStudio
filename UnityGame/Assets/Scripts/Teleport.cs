using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour {

	public Transform destination;

	// SOUND
	[FMODUnity.EventRef]
	public string soundEvent;
	public FMOD.Studio.EventInstance theSound;

	void OnDestroy() {
		theSound.release();
	}

	// Use this for initialization
	void Start () {
		theSound = FMODUnity.RuntimeManager.CreateInstance(soundEvent);

		// a bunch of default vaiables which trick fmod into looping
		theSound.setParameterValue("Distance", 0.0f);
		theSound.setParameterValue("Direction", 0.0f);
	}

	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			other.transform.position = destination.position;
			theSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(other.gameObject));
			theSound.start();
		}
	}
}
