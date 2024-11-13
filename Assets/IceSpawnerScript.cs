using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpawnerScript : MonoBehaviour
{
    public GameObject ice;
    public float spawnRateMin = 1f; // Nilai minimum waktu spawn
    public float spawnRateMax = 3f; // Nilai maksimum waktu spawn
    private float timer = 0;
    private float spawnRate; // Spawn rate acak untuk setiap siklus
    public float heightOffset = 10;

    // Start is called before the first frame update
    void Start()
    {
        spawnIce();
        spawnRate = Random.Range(spawnRateMin, spawnRateMax); // Atur spawn rate awal secara acak
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            spawnIce();
            timer = 0;
            spawnRate = Random.Range(spawnRateMin, spawnRateMax); // Update spawn rate setiap kali spawn
        }
    }

    void spawnIce()
    {
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;

        Instantiate(
            ice, 
            new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0), 
            transform.rotation
        );
    }
}
