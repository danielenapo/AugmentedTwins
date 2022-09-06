using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using static UnityEngine.XR.ARFoundation.Samples.DynamicLibrary;

public class ResizeCanvas : MonoBehaviour
{
    public GameObject canvas;
    public GameObject ipInputText;
    private ImageData[] images;

    [SerializeField]
    ARTrackedImageManager m_TrackedImageManager;

	public void initialize(ImageData[] images)
	{
        this.images = images;
	}

	void OnEnable() => m_TrackedImageManager.trackedImagesChanged += OnChanged;

    void OnDisable() => m_TrackedImageManager.trackedImagesChanged -= OnChanged;

    void OnChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var newImage in eventArgs.added)
        {
            string ip = ipInputText.GetComponent<Text>().text;
            var sizeX =newImage.referenceImage.width;
            Debug.Log("(DEB) Image found: " + newImage.referenceImage.name + " size " + newImage.referenceImage.width);
            foreach(ImageData image in images)
			{
                if(image.name== newImage.referenceImage.name)
				{
                    newImage.GetComponentInChildren<CanvasBuilder>().initialize(sizeX, image.ip, image.port, image.protocol, image.name);
                }
			}
           
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
