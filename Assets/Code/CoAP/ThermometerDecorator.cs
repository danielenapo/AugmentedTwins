using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ThermometerDecorator : MonoBehaviour, SensorInterface
{
    public Text dataText;
    public TextMeshPro labelText;
    private string uri, label;
    private CoapProxy proxy;

    public void initialize(CoapProxy proxy)
	{
        this.proxy = proxy;
        labelText.text = label.ToUpper();
    }

    public void printData()
	{
        string tempValue=proxy.get(uri);
        dataText.text = tempValue;
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

