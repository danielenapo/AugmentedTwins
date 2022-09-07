using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Actuator
{

	public void initialize(CoapProxy proxy, string uri, string label);
	public void setValue();
	public void setSensor(GameObject sensor);

}

