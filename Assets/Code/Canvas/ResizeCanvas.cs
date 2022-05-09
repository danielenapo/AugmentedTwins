using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class ResizeCanvas : MonoBehaviour
{
    public GameObject canvas;
    public GameObject ipInputText;

    [SerializeField]
    ARTrackedImageManager m_TrackedImageManager;
    
    void OnEnable() => m_TrackedImageManager.trackedImagesChanged += OnChanged;

    void OnDisable() => m_TrackedImageManager.trackedImagesChanged -= OnChanged;

    void OnChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var newImage in eventArgs.added)
        {
            string ip = ipInputText.GetComponent<Text>().text;
            var sizeX =newImage.referenceImage.width;
            Debug.Log("(DEB) Image found: " + newImage.referenceImage.name + " size " + newImage.referenceImage.width);
            newImage.GetComponentInChildren<CanvasBuilder>().initialize(sizeX, ip);
        }

        foreach (var updatedImage in eventArgs.updated)
        {
            // Handle updated event
        }

        foreach (var removedImage in eventArgs.removed)
        {
            // Handle removed event
        }
    }
}
