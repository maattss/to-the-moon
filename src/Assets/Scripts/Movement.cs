using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    Vector3 Vec;
    public int movementSpeed = 20;
    public float fuelLevel = 100;
    public Text Text;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        fuelLevel -= Time.deltaTime * 10;
        Text.text = "Fuel: " + fuelLevel.ToString("#.00");
        Vec = transform.localPosition;
        // Vec.y += Input.GetAxis("Jump") * Time.deltaTime * 20;
        Vec.x += Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed;
        Vec.x = Mathf.Clamp(Vec.x, -10, 10);
        // Vec.z += Input.GetAxis("Vertical") * Time.deltaTime * 20;
        transform.localPosition = Vec;
        if (fuelLevel < 0)
            Destroy(this.gameObject);
        
    }
}
