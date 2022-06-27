using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

/* CLASS DESCRIPTION:
 * fill the canvas with different types of interfaces (for example button, data, slider, ...)
 * depending on the values obtained by the CoAP discovery
 */
public class CanvasBuilder : MonoBehaviour
{
	private Dictionary<string, GameObject> interfaces = new Dictionary<string, GameObject>();
	private float sizeX;
	private Dictionary<string, string> discoveryDict;
	private CoapProxy coapProxy;
	private string label;
	private Factory factory;

	public GameObject monitor;
	public GameObject labelText;
	public GameObject actuatorBackground;

	private void Awake()
	{
		coapProxy = this.gameObject.GetComponent<CoapProxy>();
		factory= this.gameObject.GetComponent<Factory>();
	}

	public void initialize(float sizeX, string ip, string label)
	{
		this.label = label;
		coapProxy.setIp(ip);
		this.sizeX = sizeX;
		discovery();
		instantiateInterfaces();
		resize();
		setLabelText();

	}


	public void discovery()
	{

		discoveryDict = coapProxy.discover();
	}

	public void instantiateInterfaces()
	{
		factory.initialize(discoveryDict, coapProxy);
		monitor.GetComponent<MonitorDecorator>().initialize(coapProxy);
		Vector3 thisPosition = this.gameObject.transform.position;
		foreach (KeyValuePair<string, string> entry in discoveryDict)
		{
			string ifType, label, uri;
			label = entry.Value.Split(',')[0];
			ifType = entry.Value.Split(',')[1];
			uri = entry.Key.Substring(1);

			if (ifType == "core.a.btn")
			{
				factory.instantiateActuator("button", uri, label);
			}
			if (ifType == "core.a.slider")
			{
				factory.instantiateActuator("slider", uri, label);
			}

			if (ifType == "core.s.temp")
			{
				factory.instantiateSensor("thermometer", uri, label);
			}
			else
			{
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
	public void setLabelText()
	{
		labelText.GetComponent<TextMeshPro>().text = label;
		float height = actuatorBackground.GetComponent<RectTransform>().rect.height;
		labelText.transform.position.Set(0, height + 10, 0);
	}

}
