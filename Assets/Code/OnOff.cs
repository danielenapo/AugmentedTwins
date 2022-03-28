using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems; //to access ar raycast
using UnityEngine.UI;

public class OnOff : MonoBehaviour
{
    private Vector2 touchPosition = default;
    private Camera m_MainCamera;

	void Start()
	{
        m_MainCamera = Camera.main;
	}

	void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchPosition = touch.position;


            //Checking to see if the position of the touch is over a UI object in case of UI overlay on screen.

            //if (!touch.position.IsPointerOverUIObject())
            // {
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = m_MainCamera.ScreenPointToRay(touchPosition);
                RaycastHit hitObject;

                if (Physics.Raycast(ray, out hitObject))
                {
                    //Do whatever you want to do with the hitObject, which in this case would be your, well, case. Identify it either through name or tag, for instance below.
                    if (hitObject.collider.tag=="button") {
                        if (hitObject.collider.GetComponentInChildren<TextMesh>().text == "OFF")
                            hitObject.collider.GetComponentInChildren<TextMesh>().text = "ON";
                        else
                            hitObject.collider.GetComponentInChildren<TextMesh>().text = "OFF";
                    }
                }
				else
				{
                    Debug.Log("nothing touched");
				}
            }
            //}
        }
    }
}
