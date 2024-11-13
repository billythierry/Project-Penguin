using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinScript : MonoBehaviour
{
    public Rigidbody2D myRigitBody;
    public GameObject PauseScreen;
    public float flapStrength;
    public LogicScript logic;
    public PauseMenu pause;
    public bool IsAlive = true;
    private bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsAlive)
        {
            myRigitBody.velocity = Vector2.up * flapStrength;
        }

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
    }

    private void GameOver()
    {
        logic.gameOver();
        IsAlive = false;
        Time.timeScale = 0; // Menghentikan waktu ketika game over
    }

    private void PauseMenu()
    {
        PauseScreen.SetActive(true);
    }
}
