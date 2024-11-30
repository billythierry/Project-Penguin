using UnityEngine;

public class IceSpawnerScript : MonoBehaviour
{
    public GameObject[] iceVariants;
    public float[] heightOffsets;
    public float spawnRateMin = 1f;
    public float spawnRateMax = 3f;

    public float defaultHeightOffset = 10f;
    public float minHeight = -5f;
    public float maxHeight = 5f;
    public float maxHeightChange = 2f;

    private float timer = 0;
    private float spawnRate;
    private float lastSpawnHeight = 0f;
    private float currentSpeedMultiplier = 1f;

    [Header("Speed and Interval")]
    [SerializeField] private float speedIncreaseAmount = 0.02f; // Kecepatan bertambah per spawn
    [SerializeField] private float speedIncreaseInterval = 0.0002f; // Interval waktu untuk penambahan kecepatan
    [SerializeField] private float maxSpeedMultiplier = 3f; // Kecepatan maksimum

    private float timeSinceLastIncrease = 0f; // Waktu sejak penambahan terakhir

    void Start()
    {
        spawnRate = Random.Range(spawnRateMin, spawnRateMax);
    }

    void Update()
    {
        timer += Time.deltaTime;

        timeSinceLastIncrease += Time.deltaTime; // Menambah waktu sejak penambahan kecepatan terakhir

        if (timer > spawnRate)
        {
            spawnIce();
            timer = 0;
            spawnRate = Random.Range(spawnRateMin, spawnRateMax);

            // Jika sudah mencapai interval untuk menambah kecepatan
            if (timeSinceLastIncrease >= speedIncreaseInterval)
            {
                currentSpeedMultiplier += speedIncreaseAmount; // Menambah multiplier kecepatan
                currentSpeedMultiplier = Mathf.Min(currentSpeedMultiplier, maxSpeedMultiplier); // Batasi kecepatan agar tidak lebih dari maxSpeedMultiplier
                RintanganMoveScript.UpdateAllRintanganSpeed(currentSpeedMultiplier);
                timeSinceLastIncrease = 0f; // Reset waktu
            }
        }
    }

    void spawnIce()
    {
        int randomIndex = Random.Range(0, iceVariants.Length);
        GameObject selectedIce = iceVariants[randomIndex];

        float selectedHeightOffset = (randomIndex < heightOffsets.Length) ? 
            heightOffsets[randomIndex] : defaultHeightOffset;

        float lowestPoint = Mathf.Max(transform.position.y - selectedHeightOffset, minHeight);
        float highestPoint = Mathf.Min(transform.position.y + selectedHeightOffset, maxHeight);

        float adjustedMin = Mathf.Max(lastSpawnHeight - maxHeightChange, lowestPoint);
        float adjustedMax = Mathf.Min(lastSpawnHeight + maxHeightChange, highestPoint);

        float spawnHeight = Random.Range(adjustedMin, adjustedMax);

        GameObject newIce = Instantiate(
            selectedIce,
            new Vector3(transform.position.x, spawnHeight, 0),
            transform.rotation
        );

        // Set speed multiplier untuk rintangan baru
        RintanganMoveScript rintanganScript = newIce.GetComponent<RintanganMoveScript>();
        rintanganScript.SetSpeedMultiplier(currentSpeedMultiplier);

        lastSpawnHeight = spawnHeight;
    }
}
