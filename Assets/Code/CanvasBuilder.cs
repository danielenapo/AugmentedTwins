using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * riempie il canvas con diversi tipi di prefab UI (es bottone, slider, data, ...),
 * in base ai valori della discovery CoAP
 */
public class CanvasBuilder : MonoBehaviour
{
	private Dictionary<string, GameObject> interfaces = new Dictionary<string, GameObject>();
	private ArrayList elements = new ArrayList();
	public GameObject data, button, slider;
	public Text labelText;
	public float offsetSize;

	//CI STAREBBE FARE UN TEMPLATE/FACADE PATTERN
	void Awake()
	{
		offsetSize /= 100;
		getInterfaces();
		setLabelText();
		instantiateInterfaces();
		this.gameObject.transform.Rotate(88, 0, 0);
	}
	// function to retrieve interfaces associated to the image (available CoAP resources), and fills the interfaces list
	public void getInterfaces()
	{
		//dovrebbe chiamare una classe che fa le discovery
		elements.Add(data);
		elements.Add(button);
		elements.Add(slider);
		Debug.Log("added interfaces [deb]");
	}

	//sets labelText valuem and changes font size according to the string length
	public void setLabelText()
	{
		labelText.text = "Coffee Machine";
		labelText.fontSize = 11;
	}

	//function that instantiates prefabs
	public void instantiateInterfaces()
	{
		//Vector3 position = this.gameObject.transform.position;
		float offset = offsetSize;
		foreach (GameObject element in elements)
		{
			//position.x += offset * 10;
			GameObject newUIElement = Instantiate(element, new Vector3(0, offset, 0), this.gameObject.transform.rotation);
			newUIElement.transform.localScale= new Vector3(0.5f,0.5f,1)/500;
			//newGameobject.transform.Rotate(new Vector3(90, 0, 0));
			newUIElement.transform.parent = this.gameObject.transform;
			offset -= offsetSize;
			//Debug.Log("Instantiated " + element.Key + " at position: " + position + "[deb]");
		}
	}
}
