using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Follows camera direction
/// </summary>
public class Billboard : MonoBehaviour
{

    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(WaitASec(0.2f));
    }

    //for smooth rotation
    private IEnumerator WaitASec(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        transform.LookAt(cam.transform);
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y + 180f, 0f);
    }
}
