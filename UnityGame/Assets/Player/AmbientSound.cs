using UnityEngine;
using System.Collections;

public class AmbientSound : MonoBehaviour {

	// SOUND
	[FMODUnity.EventRef]
	public string ambientEvent;
	FMOD.Studio.EventInstance ambientSound;

	void StopAllPlayerEvents() {
		FMOD.Studio.Bus playerBus = FMODUnity.RuntimeManager.GetBus("bus:/player");
		playerBus.stopAllEvents(FMOD.Studio.STOP_MODE.IMMEDIATE);
	}

	void OnDestroy() {
		StopAllPlayerEvents();
		ambientSound.release();
	}

	// Use this for initialization
	void Start () {
		ambientSound = FMODUnity.RuntimeManager.CreateInstance(ambientEvent);
		ambientSound.setParameterValue("distance", 0.0f);
		ambientSound.start();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
