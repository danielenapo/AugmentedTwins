using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    public GameObject button, slider, monitor, thermometer, switchButton, capsules;
	public GameObject actuatorBackground, sensorBackground;
	public float offsetSize = 100f; //layout grid only works for UI elements, so offset is required for 3d objects


	private Dictionary<string, string> discoveryDict;
	private CoapProxy coapProxy;
	private Vector3 thisPosition;
	private float offset = 0;
	public void initialize(Dictionary<string, string> discoveryDict, CoapProxy coapProxy)
	{
		this.discoveryDict = discoveryDict;
		this.coapProxy = coapProxy;
		thisPosition = this.gameObject.transform.position;
		//offsetSize /= 5; 
		//thisPosition.y += offsetSize;
	}

	public void instantiateActuator(string rt, string uri, string label)
	{
		GameObject newActuator;

		if (rt.Contains("btn"))
			newActuator = Instantiate(button, thisPosition, this.gameObject.transform.rotation);
		else if (rt.Contains("slider"))
			newActuator = Instantiate(slider, thisPosition, this.gameObject.transform.rotation);
		else if (rt.Contains("switch"))
			newActuator = Instantiate(switchButton, thisPosition, this.gameObject.transform.rotation);
		else
			return;
		Debug.Log("instantiated "+rt);
		newActuator.transform.localScale = new Vector3(0.5f, 0.5f, 1) / 100;
		newActuator.transform.parent = actuatorBackground.gameObject.transform;
		newActuator.GetComponent<Actuator>().initialize(coapProxy, uri, label);

	}


	public void instantiateSensor(string rt, string uri, string label) {
		GameObject newSensor;
		Vector3 position = sensorBackground.gameObject.transform.position;
		if (rt == "temp3d")
			newSensor = Instantiate(thermometer, new Vector3(position.x, position.y + offset, position.z), this.gameObject.transform.rotation);
		else if (rt.Contains("capsules"))
			newSensor = Instantiate(capsules, new Vector3(position.x, position.y + offset, position.z), this.gameObject.transform.rotation);
		else return;
		newSensor.transform.localScale = new Vector3(0.5f, 0.5f, 1) / 50;
		newSensor.transform.parent = sensorBackground.gameObject.transform;
		newSensor.GetComponent<Sensor>().initialize(coapProxy, uri, label);
		newSensor.GetComponent<Sensor>().printData();
		offset -= offsetSize;

	}

}
