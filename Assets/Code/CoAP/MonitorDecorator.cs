using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MonitorDecorator : MonoBehaviour, SensorInterface
{
    private TextMeshPro textData;
    private CoapProxy proxy;
    //private Dictionary<string,string> uris= new Dictionary<string, string>();
    private List<string> uris = new List<string>();
    private List<string> labels = new List<string>();
    
    

    public void initialize(CoapProxy proxy)
    {
        textData = this.gameObject.GetComponent<TextMeshPro>();
        //proxy = this.gameObject.transform.parent.transform.parent.GetComponent<CoapProxy>();
        this.proxy = proxy;
    }

    /*
     * makes a get for each resource that it's connected to, then prints all the results
     */
    public void printData()
	{
        string displayText = "";

        for(int i=0; i< uris.Count; i++ )
        {
            displayText += "<b>" + labels[i].ToUpper() + "</b>"
                + ": \n"
                + proxy.get(uris[i])
                + "\n";
        }
        Debug.Log("displaying text");
        textData.text = displayText;
    }
    public void setUri(string uri)
	{
        this.uris.Add(uri);
    }

    public void setLabel(string label)
	{
        this.labels.Add(label);
	}





}
