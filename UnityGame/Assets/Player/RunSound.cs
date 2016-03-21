using UnityEngine;
using System.Collections;

public class RunSound : MonoBehaviour {

	// SOUND
	[FMODUnity.EventRef]
	public string breathEvent;
	FMOD.Studio.EventInstance breathSound;

	[FMODUnity.EventRef]
	public string heartbeatEvent;
	FMOD.Studio.EventInstance heartbeatSound;

	public float stamina = 0f;
	public float changeSpeed = 0.1f;

	void OnDestroy() {
		breathSound.release();
	}

	// Use this for initialization
	void Start () {
		breathSound = FMODUnity.RuntimeManager.CreateInstance(breathEvent);
		breathSound.setParameterValue("stamina", 1.0f);
		breathSound.start();

		heartbeatSound = FMODUnity.RuntimeManager.CreateInstance(heartbeatEvent);
		heartbeatSound.setParameterValue("stamina", 1.0f);
		heartbeatSound.start();
	}

	// Update is called once per frame
	void Update () {
		float targetStamina = 0;
		if(Input.GetKey(KeyCode.LeftShift)) {
		}else{
			targetStamina = 1.0f;
		}
		stamina = Mathf.Lerp(stamina, targetStamina, changeSpeed);
		breathSound.setParameterValue("Stamina", stamina);
		heartbeatSound.setParameterValue("Stamina", stamina);


		breathSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
		heartbeatSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
	}
}
