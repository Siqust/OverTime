using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScale : MonoBehaviour
{

    [Range(0.05f, 3)]
    public float timeScale = 1f;
    void Update()
    {
        Time.timeScale = timeScale;
    }
}
