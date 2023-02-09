using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Capsules : MonoBehaviour, Sensor
{
    public TextMeshProUGUI labelText;
    private string uri, label;
    private CoapProxy proxy;
    public TextMeshProUGUI shortC, normalC, longC;

    public void initialize(CoapProxy proxy, string uri, string label)
    {
        this.proxy = proxy;
        this.uri = uri;
        this.label = label;
        labelText.text = label.ToUpper();
    }

    public void printData()
    {
        string response = proxy.get(uri);
        List<Dictionary<string, string>> listDict = JsonParser.parse(response);
        foreach(Dictionary<string, string>  listElement in listDict)
		{
            try
            {
                if (listElement["n"] == "short_coffee")
                    shortC.text = listElement["v"];
                else if (listElement["n"] == "medium_coffee")
                    normalC.text = listElement["v"];
                else if (listElement["n"] == "long_coffee")
                    longC.text = listElement["v"];
            }
			catch { }
        }
    }

}
