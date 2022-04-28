using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

/*
 * fill the canvas with different types of interfaces (for example button, data, slider, ...)
 * depending on the values obtained by the CoAP discovery
 */
public class CanvasBuilder : MonoBehaviour
{
	private Dictionary<string, GameObject> interfaces = new Dictionary<string, GameObject>();
	private ArrayList elements = new ArrayList();
	public GameObject data, button, slider;
	public Text labelText;
	public float offsetSize;
	private float sizeX;
	//private ARTrackedImageManager imageManager;


	//CI STAREBBE FARE UN TEMPLATE/FACADE PATTERN
	/*	void Awake()
	{
		//sizeX = 0.142f;
		offsetSize /= 5;
		getInterfaces();
		setLabelText();
		instantiateInterfaces();
		this.gameObject.transform.Rotate(88, 0, 0);
		//imageManager=Camera.main.GetComponent<ARTrackedImageManager>();
		//resize();

	}
	public void initialize(float sizeX) //testing
	{ }*/


	public void initialize(float sizeX)
	{
		this.sizeX = sizeX;
		offsetSize /= 5;
		addInterfaces();
		setLabelText();
		instantiateInterfaces();
		resize();
	}
	// function to retrieve interfaces associated to the image (available CoAP resources), and fills the interfaces list
	public void addInterfaces()
	{
		//dovrebbe chiamare una classe che fa le discovery
		//elements.Add(data);
		elements.Add(button);
		elements.Add(slider);
		//Debug.Log("added interfaces [deb]");
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
		Vector3 thisPosition = this.gameObject.transform.position;
		//thisPosition.y += offsetSize;
		//float offset = offsetSize;
		foreach (GameObject element in elements)
		{
			GameObject newUIElement = Instantiate(element, thisPosition, this.gameObject.transform.rotation);
			newUIElement.transform.localScale= new Vector3(0.5f,0.5f,1)/100;
			newUIElement.transform.parent = this.gameObject.transform;
			thisPosition.y -= offsetSize;

		}
	}

	//rerizes canvas according to the image size
	public void resize()
	{
		this.gameObject.transform.Rotate(90, 0, 0);
		//Debug.Log("(DEB) width value: " + sizeX);
		this.gameObject.transform.localScale=Vector3.Scale(this.gameObject.transform.localScale,new Vector3(sizeX, sizeX, sizeX));
		//Debug.Log("(DEB) resized! -> scale: " + this.gameObject.transform.localScale);

	}


}
