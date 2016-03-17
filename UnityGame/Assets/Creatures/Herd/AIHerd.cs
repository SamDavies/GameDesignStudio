using UnityEngine;
using System.Collections;

public class AIHerd: MonoBehaviour {

	public Transform[] waypoints;
	public AIYak[] yaks;
	public int nextDest = 0;
	public float pecentageBeforeRun = 0.5f;
	public int runCoolDownLength = 10;

	private float heardRunCoolDown = Time.time;

	public NavMeshAgent agent;

	// Behavouirs of the yak
	private HerdWalkWaypoints herdWalkWaypoints ;
	private HerdRunWaypoints herdRunWaypoints ;
	private YakBehavouir currentBehavouir;

	/// <summary>
	/// The number of yaks in the herd that are running running.
	/// </summary>
	private int numberOfHerdRunning;

	// Use this for initialization
	void Start () {
		herdWalkWaypoints = new HerdWalkWaypoints(this); 
		herdRunWaypoints = new HerdRunWaypoints(this); 

		agent = GetComponent<NavMeshAgent>();
		this.setWalkWaypoints();
	}

	// Update is called once per frame
	void Update () {
		// update the number of herd running
		this.numberOfHerdRunning = getNumberRunningYaks();
		if(this.isHerdRunning()) {
			setRunWaypoints();
		}else{
			setWalkWaypoints();
		}
		currentBehavouir.doNextAction();
	}

	public bool isHerdRunning() {
		return this.numberOfHerdRunning >= yaks.Length * pecentageBeforeRun || this.heardRunCoolDown > Time.time;
	}

	/// <summary>
	/// Gets the number yaks in the herd who think they are running away.
	/// </summary>
	/// <returns>The number running yaks.</returns>
	private int getNumberRunningYaks() {
		int numRunning = 0;
		foreach(AIYak yak in yaks) {
			if(yak.isYakRunningAway()){
				numRunning += 1;
			}
		}
		return numRunning;
	}

	public void setWalkWaypoints() {
		this.setBehavouir(herdWalkWaypoints);
		Debug.Log("Herd Walking");
	}

	public void setRunWaypoints() {
		this.refreshCoolDown();
		this.setBehavouir(herdRunWaypoints);
		Debug.Log("Herd Running");
	}

	private void refreshCoolDown() {
		if(this.numberOfHerdRunning >= yaks.Length * pecentageBeforeRun) {
			this.heardRunCoolDown = Time.time + this.runCoolDownLength; 
		}
	}

	private void setBehavouir(YakBehavouir newBehave) {
		if(currentBehavouir != newBehave) {
			currentBehavouir = newBehave;
			currentBehavouir.changeAgent();
		}
	}
}
