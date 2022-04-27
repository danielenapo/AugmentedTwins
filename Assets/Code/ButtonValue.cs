using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonValue : MonoBehaviour
{
	const string pluginName = "com.example.coapplugin.PostGetClient";
	static AndroidJavaClass _pluginClass;
	static AndroidJavaObject _pluginInstance;
	public string ip, resource;

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

	void Start()
	{
		Button button = this.gameObject.GetComponent<Button>();
		button.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick()
	{
		string response = PluginClass.CallStatic<string>("request", ip, resource, "post");
		Debug.Log("[DEB] RISULTATO POST -> " + response);
	}

}
