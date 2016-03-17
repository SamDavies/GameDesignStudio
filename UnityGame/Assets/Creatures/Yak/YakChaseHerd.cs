using UnityEngine;
using System.Collections;

public class YakChaseHerd : YakBehavouir {

	private AIYak yak;

	public YakChaseHerd(AIYak yak) {
		this.yak = yak;
	}

	public override void changeAgent(){
		yak.agent.speed = 10f;
		yak.agent.angularSpeed = 360;
		yak.agent.acceleration = 10f;
	}

	public override void doNextAction(){
		if(yak.agent.remainingDistance < 10.0f) {
			yak.agent.SetDestination(yak.herd.getFollowPoint(yak));
		}
	}
}
