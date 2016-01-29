using UnityEngine;
using System.Collections;

public class AbstractActivator : MonoBehaviour {

	public bool activated = false;

	public void activate() {
		if(!activated){
			activated = true;
			onActivate();
		}
	}

	public void deactivate() {
		if(activated){
			activated = false;
			onDeactivate();
		}
	}

	public virtual void onActivate(){}

	public virtual void onDeactivate(){}
}