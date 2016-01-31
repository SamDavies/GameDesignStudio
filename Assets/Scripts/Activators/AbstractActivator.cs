using UnityEngine;
using System.Collections;

public class AbstractActivator : MonoBehaviour {

	public bool activated = false;
	public bool activateOnce = false;

	private bool hasActivated = false;

	public void activate() {
		// if set to activateOnce, only allow 1 activation
		if(!activateOnce || !hasActivated){
			hasActivated = true;
			if(!activated){
				activated = true;
				onActivate();
			}
		}
	}

	public void deactivate() {
		// never deactivate for a single use activation
		if(!activateOnce){
			if(activated){
				activated = false;
				onDeactivate();
			}
		}
	}

	public virtual void onActivate(){}

	public virtual void onDeactivate(){}
}