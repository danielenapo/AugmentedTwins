using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Actuator
{
	public void setUri(string uri);
	public void setLabel(string label);
	public void initialize(CoapProxy proxy);

}

