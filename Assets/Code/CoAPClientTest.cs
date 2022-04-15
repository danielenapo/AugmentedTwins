using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoAPClientTest : MonoBehaviour
{
    Text textData;

    void Start()
    {

        // Find the Text UI object attached to the game object this script is attached to
        textData = gameObject.GetComponent<Text>();

        // Make the call using JNI to the Java Class and write out the response (or write 'Invalid Response From JNI' if there was a problem).
        textData.text = CaptiveReality.Jni.Util.StaticCall("sayHello", "Invalid Response From JNI", "com.example.coapplugin.HelloWorld");
    }


}
