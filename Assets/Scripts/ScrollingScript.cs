using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingScript : MonoBehaviour
{
    public float baseScrollSpeed = 0.5f; // Kecepatan awal scroll background
    private float currentScrollSpeed;   // Kecepatan scroll saat ini
    private float offset;               // Offset untuk scrolling
    private Material mat;               // Material background

    [Header("Speed Adjustment")]
    [SerializeField] private float speedIncreaseAmount = 0.02f; // Peningkatan kecepatan per interval
    [SerializeField] private float speedIncreaseInterval = 5f;  // Interval waktu untuk peningkatan
    [SerializeField] private float maxScrollSpeed = 2f;         // Kecepatan maksimum

    private float timeSinceLastIncrease = 0f; // Waktu sejak penambahan terakhir

    void Start()
    {
        mat = GetComponent<Renderer>().material;
        currentScrollSpeed = baseScrollSpeed; // Inisialisasi kecepatan scroll
        Debug.Log($"Kecepatan awal scroll: {currentScrollSpeed}");
    }

    void Update()
    {
        // Update offset untuk scrolling
        offset += (Time.deltaTime * currentScrollSpeed) / 10f;
        mat.SetTextureOffset("_MainTex", new Vector2(offset, 0));

        // Hitung waktu sejak penambahan kecepatan terakhir
        timeSinceLastIncrease += Time.deltaTime;

        // Jika mencapai interval, tambahkan kecepatan scroll
        if (timeSinceLastIncrease >= speedIncreaseInterval)
        {
            currentScrollSpeed += speedIncreaseAmount;
            currentScrollSpeed = Mathf.Min(currentScrollSpeed, maxScrollSpeed); // Jangan melebihi kecepatan maksimum
            timeSinceLastIncrease = 0f; // Reset waktu

            //Debug.Log($"Kecepatan Background bertambah : {currentScrollSpeed}");
        }
    }

    // Fungsi untuk mengatur kecepatan scroll langsung (opsional, bisa dipanggil dari luar)
    public void SetScrollSpeed(float newSpeed)
    {
        currentScrollSpeed = Mathf.Clamp(newSpeed, 0f, maxScrollSpeed);

        //Debug.Log($"Kecepatan background saat ini : {currentScrollSpeed}");
    }
}
