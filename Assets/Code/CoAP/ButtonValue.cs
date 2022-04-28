using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonValue : MonoBehaviour
{
	private CoapProxy proxy;

	void Start()
	{
		proxy = this.gameObject.transform.parent.GetComponent<CoapProxy>();
		Button button = this.gameObject.GetComponent<Button>();
		button.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick()
	{
		proxy.post("coffee");
	}

}
