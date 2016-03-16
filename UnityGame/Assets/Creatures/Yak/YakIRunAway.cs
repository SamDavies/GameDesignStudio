using UnityEngine;
using System.Collections;

public class YakRunAway : YakBehavouir {

	private AIYak yak;
	private int moveDistance;

	private Transform nextDestination;

	public YakRunAway(AIYak yak, int moveDistance) {
		this.yak = yak;
		this.moveDistance = moveDistance;
	}

	public override void changeAgent(){
		yak.agent.speed = 20f;
		yak.agent.angularSpeed = 360;
		yak.agent.acceleration = 10f;
		yak.agent.SetDestination(this.getNextDestination());
	}

	public override void doNextAction(){
		if(yak.agent.remainingDistance < 0.5f) {
			// choose the furthest point and move to it
			yak.agent.SetDestination(this.getNextDestination());
		}
	}

	/// <summary>
	/// Refreshes the random point to move towards.
	/// </summary>
	private Vector3 getNextDestination() {
		// get 3 random points in space
		Vector3[] randomPositions = new Vector3[3];
		for(int i=0; i<randomPositions.Length; i++) {
			Vector2 randomVec = Random.insideUnitCircle.normalized * this.moveDistance;
			randomPositions[i] = yak.transform.position + new Vector3(randomVec.x, 0, randomVec.y);
		}

		Vector3 furthestPoint = randomPositions[0];
		float furthestDistance = 0.0f;
		foreach(Vector3 randomPosition in randomPositions) {
			float distPlayerPoint = Vector3.Distance(randomPosition, this.yak.lastKnownPlayerPos);
			if(distPlayerPoint > furthestDistance) {
				furthestPoint = randomPosition;
				furthestDistance = distPlayerPoint;
			}
		}
		Debug.Log(furthestPoint);
		return furthestPoint;
	}
}
