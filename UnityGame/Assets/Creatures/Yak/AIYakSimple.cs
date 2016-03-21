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

	// Use this for initialization
	void Start () {
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
	}

	public bool killYak(){
		bool wasJustKilled = isAlive;
		isAlive = false;
		animator.SetBool("isAlive", isAlive);
		agent.enabled = false;
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
