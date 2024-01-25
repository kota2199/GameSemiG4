using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runningspeed : MonoBehaviour
{

    [Range(0.5f,3f)]
    public float runningspeed = 1.0f;

    public bool isPause = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPause) {
            runningspeed = 0f;
        } else {
            Time.timeScale = runningspeed;
        }
        
    }
}
