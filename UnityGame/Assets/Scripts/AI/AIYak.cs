using UnityEngine;
using System.Collections;

public class AIYak : MonoBehaviour {

	public Transform[] waypoints;
	public int nextDest = 0;

	private NavMeshAgent agent;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		if(agent.remainingDistance < 0.5f) {
			agent.SetDestination(waypoints[nextDest].position);
			nextDest = (nextDest + 1) % waypoints.Length;
		}
	}
}
