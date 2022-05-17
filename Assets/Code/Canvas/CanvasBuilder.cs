using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

/*
 * fill the canvas with different types of interfaces (for example button, data, slider, ...)
 * depending on the values obtained by the CoAP discovery
 */
public class CanvasBuilder : MonoBehaviour
{
	private Dictionary<string, GameObject> interfaces = new Dictionary<string, GameObject>();
	private float sizeX;
	private Dictionary<string, string> discoveryDict;
	private CoapProxy coapProxy;
	private string ip;

	public GameObject button, slider, monitor, thermometer;
	public TextMeshPro labelText;
	public float offsetSize;


	//CI STAREBBE FARE UN TEMPLATE/FACADE PATTERN
	/*	void Awake()
	{
		//sizeX = 0.142f;
		offsetSize /= 5;
		getInterfaces();
		setLabelText();
		instantiateInterfaces();
		this.gameObject.transform.Rotate(88, 0, 0);
		//imageManager=Camera.main.GetComponent<ARTrackedImageManager>();
		//resize();

	}
	public void initialize(float sizeX) //testing
	{ }*/

	private void Awake()
	{
		coapProxy = this.gameObject.GetComponent<CoapProxy>();
	}

	public void initialize(float sizeX, string ip)
	{
		this.ip = ip;
		coapProxy.setIp(ip);
		this.sizeX = sizeX;
		offsetSize /= 5;
		addInterfaces();
		setLabelText();
		instantiateInterfaces();
		resize();
	}
	
	/* 
	 * function to retrieve interfaces associated to the image (available CoAP resources), 
	 * and fills the interfaces list after doing a coap discovery, it associates to each actuator a button.
	*/
	public void addInterfaces()
	{
		discoveryDict = coapProxy.discover();
	}

	public void setLabelText()
	{
		labelText.text = "Coffee Machine";
	}

	public void instantiateInterfaces()
	{
		Vector3 thisPosition = this.gameObject.transform.position;
		monitor.GetComponent<MonitorDecorator>().initialize(coapProxy);
		foreach (KeyValuePair<string, string> entry in discoveryDict)
		{
			string ifType, label, uri;
			label = entry.Value.Split(',')[0];
			ifType = entry.Value.Split(',')[1];
			uri = entry.Key.Substring(1);

			if (ifType == "core.a.btn")
			{
				GameObject newButton = Instantiate(button, thisPosition, this.gameObject.transform.rotation);
				newButton.transform.localScale = new Vector3(0.5f, 0.5f, 1) / 100;
				newButton.transform.parent = this.gameObject.transform;
				newButton.GetComponent<ButtonDecorator>().setUri(uri);
				newButton.GetComponent<ButtonDecorator>().setLabel(label);
				newButton.GetComponent<ButtonDecorator>().initialize(coapProxy);
				thisPosition.y -= offsetSize;
			}
			if (ifType == "core.a.slider")
			{
				GameObject newSlider = Instantiate(slider, thisPosition, this.gameObject.transform.rotation);
				newSlider.transform.localScale = new Vector3(0.5f, 0.5f, 1) / 100;
				newSlider.transform.parent = this.gameObject.transform;
				//newSlider.GetComponent<ButtonDecorator>().setUri(uri);
				//newSlider.GetComponent<ButtonDecorator>().setLabel(label);
				thisPosition.y -= offsetSize;
			}

			if (ifType == "core.s.temp")
			{
				GameObject newThermometer = Instantiate(thermometer, thisPosition, this.gameObject.transform.rotation);
				newThermometer.transform.localScale = new Vector3(0.5f, 0.5f, 1) / 100;
				newThermometer.transform.parent = this.gameObject.transform;
				newThermometer.GetComponent<ThermometerDecorator>().setUri(uri);
				newThermometer.GetComponent<ThermometerDecorator>().setLabel(label);
				newThermometer.GetComponent<ThermometerDecorator>().initialize(coapProxy);
				newThermometer.GetComponent<ThermometerDecorator>().printData();

			}
			else { 
				monitor.GetComponent<MonitorDecorator>().setUri(uri);
				monitor.GetComponent<MonitorDecorator>().setLabel(label);
			}
		}
		monitor.GetComponent<MonitorDecorator>().printData();
	}

	//rerizes canvas according to the image size
	public void resize()
	{
		this.gameObject.transform.Rotate(90, 0, 0);
		this.gameObject.transform.localScale=Vector3.Scale(this.gameObject.transform.localScale,new Vector3(sizeX, sizeX, sizeX));

	}


}
