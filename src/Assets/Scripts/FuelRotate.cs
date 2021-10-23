using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelRotate : MonoBehaviour
{
    [SerializeField]
    private Transform tf;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        Vector3 rot = new Vector3(0f, 0, Time.deltaTime * 100);
        tf.Rotate(rot);
    }
}
