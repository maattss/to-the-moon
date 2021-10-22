using UnityEngine;
using UnityEngine.UI;

public class Fuelbar : MonoBehaviour
{
    [SerializeField]
    private float fuel = 100.0f;
    [SerializeField]
    private Slider slider;


    // Update is called once per frame
    void Update()
    {
        fuel -= Time.deltaTime*5f;
        slider.value = fuel;
    }
}
