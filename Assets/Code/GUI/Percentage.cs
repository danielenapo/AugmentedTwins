using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Percentage : MonoBehaviour, Sensor
{
	private CoapProxy proxy;
	private string uri, label;

	public Text valueText;
	public TextMeshProUGUI labelText;
	public void initialize(CoapProxy proxy, string uri, string label)
	{
		this.uri = uri;
		this.label = label;
		this.proxy = proxy;
		labelText.text = label;
	}

	public void printData()
	{
		string response= proxy.get(uri);
		Dictionary<string, string> dict = JsonParser.parse(response)[0];
		response = dict["v"];
		valueText.text = response + "%";
	}

}
