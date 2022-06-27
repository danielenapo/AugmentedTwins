using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    public GameObject button, slider, monitor, thermometer;
	public GameObject actuatorBackground, sensorBackground;
	public float offsetSize = 1.5f;

	private Dictionary<string, string> discoveryDict;
	private CoapProxy coapProxy;
	private Vector3 thisPosition;
	public void initialize(Dictionary<string, string> discoveryDict, CoapProxy coapProxy)
	{
		this.discoveryDict = discoveryDict;
		this.coapProxy = coapProxy;
		thisPosition = this.gameObject.transform.position;
		offsetSize /= 5;
		//thisPosition.y += offsetSize;
	}

	public void instantiateActuator(string type, string uri, string label)
	{
		GameObject newActuator;

		if (type == "button")
			newActuator = Instantiate(button, thisPosition, this.gameObject.transform.rotation);
		else
			newActuator = Instantiate(slider, thisPosition, this.gameObject.transform.rotation);
		Debug.Log("instantiated "+type+"!");
		newActuator.transform.localScale = new Vector3(0.5f, 0.5f, 1) / 100;
		Debug.Log("scaled!");
		newActuator.transform.parent = actuatorBackground.gameObject.transform;
		Debug.Log("parented!");
		newActuator.GetComponent<Actuator>().setUri(uri);
		newActuator.GetComponent<Actuator>().setLabel(label);
		newActuator.GetComponent<Actuator>().initialize(coapProxy);
		Debug.Log("setters!");
		//thisPosition.y -= offsetSize;
		Debug.Log("position updated!");
	}

	public void instantiateSensor(string type, string uri, string label) { 
		if (type=="thermometer")
		{
			GameObject newThermometer = Instantiate(thermometer, new Vector3(-(2.5f) * offsetSize, 0, 0), this.gameObject.transform.rotation);
			newThermometer.transform.localScale = new Vector3(0.5f, 0.5f, 1) / 50;
			newThermometer.transform.parent = sensorBackground.gameObject.transform;
			newThermometer.GetComponent<ThermometerDecorator>().setUri(uri);
			newThermometer.GetComponent<ThermometerDecorator>().setLabel(label);
			newThermometer.GetComponent<ThermometerDecorator>().initialize(coapProxy);
			newThermometer.GetComponent<ThermometerDecorator>().printData();
		}
	}

}
