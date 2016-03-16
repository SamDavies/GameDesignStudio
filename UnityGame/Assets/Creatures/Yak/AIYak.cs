using UnityEngine;
using System.Collections;

public class AIYak : MonoBehaviour {

	public Transform[] waypoints;
	public int nextDest = 0;

	public int runAwayStepSize = 10;

	public NavMeshAgent agent;
	public Vector3 lastKnownPlayerPos = new Vector3(0, 0, 0);

	// Behavouirs of the yak
	private YakRunAway yakRunAway;
	private YakFollowHerd yakFollowHerd ;
	private YakBehavouir currentBehavouir;

	// Use this for initialization
	void Start () {
		yakRunAway = new YakRunAway(this, runAwayStepSize);
		yakFollowHerd = new YakFollowHerd(this); 

		agent = GetComponent<NavMeshAgent>();
		this.setFollowHerd();
	}
	
	// Update is called once per frame
	void Update () {
		currentBehavouir.doNextAction();
	}

	public void setRunAway() {
		this.setBehavouir(yakRunAway);
		Debug.Log("Running Away!");
	}

	public void setFollowHerd() {
		this.setBehavouir(yakFollowHerd);
		Debug.Log("Following");
	}

	private void setBehavouir(YakBehavouir newBehave) {
		currentBehavouir = newBehave;
		currentBehavouir.changeAgent();
	}
}
