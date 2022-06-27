using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ButtonDecorator : MonoBehaviour, Actuator
{
	private CoapProxy proxy;
	private string uri;
	public Text labelText;

	public void initialize(CoapProxy proxy)
	{
		this.proxy = proxy;
		Button button = this.gameObject.GetComponent<Button>();
		button.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick()
	{
		proxy.post(uri, null);

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

