using UnityEngine;
using System.Collections;

public class KillYaksQuest : Quest {

	public int numYaksKilled = 0;
	public int numYaksToKill = 5;

	public Light sun;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void killAYak() {
		numYaksKilled += 1;
		sun.intensity = 1.0f * (((numYaksToKill - (1.0f * numYaksKilled))/numYaksToKill));
	}

	public bool isComplete() {
		return numYaksKilled >= numYaksToKill;
	}
}
