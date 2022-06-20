using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ThermometerDecorator : MonoBehaviour, SensorInterface
{
    public Text dataText;
    public TextMeshProUGUI labelText;
    private string uri, label;
    private CoapProxy proxy;

    public void initialize(CoapProxy proxy)
	{
        this.proxy = proxy;
        labelText.text = label.ToUpper();
    }

    public void printData()
	{
        string response=proxy.get(uri);
        Dictionary<string,string> dict=JsonParser.parse(response)[0];
        response = dict["v"] + " " + dict["u"];
        dataText.text = response;
	}

    public void setUri(string uri)
	{
        this.uri = uri;
	}

    public void setLabel(string label)
	{
        this.label = label;
	}
}

