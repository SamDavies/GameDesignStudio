using UnityEngine;
using System.Collections;

public abstract class YakBehavouir {

	/// <summary>
	/// Changes the agents attribute such as movement speed.
	/// </summary>
	public abstract void changeAgent();

	/// <summary>
	/// Does the next action such as moving the agent to a point.
	/// </summary>
	public abstract void doNextAction();
}
