using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataElement : MonoBehaviour, Sensor
{
	private CoapProxy proxy;
	private string uri, label;

	public TMPro.TextMeshProUGUI valueText;
	public void initialize(CoapProxy proxy, string uri, string label)
	{
		this.uri = uri;
		this.label = label.ToUpper();
		this.proxy = proxy;
	}

	public void printData()
	{
		Debug.Log("calling GET (proxy) from DATAELEMENT!!!");
		string response = proxy.get(uri);
		Dictionary<string, string> dict = JsonParser.parse(response)[0];
		try
		{
			response = dict["v"];
		}
		catch
		{
			try
			{
				response = dict["vs"];
			}
			catch
			{
				response = dict["vb"];
			}
		}
		if(response!=null)
			valueText.text = "<b>"+ label + ": </b>"+ response;
	

	}

}
