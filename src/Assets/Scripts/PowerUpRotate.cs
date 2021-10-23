using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpRotate : MonoBehaviour
{
    [SerializeField]
    private Transform tf;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        Vector3 rot = new Vector3(0f, Time.deltaTime * 100, 0f);
        tf.Rotate(rot);
    }
}
