using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform fuelcan;
    public Transform astroid;

    private float lastSpawn;
    public int spawnRate;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.realtimeSinceStartup - lastSpawn > spawnRate)
        {
            lastSpawn = Time.realtimeSinceStartup;
            var newTransform = Instantiate(fuelcan, new Vector3(Random.value * 10, 10, 0), Quaternion.identity);
            newTransform.gameObject.SetActive(true);
        }
    }
}
