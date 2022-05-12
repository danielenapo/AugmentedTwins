using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoAPClientTest : MonoBehaviour
{
    private TextMeshPro textData;
    private CoapProxy proxy;
    private Dictionary<string,string> uris= new Dictionary<string, string>();
    
    

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

        foreach (KeyValuePair<string,string> resource in uris)
        {
            displayText += "<b>" + resource.Value.ToUpper() + "</b>"
                + ": \n"
                + proxy.get(resource.Key)
                + "\n";
        }
        Debug.Log("displaying text");
        textData.text = displayText;
    }
    public void setUri(string uri,string label)
	{
        uris.Add(uri,label);
	}





}
