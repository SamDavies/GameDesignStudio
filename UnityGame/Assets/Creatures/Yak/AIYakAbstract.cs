using UnityEngine;
using System.Collections;

public class AIYakAbstract : MonoBehaviour {

	public AIHerd herd;
	public YakEyes eyes;

	public NavMeshAgent agent;

	protected YakBehavouir currentBehavouir;
}
