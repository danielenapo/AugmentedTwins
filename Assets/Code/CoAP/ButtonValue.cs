using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonValue : MonoBehaviour
{
	private CoapProxy proxy;
	private string uri;
	public Text labelText;

	void Start()
	{
		proxy = this.gameObject.transform.parent.GetComponent<CoapProxy>();
		Button button = this.gameObject.GetComponent<Button>();
		button.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick()
	{
		proxy.post(uri);
	}

	public void setUri(string uri)
	{
		this.uri = uri;
	}

	public void setLabel(string label)
	{
		labelText.text = label;
	}
}
