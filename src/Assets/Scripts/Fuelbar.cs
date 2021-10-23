using UnityEngine;
using UnityEngine.UI;

public class Fuelbar : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    public Image sliderImage;

    public Movement Movement;


    // Update is called once per frame
    void Update()
    {
        slider.value = Movement.fuelLevel;
        if (slider.value >= 60)
        {
            sliderImage.color = Color.green;
        }
        if (slider.value >= 20 && slider.value < 60)
        {
            sliderImage.color = Color.yellow;
        }
        if (slider.value < 20)
        {
            sliderImage.color = Color.red;
        }
    }
}
