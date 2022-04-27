using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoAPClientTest : MonoBehaviour
{
    Text textData;
    const string pluginName = "com.example.coapplugin.PostGetClient";
    static AndroidJavaClass _pluginClass;
    static AndroidJavaObject _pluginInstance;
    public string ip, resource;
    //private TaskCompletionSource<string> responseText;

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

    void Start()
    {
        textData = gameObject.GetComponent<Text>();
        /*responseText = new TaskCompletionSource<string>();
        //fa partire la chiamata al plugin
        Task.Run(async () =>
         {
             Debug.Log("DEB task started");
             responseText.SetResult(await callPlugin());
         });
        //quando il plugin ha smesso di eseguire, imposto il valore di ritorno come testo del prefab
        responseText.Task.ConfigureAwait(true).GetAwaiter().OnCompleted(() =>
        {
            Debug.Log("DEB Task completed");
            textData.text = responseText.Task.Result;
        });*/
        textData.text = callPlugin();

    }

	/*public async Task<string> callPlugin()
	{
        string response = PluginClass.CallStatic<String>("getResponse", ip, resource);
        //Debug.Log("[DEB] response retourned: " + responseText);
        return response;
    }*/

	string callPlugin()
	{
        string response = PluginClass.CallStatic<String>("request", ip, resource, "get");
        //Debug.Log("[DEB] response retourned: " + responseText);
        return response;
    }

}
