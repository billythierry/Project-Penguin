using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public int playerScore;
    private int totalPlayerScoreAdded;

    public int ecoScore;
    private int totalEcoScoreAdded;

    public int gasScore;
    private int totalGasScoreAdded;

    public Text scoreText;
    public Text ecoText;
    public Text gasText;
    public Text totalScoreText;
    public Text totalEcoText;
    public Text totalGasText;

    public GameObject gameOverScreen; 
    public GameObject goodEndingScreen;
    public GameObject badEndingScreen;
    public GameObject InGameUIScreen;
    public GameObject ResultHistory;
    public GameObject ContinueToEnding;
    public GameObject GuideText;

    [ContextMenu("Tambah Score")]
    public void addScore(int scoreToAdd)
    {
        playerScore += scoreToAdd;
        totalPlayerScoreAdded += scoreToAdd;
        scoreText.text = playerScore.ToString();

        // Perbarui total score text
        updateTotalScoreText();
    }

    public void addEcoScore(int ecoScoreToAdd)
    {
        ecoScore += ecoScoreToAdd;
        totalEcoScoreAdded += ecoScoreToAdd;
        ecoText.text = ecoScore.ToString();
        Debug.Log($"Eco Score : {ecoScore}");

        // Perbarui total eco score text
        updateTotalEcoText();
    }

    public void addGasScore(int gasScoreToAdd)
    {
        gasScore += gasScoreToAdd;
        totalGasScoreAdded += gasScoreToAdd;
        gasText.text = gasScore.ToString();
        Debug.Log($"Gas Score : {gasScore}");

        // Perbarui total gas score text
        updateTotalGasText();
    }

    public int getTotalPlayerScoreAdded() => totalPlayerScoreAdded;
    public int getTotalEcoScoreAdded() => totalEcoScoreAdded;
    public int getTotalGasScoreAdded() => totalGasScoreAdded;

    // public void endingDecision()
    // {
    //     if (ecoScore == gasScore)
    //     {
    //         // Neutral Ending
    //         gameOverScreen.SetActive(true);
    //         ResultHistory.SetActive(true);
    //         Debug.Log($"Total Player Score Added: {getTotalPlayerScoreAdded()}");
    //     }
    //     else if (ecoScore > gasScore)
    //     {
    //         // Good Ending
    //         goodEndingScreen.SetActive(true);
    //         ResultHistory.SetActive(true);
    //         Debug.Log($"Total Eco Score Added: {getTotalEcoScoreAdded()}");
    //     }
    //     else if (ecoScore < gasScore)
    //     {
    //         // Bad Ending
    //         badEndingScreen.SetActive(true);
    //         ResultHistory.SetActive(true);
    //         Debug.Log($"Total Gas Score Added: {getTotalGasScoreAdded()}");
    //     }
    // }

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
        InGameUIScreen.SetActive(false);
        gameOverScreen.SetActive(true);
    }

    public void quitGame()
    {
        Application.Quit();
    }

    // Fungsi untuk memperbarui total score text
    private void updateTotalScoreText()
    {
        totalScoreText.text = totalPlayerScoreAdded.ToString();
    }

    private void updateTotalEcoText()
    {
        totalEcoText.text = totalEcoScoreAdded.ToString();
    }

    private void updateTotalGasText()
    {
        totalGasText.text = totalGasScoreAdded.ToString();
    }
}

