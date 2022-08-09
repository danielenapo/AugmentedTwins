using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Sensor
{
	public void initialize(CoapProxy proxy, string uri, string label);
	public void printData();
}
