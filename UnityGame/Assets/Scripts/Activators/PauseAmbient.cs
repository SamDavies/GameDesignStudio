using UnityEngine;
using System.Collections;

public class PauseAmbient : AbstractActivator {

	// SOUND
	public SoundEmitter ambient;
	public SoundEmitter crystal;

	private Rigidbody cachedRigidBody;

	// Use this for initialization
	void Start () {
	}

	public override void onActivate(){
		ambient.theSound.setPaused(true);
		crystal.theSound.setPaused(true);
	}

	public override void onDeactivate(){
		ambient.theSound.setPaused(false);
		crystal.theSound.setPaused(false);
	}


}
