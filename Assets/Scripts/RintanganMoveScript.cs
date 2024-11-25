using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RintanganMoveScript : MonoBehaviour
{
    public float moveSpeed; // Kecepatan awal rintangan
    public float deadZone = -12;
    private float rintanganTimeScale = 1f; // Time scale lokal untuk rintangan

    // List global untuk menyimpan semua instance aktif
    private static List<RintanganMoveScript> activeRintangan = new List<RintanganMoveScript>();

    private void OnEnable()
    {
        // Tambahkan instance ini ke daftar aktif
        activeRintangan.Add(this);
    }

    private void OnDisable()
    {
        // Hapus instance ini dari daftar aktif
        activeRintangan.Remove(this);
    }

    void Update()
    {
        if (transform.position.x < deadZone)
        {
            Debug.Log("Rintangan Dihapus");
            Destroy(gameObject);
        }

        // Gerakan rintangan dengan waktu lokal
        transform.position += Vector3.left * moveSpeed * rintanganTimeScale * Time.deltaTime;
    }

    // Fungsi untuk mengatur time scale lokal rintangan
    public void SetTimeScale(float timeScale)
    {
        rintanganTimeScale = timeScale;
    }

    // Fungsi statis untuk memperbarui time scale semua rintangan aktif
    public static void UpdateTimeScaleForAll(float newTimeScale)
    {
        foreach (var rintangan in activeRintangan)
        {
            rintangan.SetTimeScale(newTimeScale);
        }
    }
}
