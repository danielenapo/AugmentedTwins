using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface SensorInterface 
{

	public void initialize(CoapProxy proxy);
	public void setUri(string uri);
	public void setLabel(string label);
	public void printData();
}
