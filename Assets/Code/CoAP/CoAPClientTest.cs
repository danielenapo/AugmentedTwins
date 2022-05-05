using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoAPClientTest : MonoBehaviour
{
    private Text textData;
    private CoapProxy proxy;
    private List<string> uris= new List<string>();
    
    

    public void initialize(CoapProxy proxy)
    {
        textData = this.gameObject.GetComponent<Text>();
        //proxy = this.gameObject.transform.parent.transform.parent.GetComponent<CoapProxy>();
        this.proxy = proxy;
    }
    /*
     * makes a get for each resource that it's connected to, then prints all the results
     */
    public void printData()
	{
        string displayText = "";

        foreach (string uri in uris)
        {
            displayText += uri.ToUpper()
                + ": \n"
                + proxy.get(uri)
                + "\n";
        }
        textData.text = displayText;
    }
    public void setUri(string uri)
	{
        Debug.Log("added uri " + uri+ " to the monitor");
        uris.Add(uri);
	}





}
