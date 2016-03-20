using UnityEngine;
using System.Collections;

public class YakFollowHerd : YakBehavouir {

	private AIYak yak;

	public YakFollowHerd(AIYak yak) {
		this.yak = yak;
	}

	public override void changeAgent(){
		yak.agent.speed = 3.5f;
		yak.agent.angularSpeed = 120;
		yak.agent.acceleration = 8f;
		yak.agent.SetDestination(yak.herd.transform.position);
	}

	public override void doNextAction(){
		if(yak.agent.remainingDistance < 15.0f) {
			Vector3 goTo = yak.herd.getFollowPoint(yak);
			yak.agent.SetDestination(goTo);
		}
	}
}
