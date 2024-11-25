using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRateScript : MonoBehaviour
{
    // Start is called before the first frame update
    public enum limits
    {
        noLimit = 0,
        limit30 = 30,
        limit60 = 60
    }

    public limits limit;

    void Awake()
    {
        Application.targetFrameRate = (int)limit;
    }
}
