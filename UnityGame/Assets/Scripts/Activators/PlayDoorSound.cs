using UnityEngine;
using System.Collections;

public class PlayDoorSound : AbstractActivator {

	// SOUND
	[FMODUnity.EventRef]
	public string openEvent;
	FMOD.Studio.EventInstance openSound;

	// SOUND
	[FMODUnity.EventRef]
	public string closeEvent;
	FMOD.Studio.EventInstance closeSound;

	private Rigidbody cachedRigidBody;

	void OnDestroy() {
		openSound.release();
		closeSound.release();
	}

	// Use this for initialization
	void Start () {
		cachedRigidBody = GetComponent<Rigidbody>();
		openSound = FMODUnity.RuntimeManager.CreateInstance(openEvent);
		closeSound = FMODUnity.RuntimeManager.CreateInstance(closeEvent);

		openSound.setParameterValue("Distance", 0.0f);
		closeSound.setParameterValue("Distance", 0.0f);
	}

	// Update is called once per frame
	void Update () {
		openSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject, cachedRigidBody));
		closeSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject, cachedRigidBody));
	}

	public override void onActivate(){

		openSound.start();
	}

	public override void onDeactivate(){

		closeSound.start();
	}


}
