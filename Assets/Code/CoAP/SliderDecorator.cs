using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SliderDecorator : MonoBehaviour, IPointerUpHandler, Actuator
{
	public Text labelText;

	private CoapProxy proxy;
	private string uri;
	private Slider slider;

	public void initialize(CoapProxy proxy)
	{
		this.proxy = proxy;
		slider = this.gameObject.GetComponent<Slider>();
	}

	

	public void OnPointerUp(PointerEventData eventData)
	{
		proxy.post(uri, slider.value.ToString());
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
