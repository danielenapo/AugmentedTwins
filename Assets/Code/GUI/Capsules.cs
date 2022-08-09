using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Capsules : MonoBehaviour, Sensor
{
    public TextMeshProUGUI labelText;
    private string uri, label;
    private CoapProxy proxy;

    public void initialize(CoapProxy proxy, string uri, string label)
    {
        this.proxy = proxy;
        this.uri = uri;
        this.label = label;
        labelText.text = label.ToUpper();
    }

    public void printData()
    {
        return;
    }

}
