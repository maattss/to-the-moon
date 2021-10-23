using UnityEngine;
using UnityEngine.UI;

public class Fuelbar : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private Image sliderBar;
    public Movement Movement;


    // Update is called once per frame
    void Update()
    {
        slider.value = Movement.fuelLevel;
        if(slider.value <= 0)
        {
            sliderBar.color = Color.clear;
        }
    }
}
