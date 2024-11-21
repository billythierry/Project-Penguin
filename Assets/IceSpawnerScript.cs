using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpawnerScript : MonoBehaviour
{
    public GameObject ice;
    public float spawnRateMin = 1f; // Nilai minimum spawn rate
    public float spawnRateMax = 3f; // Nilai maksimum spawn rate
    private float timer = 0;
    private float spawnRate;
    public float heightOffset = 10;

    private float rintanganTimeScale = 1f; // Time scale untuk rintangan
    public float timeScaleIncreaseAmount = 0.05f; // Penambahan time scale per interval waktu
    public float timeScaleIncreaseInterval = 20f; // Interval waktu dalam detik (sekarang menjadi 20 detik)
    public float maxTimeScale = 1.5f; // Batas maksimal time scale

    private float timeElapsed = 0f; // Waktu yang telah berlalu sejak perubahan terakhir

    // Start is called before the first frame update
    void Start()
    {
        spawnIce();
        spawnRate = Random.Range(spawnRateMin, spawnRateMax);
    }

    // Update is called once per frame
    void Update()
    {
        // Timer untuk spawn rintangan
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            spawnIce();
            timer = 0;
            spawnRate = Random.Range(spawnRateMin, spawnRateMax) / rintanganTimeScale; // Spawn rate dipengaruhi oleh time scale
        }

        // Tambahkan waktu dan cek apakah sudah mencapai interval untuk menambah kecepatan
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= timeScaleIncreaseInterval)
        {
            IncreaseTimeScale();
            timeElapsed = 0f; // Reset timer
        }
    }

    void spawnIce()
    {
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;

        // Spawn rintangan
        GameObject newIce = Instantiate(
            ice,
            new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0),
            transform.rotation
        );

        // Set time scale lokal untuk rintangan yang baru
        newIce.GetComponent<RintanganMoveScript>().SetTimeScale(rintanganTimeScale);
    }

    void IncreaseTimeScale()
    {
        // Tambahkan kecepatan waktu setiap interval waktu
        rintanganTimeScale += timeScaleIncreaseAmount;
        rintanganTimeScale = Mathf.Min(rintanganTimeScale, maxTimeScale); // Pastikan tidak melebihi maxTimeScale

        Debug.Log($"Time scale rintangan meningkat menjadi {rintanganTimeScale}x setelah {timeScaleIncreaseInterval} detik");

        // Update time scale semua rintangan aktif
        RintanganMoveScript.UpdateTimeScaleForAll(rintanganTimeScale);
    }
}
