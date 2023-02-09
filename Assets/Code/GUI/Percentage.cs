using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Percentage : MonoBehaviour, Sensor
{
	private CoapProxy proxy;
	private string uri, label;

	public TextMeshProUGUI labelText, valueText;
	public GameObject indicator;
	public void initialize(CoapProxy proxy, string uri, string label)
	{
		this.uri = uri;
		this.label = label.ToUpper();
		this.proxy = proxy;
		labelText.text = this.label;
	}

	public void printData()
	{
		string response= proxy.get(uri);
		Dictionary<string, string> dict = JsonParser.parse(response)[0];
		response = dict["v"];
		valueText.text = response + "%";
		try
		{
			float perc = float.Parse(response);
			indicator.gameObject.transform.localScale= Vector3.Scale(indicator.gameObject.transform.localScale, new Vector3(1, (perc / 1000f), 1));


		}
		catch
		{
			indicator.gameObject.transform.localScale= Vector3.Scale(indicator.gameObject.transform.localScale,new Vector3(1, 0.5f, 1));

		}

	}

}
