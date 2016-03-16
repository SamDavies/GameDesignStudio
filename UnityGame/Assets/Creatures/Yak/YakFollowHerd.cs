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
	}

	public override void doNextAction(){
		if(yak.agent.remainingDistance < 0.5f) {
			yak.agent.SetDestination(yak.waypoints[yak.nextDest].position);
			yak.nextDest = (yak.nextDest + 1) % yak.waypoints.Length;
		}
	}
}
