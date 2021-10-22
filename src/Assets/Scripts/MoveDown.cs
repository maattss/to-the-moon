using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : MonoBehaviour
{
    public bool Remove = true;
    Vector3 vec;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        vec = transform.position;
        vec.y -= Time.deltaTime * 10;
        transform.position = vec;

        if (vec.y < -20 && Remove)
        {
            Destroy(this.gameObject);
        }
    }
}
