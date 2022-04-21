using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoAPClientTest : MonoBehaviour
{
    Text textData;
    const string pluginName = "com.example.coapplugin.SimpleClient";
    //const string pluginName = "com.example.coapplugin.HelloWorld";
    static AndroidJavaClass _pluginClass;
    static AndroidJavaObject _pluginInstance;
    public string ip, resource;

    public static AndroidJavaClass PluginClass
	{
		get
		{
            if (_pluginClass == null) {
                _pluginClass = new AndroidJavaClass(pluginName);
            }
            return _pluginClass;
        }
    }

    /*public static AndroidJavaObject PluginInstance
    {
        get
        {
            if (_pluginInstance == null)
            {
                _pluginInstance = PluginClass.CallStatic<AndroidJavaObject>("getInstance");
            }
            return _pluginClass;
        }
    }*/

    void Start()
    {
        textData = gameObject.GetComponent<Text>();
        string responseText= callPlugin();
        Debug.Log("[DEB] response retourned: " + responseText);
        textData.text = responseText;
    }

    public string callPlugin()
	{
        Debug.Log("[DEB] calling the plugin");
        /*if (Application.platform == RuntimePlatform.Android)
            return PluginInstance.Call<String>("getResponse", ip, resource);
        else
            return "wrong platform";*/
        return PluginClass.CallStatic<String>("getResponse", ip, resource);

        //textData.text = PluginClass.CallStatic<string>("sayHello") + "\n"+ip+"/"+resource;
    }

}
