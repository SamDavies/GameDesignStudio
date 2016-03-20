﻿using UnityEngine;
using System.Collections;

public class AIYak : AIYakAbstract {

	public int runAwayStepSize = 10;

	// Behavouirs of the yak
	private YakRunAway yakRunAway;
	private YakFollowHerd yakFollowHerd;
	private YakChaseHerd yakChaseHerd;

	// Use this for initialization
	void Start () {
		yakRunAway = new YakRunAway(this, runAwayStepSize);
		yakFollowHerd = new YakFollowHerd(this); 
		yakChaseHerd = new YakChaseHerd(this); 

		agent = GetComponent<NavMeshAgent>();
		this.setFollowHerd();
	}
	
	// Update is called once per frame
	void Update () {
		if(herd.isHerdRunning()) {
			setChaseHerd();
		} else {
			if(eyes.getIsPlayerSeen()){
				setRunAway();
			} else {
				setFollowHerd();
			}
		}

		this.currentBehavouir.doNextAction();
	}

	public bool isYakRunningAway() {
		return currentBehavouir == yakRunAway;
	}

	public void setRunAway() {
		this.setBehavouir(yakRunAway);
		Debug.Log("Running Away!");
	}

	public void setFollowHerd() {
		this.setBehavouir(yakFollowHerd);
		Debug.Log("Following Herd");
	}

	public void setChaseHerd() {
		this.setBehavouir(yakChaseHerd);
		Debug.Log("Chasing Herd");
	}

	private void setBehavouir(YakBehavouir newBehave) {
		if(currentBehavouir != newBehave) {
			currentBehavouir = newBehave;
			currentBehavouir.changeAgent();
		}
	}
}
