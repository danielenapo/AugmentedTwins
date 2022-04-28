using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoAPClientTest : MonoBehaviour
{
    Text textData;
    private CoapProxy proxy;
    
    

    void Start()
    {
        textData = gameObject.GetComponent<Text>();
        proxy = this.gameObject.transform.parent.GetComponent<CoapProxy>();
        textData.text=coapGet();

    }

    public string coapGet()
	{
        return proxy.get("coffee");
	}




}
