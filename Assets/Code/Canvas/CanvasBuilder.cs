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
	private string label, protocol;
	private int port;
	private Factory factory;

	public GameObject monitor;
	public GameObject labelText;
	public GameObject actuatorBackground;

	private void Awake()
	{
		coapProxy = this.gameObject.GetComponent<CoapProxy>();
		factory= this.gameObject.GetComponent<Factory>();
	}

	public void initialize(float sizeX, string ip, int port, string protocol,  string label)
	{
		this.port = port;
		this.protocol = protocol;
		this.label = label;
		coapProxy.setIp(ip);
		this.sizeX = sizeX;
		discovery();
		instantiateInterfaces();
		resize();
		setLabelText();

	}

	/*
	 * Il proxy ritorna un dictionary di tipo:
	 * key-> nome risorsa (es "/temperature")
	 * value -> elenco attributi separati da "," (es "temp,internal temp,core.a,")
	 *											 (		RT,	    TITLE,		 IF)
	 */
	public void discovery()
	{

		discoveryDict = coapProxy.discover();
	}

	/*
	 * Istanzia le interfacce ispezionando il risultato della discovery, chiamando le classi Factory
	 * in ordine gli attributi nel campo value di discoveryDict da controllare sono:
	 * - RT: per distinguere il tipo di prefab da istanziare (es. temp per il termometro, btn bottoni, slider, ...)
	 * - TITLE: lo uso come label dell'interfaccia
	 * - IF: per distinguere tra sensore (core.a) e attuatore (core.s)
	 * NOTA: per i sensori, se non si conosce il valore dentro RT, lo aggiungo nel monitor
	*/
	public void instantiateInterfaces()
	{
		factory.initialize(discoveryDict, coapProxy);
		monitor.GetComponent<MonitorDecorator>().initialize(coapProxy, null, null);
		Vector3 thisPosition = this.gameObject.transform.position;
		foreach (KeyValuePair<string, string> entry in discoveryDict)
		{
			string ifType, label, rt, uri;
			
			rt= entry.Value.Split(',')[0];
			label = entry.Value.Split(',')[1];
			ifType = entry.Value.Split(',')[2];
			uri = entry.Key.Substring(1);   //tolgo la "/" all'inizio del nome della risorsa
			if (ifType == "core.a")
			{
				factory.instantiateActuator(rt, uri, label);
				factory.instantiateSensor(rt, uri, label); //an actuator supports GET, POST and PUT operations, so it can be shown both as a button and a special sensor
			}
			else if (ifType == "core.s")
			{
				factory.instantiateSensor(rt, uri, label);
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
