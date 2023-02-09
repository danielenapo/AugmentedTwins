using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation.Samples;

public class HttpProxy : MonoBehaviour
{
    public Text ip;
    public DynamicLibrary imgLibrary;
    public Button button;
    
    void Start()
    {
        button.onClick.AddListener(TaskOnClick);
        
    }

    public void TaskOnClick()
	{
        string uri = "http://" + ip.text + ":7070/api/iot/inventory/device";
        StartCoroutine(getRequest(uri));
    }

    /*ESEMPIO DI RISPOSTA DELL'API:
    * [{"uuid":"device00001","protocol":"coap","ip":"localhost","port":7252,"image":"base64/...","displayName":"Coffee machine"}]
    - uuid: codice identificativo del dispositivo
    - protocol: protocollo di comunicazione (si ipotizza essere CoAP per questo progetto)
    - ip: indirizzo locale del dispositivo
    - port: porta del protocollo (standard CoAP è 7252)
    - image: immagine identificativa, codificata in base64
    - displayName: testo human-readable che verrà mostrato nel label del canvas
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
            List<Dictionary<string,string>> dictResp= CoapProxy.parse(uwr.downloadHandler.text);
            imgLibrary.pickImage(dictResp);
        }
    }




}
