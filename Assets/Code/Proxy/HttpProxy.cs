using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class HttpProxy : MonoBehaviour
{
    public Text ip;
    void Start()
    {
        string uri = "http://" + ip.text + ":7070/api/iot/inventory/device";
        StartCoroutine(getRequest(uri));
    }

    /*ESEMPIO DI RISPOSTA DELL'API:
    * [{"uuid":"device00001","protocol":"coap","ip":"localhost","port":7252,"image":"lol","displayName":"Coffee machine"}]
    */
    IEnumerator getRequest(string uri)
    {
        Debug.Log("Sending request to: " + uri);
        UnityWebRequest uwr = UnityWebRequest.Get(uri);
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
            Dictionary<string,string> dictResp= CoapProxy.parse(uwr.downloadHandler.text)[0];
            Debug.Log("Name: " + dictResp["displayName"]);
        }
    }


}
