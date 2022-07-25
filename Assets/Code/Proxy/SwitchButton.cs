using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class SwitchButton : MonoBehaviour, Actuator
{
	private CoapProxy proxy;
	private string uri;
	private bool value;
	private Button button;

	public Text labelText;
	public Image buttonImage;


	public void initialize(CoapProxy proxy, string uri, string label)
	{
		this.proxy = proxy;
		this.uri = uri;
		labelText.text = label;
		button = this.gameObject.GetComponent<Button>();
		setValue();
		button.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick()
	{
		//aggiorno il valore booleano
		if (value == true)
			value = false;
		else
			value = true;
		//mando il nuovo valore al server
		proxy.post(uri, value.ToString());
		setColor();
	}

	void setColor()
	{
		if (value == true)
			buttonImage.color = new Color(5, 105, 32, 255);
		else
			buttonImage.color = Color.red;
	}

	public void setValue()
	{
		Dictionary<string, string> records = JsonParser.parse(proxy.get(uri))[0];
		try
		{
			Debug.Log("SWITCH GET -> " + records["vb"]);

			if (records.ContainsKey("vb"))
			{
				value= bool.Parse(records["vb"]);
				setColor();
			}
		}
		catch
		{
			value = false;
		}
	}

}

