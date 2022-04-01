using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*modifica un prefab vuoto istanziandoci i prefab gia fatti delle singole interfacce (es bottone, display, slider, ...)
 * in base ai valori della discovery CoAP
 */
public class InterfaceBuilder : MonoBehaviour
{
	private Dictionary<string, GameObject> interfaces = new Dictionary<string, GameObject>();
	public GameObject g1 , g2;

	void Awake()
	{
		Debug.Log("started [deb]");
		getInterfaces();
		instantiateInterfaces();
	}
	// function to retrieve interfaces associated to the image (available CoAP resources), and fills the interfaces list
	public void getInterfaces()
	{
		//dovrebbe chiamare una classe che fa le discovery
		interfaces.Add("Data", g1);
		interfaces.Add("Button", g2);
		Debug.Log("added interfaces [deb]");

	}

	//function that instantiates prefabs
	public void instantiateInterfaces()
	{
		Vector3 position = this.gameObject.transform.position;
		float offset = 0;
		foreach( var element in interfaces){
			position.x += offset*10;
			GameObject newGameobject= Instantiate(element.Value, position ,this.gameObject.transform.rotation);
			newGameobject.transform.parent = this.gameObject.transform;
			offset+=0.018f;
			Debug.Log("Instantiated " + element.Key+ " at position: "+ position +"[deb]");
		}
	}
}
