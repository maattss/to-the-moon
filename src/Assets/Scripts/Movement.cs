using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    float prevAngle = 0;
    float facingAngle = 0;
    Vector3 Vec;
    public int movementSpeed = 20;
    public float fuelLevel = 100;
    MoonManager moonManager;
    // Start is called before the first frame update
    void Start()
    {
        moonManager = FindObjectOfType<MoonManager>();
    }

    // Update is called once per frame
    void Update()
    {
        fuelLevel -= Time.deltaTime * moonManager.State.FuelConsumption;


        var leftRight = Input.GetAxis("Horizontal");
        Vec = transform.localPosition;
        // Vec.y += Input.GetAxis("Jump") * Time.deltaTime * 20;
        Vec.x += leftRight * Time.deltaTime * moonManager.State.MovementSpeed;
        Vec.x = Mathf.Clamp(Vec.x, -10, 10);
        // Vec.z += Input.GetAxis("Vertical") * Time.deltaTime * 20;
        //var oldAngle = facingAngle;
        facingAngle -= Mathf.Clamp(facingAngle / 2f, -8f, 8f);
        facingAngle -= leftRight * 1000f * Time.deltaTime;
        //var delta = (facingAngle - prevAngle) / Time.deltaTime;
        //facingAngle += delta * 0.4f;
        //prevAngle = oldAngle;

        facingAngle = Mathf.Clamp(facingAngle, -70, 70);
        transform.localPosition = Vec;
        transform.localRotation = Quaternion.Euler(0,180,0) * Quaternion.AngleAxis(facingAngle, new Vector3(0, 1, 0));
        if (fuelLevel < 0)
            Destroy(this.gameObject);
        
    }
}
