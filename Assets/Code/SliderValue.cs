using UnityEngine;
using UnityEngine.UI;

public class SliderValue : MonoBehaviour
{
    string sliderTextString = "0";
    public Text sliderText; // public is needed to ensure it's available to be assigned in the inspector.

    public void textUpdate(float textUpdateNumber)
    {
        sliderTextString = textUpdateNumber.ToString();
        sliderText.text = "VALUE: "+sliderTextString;
    }
}
