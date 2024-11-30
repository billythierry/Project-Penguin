using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RintanganMoveScript : MonoBehaviour
{
    public float baseMoveSpeed = 5f;
    public float deadZone = -12f;
    public float maxMoveSpeed = 10f;

    private float currentSpeedMultiplier = 1f;
    private static List<RintanganMoveScript> activeRintangan = new List<RintanganMoveScript>();

    private void OnEnable()
    {
        activeRintangan.Add(this);
    }

    private void OnDisable()
    {
        activeRintangan.Remove(this);
    }

    void Update()
    {
        // Gerakan rintangan dengan kecepatan yang disesuaikan
        transform.position += Vector3.left * (baseMoveSpeed * currentSpeedMultiplier) * Time.deltaTime;

        if (transform.position.x < deadZone)
        {
            Destroy(gameObject);
        }
    }

    public void SetSpeedMultiplier(float multiplier)
    {
        currentSpeedMultiplier = multiplier;
    }

    public static void UpdateAllRintanganSpeed(float newSpeedMultiplier)
    {
        foreach (var rintangan in activeRintangan)
        {
            rintangan.SetSpeedMultiplier(newSpeedMultiplier);
        }
        Debug.Log($"Semua Rintangan Diupdate ke Speed Multiplier: {newSpeedMultiplier}x");
    }
}