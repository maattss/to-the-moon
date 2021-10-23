using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<Transform> objects;
    public float minSpawnTime, maxSpawnTime, nextSpawn;

    private float lastSpawn;
    public int spawnRate;
    MoonManager moonManager;
    // Start is called before the first frame update
    void Start()
    {
        moonManager = FindObjectOfType<MoonManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (moonManager.State.PreventSpawn) return;
        if (Time.realtimeSinceStartup >= nextSpawn)
        {
            nextSpawn = Time.realtimeSinceStartup + Random.Range(minSpawnTime, maxSpawnTime);
            Spawn(objects[Random.Range(0, objects.Count)]);
        }
    }

    private Transform Spawn(Transform transform)
    {
        var newTransform = Instantiate(transform, new Vector3(Random.value * 20 - 10, 20, 0), transform.rotation);
        newTransform.gameObject.SetActive(true);
        return newTransform;
    }
}
