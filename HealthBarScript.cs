using System.Data;
using UnityEngine;
using UnityEngine.UI;

//This containts the Logic script for the health behavior which requires to be attached to the character

public class HealthBarScript : MonoBehaviour
{
    [Header(" ----Please drag the Slider Component and child object -> Fill <- to the slots ----")]
    [Header(" ---- You can Adjust the Gradient field the color of your choosing ----")]
    [Header(" ----Then click on the Border child object and under Source the image of your choosing ----")]
    [Header(" ----Then click on the Heart child object and under Source the image of your choosing ----")]
    public Slider slider; //Variable that contains the slider component to manipulate the Health object bar
    public Gradient gradient; // Creates the component gradient where we can specify the color behavior of our
    // "fill" image bar
    public Image fill; // component where the bar image "fill" is added to manipulate with variable "gradient"
    // for the color position
    public void Start()
    {
        GradientColor(); // Calls the method to create the gradient color from green to red
        if (slider == null)
        {
            slider = GetComponent<Slider>();
            Debug.LogError("Found it !");
    //        slider.maxValue = health.maxHealth;
            slider.value = slider.maxValue; // Sets the current value of the Health object to the max value
            // if (slider == null)
            // {
            //     Debug.LogError("Slider not found. Please assign it in the Inspector or ensure the Slider component exists on this GameObject.");
            // }
        }
        fill = slider.fillRect.GetComponent<Image>();
        // if (fill == null)
        // {
        //     Debug.LogError("Fill Image component not found! Please ensure it exists as a child of the Slider.");
        // }
    }
    public void SetMaxHealth(float health)
    {
        // if (slider != null)
        // {
            slider.maxValue = health; // Sets max value for the Health object
            slider.value = health; // Defaults current starting Health object value
        // }
        // else
        // {
        //     Debug.LogError("Slider is not assigned. Please ensure it is properly set in the Inspector.");
        // }

        fill.color = gradient.Evaluate(1f); // gradient value goes from 0 - 1 , 1 (1f) being 100%, however, the max
        // value for the "fill" image can be fixed 20, 100, etc. to addapt it we use the method below  ↓
    }                                                                                            //    ↓
    public void SetHealth(float health)                                                          //    ↓
    {                                                                                            //    ↓
        slider.GetComponent<Slider>().value = health; // Indicates the variable containing the health value             //    ↓
        if (slider != null && gradient != null)
        {
            fill.color = gradient.Evaluate(slider.normalizedValue); // The function allows to adapt
        }
        else
        {
            Debug.LogError("Slider or Gradient is not assigned. Please ensure they are properly set in the Inspector.");
        }
         // the gradient value (0 - 1) to the values specified by the slider variable for the gradient
         // which can vary to any given value.
    }

    public void GradientColor()
    {
        // Create a gradient from green to red
        GradientColorKey[] colorKeys = new GradientColorKey[2];
        colorKeys[0].color = Color.red; // Start color
        colorKeys[0].time = 0f;           // Start of the gradient
        colorKeys[1].color = Color.green;   // End color
        colorKeys[1].time = 1f;           // End of the gradient

        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2];
        alphaKeys[0].alpha = 1f;          // Fully opaque at the start
        alphaKeys[0].time = 0f;
        alphaKeys[1].alpha = 1f;          // Fully opaque at the end
        alphaKeys[1].time = 1f;

        gradient = new Gradient();
        gradient.SetKeys(colorKeys, alphaKeys);
    }
    
}
