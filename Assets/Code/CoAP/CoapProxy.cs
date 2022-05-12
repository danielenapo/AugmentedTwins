using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/*
 * Si occupa di inoltrare le richieste al server CoAP al plugin Android nativo
 * utilizzando i metodi dell'interfaccia CoapManager
 */
public class CoapProxy : MonoBehaviour, CoapManager
{
    const string pluginName = "com.example.coapplugin.PostGetClient";
    static AndroidJavaClass _pluginClass;
    static AndroidJavaObject _pluginInstance;
    private string ip;
    public GameObject monitor;

    public static AndroidJavaClass PluginClass
    {
        get
        {
            if (_pluginClass == null)
            {
                _pluginClass = new AndroidJavaClass(pluginName);
            }
            return _pluginClass;
        }
    }

	public void Start()
	{
		//monitor = this.gameObject.transform.Find("Data").gameObject;
    }

	public string get(string resource)
	{
        string response = PluginClass.CallStatic<string>("get", ip, resource);
        return response;
    }

    public void post(string resource)
	{
        string status = PluginClass.CallStatic<string>("post", ip, resource);
        Debug.Log("[DEB] Status post request: " + status);
        // monitor.GetComponent<Text>().text = get(resource); //after each post, a get is executed to show what changed
        monitor.GetComponent<CoAPClientTest>().printData();//after each post, a get is executed to show what changed
    }

    public Dictionary<string, string> discover()
    {
        string response = PluginClass.CallStatic<string>("discover", ip);
        Dictionary<string, string> resourcesDict = new Dictionary<string, string>();
        //response = response.Substring(1, response.Length - 2);
        //response= String.Concat(response.Where(c => !Char.IsWhiteSpace(c)));
        resourcesDict = response.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
               .Select(part => part.Split('='))
               .ToDictionary(split => split[0], split => split[1]);

        //DEBUG
        Debug.Log("[DEB] DISCOVER TO DICTIONARY -> ");
        foreach (KeyValuePair<string, string> entry in resourcesDict)
        {
            Debug.Log("KEY: " + entry.Key + " ,VALUE: " + entry.Value);
        }

        return resourcesDict;
    }

    public void setIp(String ip)
	{
        this.ip = ip;
	}
}

/*
 * Interfaccia del proxy pattern che indica i metodi utilizzabili.
 * Questa interfaccia viene implementata da CoapProxy (su unity) e dal client sul plugin nativo 
 */
public interface CoapManager
{
    public string get(string resource);
    public void post(string resource);
    public Dictionary<string, string> discover();
}