using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingManager : MonoBehaviour
{
    public LogicScript logic;
    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    public void EndingDecision()
    {
        if (logic.getTotalEcoScoreAdded() == logic.getTotalGasScoreAdded())
        {
            // Neutral Ending
            SceneManager.LoadSceneAsync("Neutral Ending");
        }
        else if (logic.getTotalEcoScoreAdded() > logic.getTotalGasScoreAdded())
        {
            // Good Ending
            SceneManager.LoadSceneAsync("Good Ending");
        }
        else if (logic.getTotalEcoScoreAdded() < logic.getTotalGasScoreAdded())
        {
            // Bad Ending
            SceneManager.LoadSceneAsync("Bad Ending");
        }
    }
}
