using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class PenguinScript : MonoBehaviour
{
    public Rigidbody2D myRigitBody;
    public GameObject PauseScreen;
    // public GameObject ContinueToEnding;
    public float flapStrength;
    public LogicScript logic;
    public AudioManagerScript audioManager;
    public PauseMenu pause;
    public EndingManager ending;
    public bool IsAlive = true;
    private bool isPaused = false;
    private bool hasFlapped = false;
    public float autoMoveSpeed = 2f; // Kecepatan pergerakan otomatis ke kanan
    private bool isMovingAutomatically = false; // Cek jika pemain sedang bergerak otomatis
    public float targetX = 10f; // Posisi X target untuk mengakhiri pergerakan
    public Image fadeImage; // Referensi untuk fade image

    // Variabel untuk menentukan kapan pergerakan otomatis dimulai
    private bool canMoveAutomatically = false;

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManagerScript>();
        ending = GameObject.FindGameObjectWithTag("Ending").GetComponent<EndingManager>();
        Time.timeScale = 0;
        logic.GuideText.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsAlive && !isMovingAutomatically)
        {
            if (!hasFlapped)
            {
                logic.GuideText.SetActive(false); 
                logic.InGameUIScreen.SetActive(true); 
                hasFlapped = true; 
            }

            Time.timeScale = 1;
            myRigitBody.velocity = Vector2.up * flapStrength;
            audioManager.playSFX(audioManager.jetpack);
        }

        // Mengecek jika game sudah selesai dan pemain harus mulai bergerak otomatis
        if (logic.getTotalPlayerScoreAdded() >= 50 && !isMovingAutomatically && !canMoveAutomatically)
        {
            StartCoroutine(StartAutoMove());
            canMoveAutomatically = true; // Mengatur agar pergerakan otomatis hanya dimulai sekali
        }

        // Periksa apakah pemain sudah mencapai posisi X yang ditargetkan untuk berpindah scene
        if (isMovingAutomatically)
        {
            // Gerakkan karakter ke kanan
            transform.position += Vector3.right * autoMoveSpeed * Time.deltaTime;
            
            
            

            // Jika posisi pemain sudah cukup jauh ke kanan, pindah ke scene ending
            if (transform.position.x >= targetX)
            {
                logic.ContinueToEnding.SetActive(true);
            }
        }

        // Cegah pemain menggerakkan karakter jika sedang bergerak otomatis
        if (isMovingAutomatically)
        {
            logic.ResultHistory.SetActive(true);
            return; // Tidak memproses input jika karakter sedang bergerak otomatis
        }

        // Cek batas posisi dan kondisi game over
        if (transform.position.y < -6.3 || transform.position.y > 6.3 && IsAlive)
        {
            if (IsAlive)
            {
                GameOver();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Time.timeScale = 1; // Lanjutkan game
                isPaused = false;
                pause.Continue();
            }
            else
            {
                Time.timeScale = 0; // Pause game
                isPaused = true;
                pause.Pause();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameOver();
        audioManager.playSFX(audioManager.wallTouch);
    }

    private void GameOver()
    {
        logic.gameOver();
        IsAlive = false;
        Time.timeScale = 0;
        audioManager.Stop();
        logic.ResultHistory.SetActive(true);
        StartCoroutine(PlayDeathSoundWithDelay(0.5f));
    }

    

    private IEnumerator PlayDeathSoundWithDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay); // Tunggu selama "delay" detik
        audioManager.playSFX(audioManager.die); // Putar sound effect "die"
    }

    private IEnumerator ShowResultDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        logic.ResultHistory.SetActive(true);
    }

    // Coroutine untuk mulai pergerakan otomatis
    private IEnumerator StartAutoMove()
    {
        yield return new WaitForSeconds(2f); // Tunggu selama 2 detik setelah game berhenti
        isMovingAutomatically = true; // Mulai pergerakan otomatis

        // Nonaktifkan gravitasi dan set kecepatan vertikal menjadi nol
        myRigitBody.gravityScale = 0f; // Nonaktifkan gravitasi
        myRigitBody.velocity = Vector2.zero; // Pastikan karakter tidak jatuh
    }
    
}
