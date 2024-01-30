using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerCounter : MonoBehaviour
{
    [SerializeField]
    private GameObject timerObj;

    private Text timerText;

    public float timer = 0;

    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    private ObsManager obsManager;


    private void Start()
    {
        timerText = timerObj.GetComponent<Text>();
    }
    // Update is called once per frame
    void Update()
    {
        if (obsManager.isPause == false)
        {
            timerObj.SetActive(true);
            timer += Time.deltaTime;
            timerText.text = "Time\n" + timer.ToString("f1");
        }
    }
}
