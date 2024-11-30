using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public int playerScore;
    public int ecoScore;
    //public int gasScore;
    public Text scoreText;
    public Text ecoText;
    public GameObject gameOverScreen; 

    [ContextMenu("Tambah Score")]
    public void addScore(int scoreToAdd)
    {
        playerScore += scoreToAdd;
        scoreText.text = playerScore.ToString();
    }

    public void addEcoScore(int ecoScoreToAdd)
    {
        ecoScore += ecoScoreToAdd;
        ecoText.text = ecoScore.ToString();
    }

    public void restartGame()
    {
        Time.timeScale = 1; // Mengembalikan waktu ke normal
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMenu()
    {
        SceneManager.LoadSceneAsync("Main Menu");
        Time.timeScale = 1;
    }

    public void gameOver()
    {
        gameOverScreen.SetActive(true);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
