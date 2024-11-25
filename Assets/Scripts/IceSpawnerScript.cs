using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpawnerScript : MonoBehaviour
{
    public GameObject[] iceVariants; // Array untuk prefab rintangan
    public float[] heightOffsets; // Array untuk heightOffset masing-masing prefab
    public float spawnRateMin = 1f; // Nilai minimum spawn rate
    public float spawnRateMax = 3f; // Nilai maksimum spawn rate
    private float timer = 0;
    private float spawnRate;

    // Nilai default height offset jika tidak ada di array
    public float defaultHeightOffset = 10f;

    public float minHeight = -5f; // Ketinggian minimal
    public float maxHeight = 5f;  // Ketinggian maksimal

    public float maxHeightChange = 2f; // Selisih maksimal perubahan ketinggian antar rintangan

    private float lastSpawnHeight = 0f; // Posisi ketinggian terakhir rintangan yang dispawn

    private float rintanganTimeScale = 1f; // Time scale untuk rintangan
    public float timeScaleIncreaseAmount = 0.05f; // Penambahan time scale per interval waktu
    public float timeScaleIncreaseInterval = 20f; // Interval waktu dalam detik
    public float maxTimeScale = 1.5f; // Batas maksimal time scale

    private float timeElapsed = 0f; // Waktu yang telah berlalu sejak perubahan terakhir

    void Start()
    {
        spawnRate = Random.Range(spawnRateMin, spawnRateMax);
    }

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
        // Pilih prefab secara acak
        int randomIndex = Random.Range(0, iceVariants.Length);
        GameObject selectedIce = iceVariants[randomIndex];

        // Tentukan heightOffset untuk prefab yang dipilih
        float selectedHeightOffset = (randomIndex < heightOffsets.Length) ? heightOffsets[randomIndex] : defaultHeightOffset;

        // Hitung batas ketinggian untuk spawn (dengan memperhatikan perbedaan maksimal)
        float lowestPoint = Mathf.Max(transform.position.y - selectedHeightOffset, minHeight);
        float highestPoint = Mathf.Min(transform.position.y + selectedHeightOffset, maxHeight);

        // Batasi ketinggian berdasarkan ketinggian terakhir
        float adjustedMin = Mathf.Max(lastSpawnHeight - maxHeightChange, lowestPoint);
        float adjustedMax = Mathf.Min(lastSpawnHeight + maxHeightChange, highestPoint);

        // Pilih ketinggian secara acak dalam batasan yang diperbarui
        float spawnHeight = Random.Range(adjustedMin, adjustedMax);

        // Spawn rintangan
        GameObject newIce = Instantiate(
            selectedIce,
            new Vector3(transform.position.x, spawnHeight, 0),
            transform.rotation
        );

        // Update ketinggian terakhir
        lastSpawnHeight = spawnHeight;

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
