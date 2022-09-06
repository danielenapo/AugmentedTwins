using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ThermometerDecorator : MonoBehaviour, Sensor
{
    public Text dataText;
    public TextMeshProUGUI labelText;
    private string uri, label;
    private CoapProxy proxy;

    public void initialize(CoapProxy proxy, string uri, string label)
	{
        this.uri = uri;
        this.proxy = proxy;
        this.label = label;
        labelText.text = label.ToUpper();
    }

    public void printData()
	{
        string response=proxy.get(uri);
        Dictionary<string,string> dict=JsonParser.parse(response)[0];
        response = dict["v"];
        dataText.text = response + dict["u"];
        

    }

}

