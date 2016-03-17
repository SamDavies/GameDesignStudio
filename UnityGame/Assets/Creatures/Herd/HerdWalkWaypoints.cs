using UnityEngine;
using System.Collections;

public class HerdWalkWaypoints : YakBehavouir {

	private AIHerd herd;

	public HerdWalkWaypoints(AIHerd herd) {
		this.herd = herd;
	}

	public override void changeAgent(){
		herd.agent.speed = 3.5f;
		herd.agent.angularSpeed = 120;
		herd.agent.acceleration = 10f;
	}

	public override void doNextAction(){
		if(herd.agent.remainingDistance < 10f) {
			herd.agent.SetDestination(herd.waypoints[herd.nextDest].position);
			herd.nextDest = (herd.nextDest + 1) % herd.waypoints.Length;
		}
	}
}
