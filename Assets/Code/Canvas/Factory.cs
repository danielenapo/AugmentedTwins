/*
	Class called by CanvasBuilder, it's responsible of the instantiation of single objects to attach to the canvas.
	Has 2 methods (instantiateActuator and instantiateSensor), depending on the type of object
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    public GameObject button, slider, monitor, thermometer, switchButton, capsules, percentage;
	public GameObject actuatorBackground, sensorBackground, monitorBackground;
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


		if (rt.Contains("slider"))
			newActuator = Instantiate(slider, thisPosition, this.gameObject.transform.rotation);
		else if (rt.Contains("switch"))
			newActuator = Instantiate(switchButton, thisPosition, this.gameObject.transform.rotation);
		else
		{
			newActuator = Instantiate(button, thisPosition, this.gameObject.transform.rotation); //default actuator is a button
			newActuator.GetComponent<Actuator>().setSensor(instantiateSensor(rt, uri, label)); //an actuator supports GET, POST and PUT operations, so it can be shown both as a button and a special sensor (an actuator can be paired with a monitor)
		}
		Debug.Log("instantiated "+rt);
		newActuator.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f) / 100;
		newActuator.transform.parent = actuatorBackground.gameObject.transform;
		newActuator.GetComponent<Actuator>().initialize(coapProxy, uri, label);

	}


	public GameObject instantiateSensor(string rt, string uri, string label) {
		bool isMonitor = false;
		GameObject newSensor;
		Vector3 position = sensorBackground.gameObject.transform.position;
		if (rt == "temp3d")
			newSensor = Instantiate(thermometer, new Vector3(position.x, position.y + offset, position.z), this.gameObject.transform.rotation);
		else if (rt.Contains("capsules"))
			newSensor = Instantiate(capsules, new Vector3(position.x, position.y + offset, position.z), this.gameObject.transform.rotation);
		else if (rt.Contains("percentage"))
			newSensor = Instantiate(percentage, new Vector3(position.x, position.y + offset, position.z), this.gameObject.transform.rotation);
		else
		{
			newSensor = Instantiate(monitor, new Vector3(position.x, position.y + offset, position.z), this.gameObject.transform.rotation);
			isMonitor = true; //if it's a monitor, it has to be a monitorBackground children and not sensorBackground
		}


		if (isMonitor == false)
		{
			newSensor.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f) / 50;
			newSensor.transform.parent = sensorBackground.gameObject.transform;
		}
		else
		{
			newSensor.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f) / 100;
			newSensor.transform.parent = monitorBackground.gameObject.transform;
		}
		newSensor.GetComponent<Sensor>().initialize(coapProxy, uri, label);
		newSensor.GetComponent<Sensor>().printData();
		offset -= offsetSize;

		return newSensor;

	}

}
