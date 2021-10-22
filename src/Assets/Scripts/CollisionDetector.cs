using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    public CollisionType Type = CollisionType.None;
    void OnCollisionEnter(Collision collision)
    {
        if (Type == CollisionType.GameOver)
            Destroy(collision.gameObject);
        else if (Type == CollisionType.Refule)
            collision.gameObject.GetComponent<Movement>().fuelLevel = 100;
        Destroy(this.gameObject);
    }

    public enum CollisionType
    {
        None,
        GameOver,
        Refule,
    }
}
