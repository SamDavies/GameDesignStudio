using UnityEngine;
using System.Collections;

public class HerdRunWaypoints : YakBehavouir {

	private AIHerd herd;

	public HerdRunWaypoints(AIHerd herd) {
		this.herd = herd;
	}

	public override void changeAgent(){
		herd.agent.speed = 11f;
		herd.agent.angularSpeed = 120;
		herd.agent.acceleration = 10f;
	}

	public override void doNextAction(){
		if(herd.agent.remainingDistance < 0.5f) {
			herd.agent.SetDestination(herd.waypoints[herd.nextDest].position);
			herd.nextDest = (herd.nextDest + 1) % herd.waypoints.Length;
		}
	}
}
