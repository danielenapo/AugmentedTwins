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
	public TextMeshProUGUI textValue;

	private CoapProxy proxy;
	private string uri;
	private Slider slider;

	public void initialize(CoapProxy proxy)
	{
		this.proxy = proxy;
		slider = this.gameObject.GetComponent<Slider>();
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
		Debug.Log("SLIDER GET -> " + records.ToString());
		try
		{
			if (records.ContainsKey("v"))
			{
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

	public void setUri(string uri)
	{
		this.uri = uri;
	}

	public void setLabel(string label)
	{
		labelText.text = label;
	}

}
