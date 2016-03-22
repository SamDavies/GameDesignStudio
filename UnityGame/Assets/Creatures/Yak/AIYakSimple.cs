using UnityEngine;
using System.Collections;

public class AIYakSimple : AIYakAbstract {

	public Animator animator;
	public int nextDest = 0;
	public int runAwayStepSize = 10;
	public bool isAlive = true;

	// Behavouirs of the yak
	private YakRunAway yakRunAway;
	private YakFollowWaypoints yakFollowWaypoints;

	// SOUND
	[FMODUnity.EventRef]
	public string yakAliveEvent;
	FMOD.Studio.EventInstance yakAliveSound;

	// SOUND
	[FMODUnity.EventRef]
	public string yakDeathEvent;
	FMOD.Studio.EventInstance yakDeathSound;

	private Rigidbody cachedRigidBody;

	void OnDestroy() {
		yakAliveSound.release();
	}

	// Use this for initialization
	void Start () {

		cachedRigidBody = GetComponent<Rigidbody>();
		yakAliveSound = FMODUnity.RuntimeManager.CreateInstance(yakAliveEvent);

		yakAliveSound.setParameterValue("Distance", 0.0f);
		yakAliveSound.start();

		yakDeathSound = FMODUnity.RuntimeManager.CreateInstance(yakDeathEvent);

		yakRunAway = new YakRunAway(this, runAwayStepSize);
		yakFollowWaypoints = new YakFollowWaypoints(this);

		agent = GetComponent<NavMeshAgent>();
		this.setFollowWaypoints();
	}
	
	// Update is called once per frame
	void Update () {
		if(isAlive) {
			if(eyes.getIsPlayerSeen()){
				setRunAway();
			} else {
				setFollowWaypoints();
			}

			currentBehavouir.doNextAction();
		}
			
		yakAliveSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject, cachedRigidBody));
		yakDeathSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject, cachedRigidBody));
	}

	public bool killYak(){
		bool wasJustKilled = isAlive;
		isAlive = false;
		animator.SetBool("isAlive", isAlive);
		agent.enabled = false;
		yakAliveSound.setPaused(true);
		yakDeathSound.start();
		return wasJustKilled;
	}

	public bool isYakRunningAway() {
		return currentBehavouir == yakRunAway;
	}

	public void setRunAway() {
		this.setBehavouir(yakRunAway);
		Debug.Log("Running Away!");
	}

	public void setFollowWaypoints() {
		this.setBehavouir(yakFollowWaypoints);
		Debug.Log("Following Waypoints");
	}

	private void setBehavouir(YakBehavouir newBehave) {
		if(currentBehavouir != newBehave) {
			currentBehavouir = newBehave;
			currentBehavouir.changeAgent();
		}
	}
}
