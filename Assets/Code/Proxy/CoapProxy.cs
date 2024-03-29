using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

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
    private int port;

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


	public string get(string resource)
	{
        string response = PluginClass.CallStatic<string>("get", ip, resource);
        return response;
    }

    public void post(string resource, string payload)
	{
        
        string status = PluginClass.CallStatic<string>("post", ip, resource, payload);
        Debug.Log("[DEB] Status post request: " + status);
    }

    /*
     * chiama il server per effettuare la discovery
     * gli ritorna una stringa di questo tipo:
     * ogni risorsa � separata da ";" ed � scritta cos�: /nomeRisorsa=rt,title,if
    */
    public Dictionary<string, string> discover()
    {
        string response = PluginClass.CallStatic<string>("discover", ip);
        Debug.Log("[DEB] discovery response from plugin -> " + response);
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

    public static List<Dictionary<string, string>> parse(string response)
    {
        List<Dictionary<string, string>> parsedResponse;
        parsedResponse = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(response);
        return parsedResponse;
    }

}

/*
 * Interfaccia del proxy pattern che indica i metodi utilizzabili.
 * Questa interfaccia viene implementata da CoapProxy (su unity) e dal client sul plugin nativo 
 */
public interface CoapManager
{
    public string get(string resource);
    public void post(string resource, string payload);
    public Dictionary<string, string> discover();
}