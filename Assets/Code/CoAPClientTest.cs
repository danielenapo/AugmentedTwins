using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoAPClientTest : MonoBehaviour
{
    Text textData;
    const string pluginName = "com.example.coapplugin.GETClient";
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

    public static AndroidJavaObject PluginInstance
    {
        get
        {
            if (_pluginInstance == null)
            {
                _pluginInstance = PluginClass.CallStatic<AndroidJavaObject>("getInstance");//CORREGGI TYPO getinstaNce
            }
            return _pluginClass;
        }
    }

    void Start()
    {
        // Find the Text UI object attached to the game object this script is attached to
        textData = gameObject.GetComponent<Text>();
        // Make the call using JNI to the Java Class and write out the response (or write 'Invalid Response From JNI' if there was a problem).
        //textData.text = CaptiveReality.Jni.Util.StaticCall("sayHello", "Invalid Response From JNI", "com.example.coapplugin.HelloWorld");
        Debug.Log("[DEB] calling the plugin");
        string responseText=PluginInstance.Call<string>("getResponse", ip, resource);
        textData.text = responseText;
        //textData.text = PluginClass.CallStatic<string>("sayHello") + "\n"+ip+"/"+resource;
    }


}
