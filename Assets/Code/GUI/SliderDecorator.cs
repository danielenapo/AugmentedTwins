using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Newtonsoft.Json;
using TMPro;

public class SliderDecorator : MonoBehaviour, IPointerUpHandler, Actuator
{
	public Text labelText;
	public Text textValue;

	private CoapProxy proxy;
	private string uri;
	private Slider slider;

	public void initialize(CoapProxy proxy, string uri, string label)
	{
		this.uri = uri;
		labelText.text = label;
		this.proxy = proxy;
		slider = this.gameObject.GetComponent<Slider>();
		slider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
		setValue(); //inizialmente imposta il valore prendendolo dal server
	}

	

	public void OnPointerUp(PointerEventData eventData)
	{
		proxy.post(uri, slider.value.ToString());
	}

	public void ValueChangeCheck()
	{
		textValue.text = slider.value.ToString();
	}

	public void setValue()
	{
		Dictionary<string, string> records = JsonParser.parse(proxy.get(uri))[0];
		try
		{
			if (records.ContainsKey("v"))
			{
				Debug.Log("SLIDER GET -> " + records["v"]);
				this.slider.value = float.Parse(records["v"]);
				textValue.text = records["v"];
			}
		}
		catch
		{
			this.slider.value = 0f;
			textValue.text = "0";
		}
	}

	//slider doesen't need a sensor because it's numeric value is shown directly
	public void setSensor(GameObject sensor)
	{
		return;
	}
}
