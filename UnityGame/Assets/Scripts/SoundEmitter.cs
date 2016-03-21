using UnityEngine;
using System.Collections;

public class SoundEmitter : MonoBehaviour {

	// SOUND
	[FMODUnity.EventRef]
	public string soundEvent;
	FMOD.Studio.EventInstance theSound;

	private Rigidbody cachedRigidBody;

	void OnDestroy() {
		theSound.release();
	}

	// Use this for initialization
	void Start () {
		cachedRigidBody = GetComponent<Rigidbody>();
		theSound = FMODUnity.RuntimeManager.CreateInstance(soundEvent);

		// a bunch of default vaiables which trick fmod into looping
		theSound.setParameterValue("Distance", 0.0f);
		theSound.setParameterValue("Direction", 0.0f);
		theSound.start();
	}
	
	// Update is called once per frame
	void Update () {
		theSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject, cachedRigidBody));
	}
}
