using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<Transform> fuelcans;
    public List<Transform> astroids;

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
        if (Time.realtimeSinceStartup - lastSpawn > spawnRate)
        {
            lastSpawn = Time.realtimeSinceStartup;
            Spawn(fuelcans[Random.Range(0, fuelcans.Count)]);
            Spawn(astroids[Random.Range(0, astroids.Count)]);
        }
    }

    private void Spawn(Transform transform)
    {
        var newTransform = Instantiate(transform, new Vector3(Random.value * 20 - 10, 20, 0), transform.rotation);
        newTransform.gameObject.SetActive(true);
    }
}
