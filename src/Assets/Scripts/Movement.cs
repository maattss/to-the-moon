using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    float prevAngle = 0;
    float facingAngle = 0;
    Vector3 Vec;
    public int movementSpeed = 20;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var leftRight = Input.GetAxis("Horizontal");
        Vec = transform.localPosition;
        // Vec.y += Input.GetAxis("Jump") * Time.deltaTime * 20;
        Vec.x += leftRight * Time.deltaTime * 20;
        Vec.x = Mathf.Clamp(Vec.x, -10, 10);
        // Vec.z += Input.GetAxis("Vertical") * Time.deltaTime * 20;
        //var oldAngle = facingAngle;
        facingAngle -= Mathf.Clamp(facingAngle / 4f, -4f, 4f);
        facingAngle -= leftRight * 40f * Time.deltaTime;
        //var delta = (facingAngle - prevAngle) / Time.deltaTime;
        //facingAngle += delta * 0.4f;
        //prevAngle = oldAngle;

        facingAngle = Mathf.Clamp(facingAngle, -70, 70);
        transform.localPosition = Vec;
        Debug.Log($"{facingAngle}");
        transform.localRotation = Quaternion.Euler(0,180,0) * Quaternion.AngleAxis(facingAngle + Mathf.PI, new Vector3(0, 1, 0));
        
    }
}
