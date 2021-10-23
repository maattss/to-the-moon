using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    public CollisionType Type = CollisionType.None;
    public bool Remove = true;
    Vector3 vec;
    MoonManager moonManager;
    void Start()
    {
        moonManager = FindObjectOfType<MoonManager>();
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Movement>(out var movement))
        {
            if (Type == CollisionType.GameOver)
            {
                moonManager.GameOver();
            }
            else if (Type == CollisionType.Refule)
            {
                movement.fuelLevel = 100;
            }
            else if (Type == CollisionType.Slowmo)
            {
                moonManager.AddModifier(s =>
                {
                    s.FallSpeed /= 2;
                }, 5);
            }
            else if (Type == CollisionType.DoubleSpeed)
            {
                moonManager.AddModifier(s =>
                {
                    s.MovementSpeed *= 2;
                }, 5);
            }
        }

        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.realtimeSinceStartup < 4) return;
        vec = transform.position;
        vec.y -= Time.deltaTime * moonManager.State.FallSpeed;
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
        DoubleSpeed,
        Slowmo,
    }
}
