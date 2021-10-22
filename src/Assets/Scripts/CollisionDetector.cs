using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    public CollisionType Type = CollisionType.None;
    public bool Remove = true;
    Vector3 vec;
    void OnCollisionEnter(Collision collision)
    {
        if (Type == CollisionType.GameOver)
            Destroy(collision.gameObject);
        else if (Type == CollisionType.Refule)
            collision.gameObject.GetComponent<Movement>().fuelLevel = 100;
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.realtimeSinceStartup < 4) return;
        vec = transform.position;
        vec.y -= Time.deltaTime * 10;
        transform.position = vec;

        if (vec.y < -20 && Remove)
        {
            Destroy(this.gameObject);
        }
    }

    public enum CollisionType
    {
        None,
        GameOver,
        Refule,
    }
}
