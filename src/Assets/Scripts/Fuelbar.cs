using UnityEngine;
using UnityEngine.UI;

public class Fuelbar : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    public Movement Movement;

    // Update is called once per frame
    void Update()
    {
        slider.value = Movement.fuelLevel;
    }
}
