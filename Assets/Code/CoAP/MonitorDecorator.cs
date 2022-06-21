using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MonitorDecorator : MonoBehaviour, SensorInterface
{
    private TextMeshProUGUI textData;
    private CoapProxy proxy;
    //private Dictionary<string,string> uris= new Dictionary<string, string>();
    private List<string> uris = new List<string>();
    private List<string> labels = new List<string>();
    
    

    public void initialize(CoapProxy proxy)
    {
        textData = this.gameObject.GetComponent<TextMeshProUGUI>();
        //proxy = this.gameObject.transform.parent.transform.parent.GetComponent<CoapProxy>();
        this.proxy = proxy;
    }

    /*
     * makes a get for each resource that it's connected to,
     * then parses the json SenML response and iterates through each record
     * and for each record it displays its data (based on the key, it will only 
     * display name, value and unit, because some values are not useful to the user)
     * SenML KEYS:
     * - n = name
     * - v = value (generic)
     * - vb = boolean value
     * - vs = string value
     * - u = unit
     */
    public void printData()
	{
        string displayText = "";

        for(int i=0; i< uris.Count; i++ )
        {
            List<Dictionary<string, string>> records;
            records= JsonParser.parse(proxy.get(uris[i]));
            displayText += "<b>" + labels[i].ToUpper() + "</b>: \n";
            bool isPrint = false; //per non stampare linee vuote
            foreach(Dictionary<string,string> record in records)
			{
                foreach(KeyValuePair<string, string> entry in record)
				{
					switch (entry.Key)
					{
                        case "n":
                            displayText += entry.Value + ": ";
                            isPrint = true;
                            break;
                        case "v":
                        case "vb":
                        case "vs":
                        case "u":
                            displayText += entry.Value+" ";
                            isPrint = true;
                            break;
                    }
                }
                if(isPrint)
                    displayText += "\n";
                isPrint = false;
			}
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
