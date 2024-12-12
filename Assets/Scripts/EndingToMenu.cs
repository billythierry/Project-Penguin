using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingToMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void BackToMenuFromEnding()
    {
        SceneManager.LoadSceneAsync("Main Menu");
    }
}
