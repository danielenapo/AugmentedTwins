using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class GenericButton : MonoBehaviour, Actuator
{
	private CoapProxy proxy;
	private string uri;
	public Text labelText;
	private GameObject monitor;


	public void initialize(CoapProxy proxy, string uri, string label)
	{
		this.uri = uri;
		labelText.text = label;
		this.proxy = proxy;
		Button button = this.gameObject.GetComponent<Button>();
		button.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick()
	{
		proxy.post(uri, null);
		if (monitor != null)
			monitor.GetComponent<Sensor>().printData();

	}

	public void setValue() //non implementata per il bottone (viene visualizzato sul monitor)
	{
		return;
	}

	public void setSensor(GameObject sensor)
	{
		this.monitor = sensor;
	}
}

