using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


/// <summary>
/// Adds images to the reference library at runtime.
/// </summary>
    
[RequireComponent(typeof(ARTrackedImageManager))]
public class DynamicLibrary : MonoBehaviour
{
    public Button button;
    public TextMeshProUGUI textLabel;
    private ARTrackedImageManager manager;
    public void Start()
    {
        button.onClick.AddListener(TaskOnClick);
        manager = this.gameObject.GetComponent<ARTrackedImageManager>();
    }
    [Serializable]
    public class ImageData
    {
        [SerializeField, Tooltip("The source texture for the image. Must be marked as readable.")]
        Texture2D m_Texture;

        public Texture2D texture
        {
            get => m_Texture;
            set => m_Texture = value;
        }

        [SerializeField, Tooltip("The name for this image.")]
        string m_Name;

        public string name
        {
            get => m_Name;
            set => m_Name = value;
        }

        [SerializeField, Tooltip("The width, in meters, of the image in the real world.")]
        float m_Width;

        public float width
        {
            get => m_Width;
            set => m_Width = value;
        }

        public string ip { get; set; }
        public int port { get; set; }
        public string protocol { get; set; }

        public AddReferenceImageJobState jobState { get; set; }
    }



	private int imagesCounter = 0; //number of images added

    [SerializeField, Tooltip("The set of images to add to the image library at runtime")]
    ImageData[] m_Images;

    /// <summary>
    /// The set of images to add to the image library at runtime
    /// </summary>
    public ImageData[] images
    {
        get => m_Images;
        set => m_Images = value;
    }

    enum State
    {
        NoImagesAdded, //non è stata aggiunta nessuna immagine
        AddImagesRequested, //è stata inserita un'immagine, e la si vuole aggiungere alla Library
        AddingImages, //aggiungere alla Library
        Done, //aggiunta correttamente
        Error //errore
    } 


    string m_ErrorMessage = "";

    StringBuilder m_StringBuilder = new StringBuilder();

    //updates gui based on the state
    void TaskOnClick()
    {
        PickImage();
    }

    void SetError(string errorMessage)
    {
        textLabel.text = $"Error: {errorMessage}";
    }

    public void addImagesRequested()
    {
        Debug.Log("ADDING IMAGES");
        if (m_Images == null)
        {
            SetError("No images to add.");
            return;
        }


        if (manager == null)
        {
            SetError($"No {nameof(ARTrackedImageManager)} available.");
            return;
        }

        // You can either add raw image bytes or use the extension method (used below) which accepts
        // a texture. To use a texture, however, its import settings must have enabled read/write
        // access to the texture.
        foreach (var image in m_Images)
        {
            if (!image.texture.isReadable)
            {
                SetError($"Image {image.name} must be readable to be added to the image library.");
                return;
            }
        }

        if (manager.referenceLibrary is MutableRuntimeReferenceImageLibrary mutableLibrary)
        {
            try
            {
                foreach (var image in m_Images)
                {
                    // Note: You do not need to do anything with the returned JobHandle, but it can be
                    // useful if you want to know when the image has been added to the library since it may
                    // take several frames.
                    image.jobState = mutableLibrary.ScheduleAddImageWithValidationJob(image.texture, image.name, image.width);
                }

                addImages();
            }
            catch (InvalidOperationException e)
            {
                SetError($"ScheduleAddImageJob threw exception: {e.Message}");
            }
        }
        else
        {
            SetError($"The reference image library is not mutable.");
        }
    }

    public void addImages()
	{
        Debug.Log("Really adding images");
        // Check for completion
        m_StringBuilder.Clear();
        m_StringBuilder.AppendLine("Add image status:");
        var done = true;
        foreach (var image in m_Images)
        {
            m_StringBuilder.AppendLine($"\t{image.name}: {(image.jobState.status.ToString())}");

            if (!image.jobState.jobHandle.IsCompleted)
            {
                done = false;
                break;
            }
        }
        textLabel.text = m_StringBuilder.ToString();

        if (done)
        {
            textLabel.text = ("Image added successfully");
        }

    }

    public void pickImage(List<Dictionary<string, string>> imagesResp)//from HTTP response
	{
        Texture2D texture=new Texture2D(1, 1);
        foreach (Dictionary<string, string> image in imagesResp)
        {
            if (image["image"] != null)
			{
                try
                {
                    byte[] b64_bytes = System.Convert.FromBase64String(image["image"]);
                    texture = new Texture2D(1, 1);
                    texture.LoadImage(b64_bytes);
                }
                catch
                {
                    Debug.Log("Could not convert base64 image to string from " + image["displayName"]);
                }
                finally
                {
                    if (texture != null)
                    {
                        texture = AdaptTexture(texture);
                        m_Images[imagesCounter].texture = texture;
                        m_Images[imagesCounter].name = image["displayName"];
                        m_Images[imagesCounter].ip = image["ip"];
                        m_Images[imagesCounter].port = int.Parse(image["port"]);
                        m_Images[imagesCounter].protocol = image["protocol"];



                        imagesCounter++;
                        addImagesRequested();
                    }
                    else
                    {
                        Debug.Log("Couldn't load texture from " + image["displayName"]);
                    }
                }
            }
			else {
                Debug.Log("No image provided");
                    return;
            }
        }
           
    }
            
        
    private void PickImage()//from gallery
    {
        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
        {
            Debug.Log("Image path: " + path);
            if (path != null)
            {
                // Create Texture from selected image
                Texture2D texture = NativeGallery.LoadImageAtPath(path);
                if (texture != null)
                {
                    texture = AdaptTexture(texture);
                    m_Images[0].texture = texture;
                    //m_Images[0].name = m_Images[imagesCounter].name + imagesCounter;
                 

                    imagesCounter++;
                    addImagesRequested();
                }
                else
                {
                    Debug.Log("Couldn't load texture from " + path);
                    return;
                }

            }
        }, "", "image/*");
    }

    //the funcion makes the newly imported texture readable
    private Texture2D AdaptTexture(Texture2D source)
    {
        RenderTexture renderTex = RenderTexture.GetTemporary(
                    source.width,
                    source.height,
                    0,
                    RenderTextureFormat.Default,
                    RenderTextureReadWrite.Linear);

        Graphics.Blit(source, renderTex);
        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = renderTex;
        Texture2D readableText = new Texture2D(source.width, source.height);
        readableText.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height), 0, 0);
        readableText.Apply();
        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(renderTex);
        return readableText;
    }

    private IEnumerator WaitASec(float seconds)
	{
        yield return new WaitForSeconds(seconds);
    }



}

