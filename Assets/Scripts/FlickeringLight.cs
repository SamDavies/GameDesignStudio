using UnityEngine;
using System.Collections;

public class FlickeringLight : MonoBehaviour {

	public Light light;
	public float minIntensity;
	public float maxIntensity;
	public float maxChangePerUpdate;

	// Use this for initialization
	void Start () { 
	}
	
	// Update is called once per frame
	void Update () {
		// set a new random intensity in as a random number between the min and max intensity
		float preIntensity = light.intensity;
		float newIntensity = Random.Range(minIntensity, maxIntensity);

		// limit the change in light intensity
		newIntensity = Mathf.Clamp(newIntensity, preIntensity-maxChangePerUpdate, preIntensity+maxChangePerUpdate);
		light.intensity = newIntensity;
	}
}
