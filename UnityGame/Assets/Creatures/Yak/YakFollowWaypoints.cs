using UnityEngine;
using System.Collections;

public class YakFollowWaypoints : YakBehavouir {

	private AIYakSimple yak;

	public YakFollowWaypoints(AIYakSimple yak) {
		this.yak = yak;
	}

	public override void changeAgent(){
		yak.agent.speed = 3.5f;
		yak.agent.angularSpeed = 120;
		yak.agent.acceleration = 8f;
	}

	public override void doNextAction(){
		if(yak.agent.remainingDistance < 10f) {
			yak.agent.SetDestination(yak.herd.waypoints[yak.nextDest].position);
			yak.nextDest = (yak.nextDest + 1) % yak.herd.waypoints.Length;
		}
	}
}
