using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;


public class JsonParser : MonoBehaviour
{
	/*
	 * This method takes a JSON response and parses it as a 
	 * List of Dictionaries (where both keys and values are string).
	 * It's a list beacause some respones in SenML may have multiple records (especially for sensors) 
	*/
	public static List<Dictionary<string,string>> parse(string response)
	{
		List<Dictionary<string, string>> parsedResponse;
		parsedResponse = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(response);
		return parsedResponse;
	}
}
