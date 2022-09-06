using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
public class StaticCanvasBuilder : MonoBehaviour
{
	private Dictionary<string, GameObject> interfaces = new Dictionary<string, GameObject>();
	private float sizeX;
	private Dictionary<string, string> discoveryDict;
	private string label;

	public GameObject monitor;
	public GameObject labelText;
	public GameObject actuatorBackground, sensorBackground;
	public GameObject termomether, water, capsules;
	public GameObject button, slider;

	//test percentage
	public GameObject PercIndicator;

	public void Awake()
	{
		initialize();
	}


	public void initialize()
	{
		this.label = "Coffee Machine";
		this.sizeX = 1;
		instantiateInterfaces();
		resize();
		setLabelText();

	}

	/*
	 * Il proxy ritorna un dictionary di tipo:
	 * key-> nome risorsa (es "/temperature")
	 * value -> elenco attributi separati da "," (es "temp,internal temp,core.a,")
	 *											 (		RT,	    TITLE,		 IF)
	 */


	/*
	 * Istanzia le interfacce ispezionando il risultato della discovery, chiamando le classi Factory
	 * in ordine gli attributi nel campo value di discoveryDict da controllare sono:
	 * - RT: per distinguere il tipo di prefab da istanziare (es. temp per il termometro, btn bottoni, slider, ...)
	 * - TITLE: lo uso come label dell'interfaccia
	 * - IF: per distinguere tra sensore (core.a) e attuatore (core.s)
	 * NOTA: per i sensori, se non si conosce il valore dentro RT, lo aggiungo nel monitor
	*/
	public void instantiateInterfaces()
	{
		var position = this.gameObject.transform.position;

		GameObject newSensor;

		//water
		/*newSensor = Instantiate(water, position, this.gameObject.transform.rotation);
		newSensor.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f) / 50;
		newSensor.transform.parent = sensorBackground.gameObject.transform;*/
		PercIndicator.gameObject.transform.localScale=Vector3.Scale(PercIndicator.gameObject.transform.localScale, new Vector3(1,78.0f/100f,1));



		GameObject newActuator;
		newActuator = Instantiate(button, position, this.gameObject.transform.rotation);
		newActuator.transform.localScale = new Vector3(0.5f, 0.5f, 1) / 100;
		newActuator.transform.parent = actuatorBackground.gameObject.transform;

	}

	//rerizes canvas according to the image size
	public void resize()
	{
		
		//this.gameObject.transform.Rotate(90, 0, 0);
		this.gameObject.transform.localScale = Vector3.Scale(this.gameObject.transform.localScale, new Vector3(sizeX, sizeX, sizeX));

	}
	public void setLabelText()
	{
		labelText.GetComponent<TextMeshPro>().text = label;
		float height = actuatorBackground.GetComponent<RectTransform>().rect.height;
		labelText.transform.position.Set(0, height + 10, 0);
	}

}
