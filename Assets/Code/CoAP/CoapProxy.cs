using System.Collections;
using System.Collections.Generic;
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
    public string ip;
    private GameObject monitor;

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
		monitor = this.gameObject.transform.Find("Data").gameObject;
    }

	public string get(string resource)
	{
        Debug.Log("[DEB] got a get call");
        string response = PluginClass.CallStatic<string>("request", ip, resource, "get");
        return response;
    }

    public void post(string resource)
	{
        string status = PluginClass.CallStatic<string>("request", ip, resource, "post");
        Debug.Log("[DEB] Status post request: " + status);
        monitor.GetComponent<Text>().text = get(resource);
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
}