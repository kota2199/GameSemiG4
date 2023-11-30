using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Title : MonoBehaviour
{
    public string StartScene;
    public string TitleScene;

    //public Button isButton;

    public AudioClip sound1;
    AudioSource audioSource;

    private bool isStart = false;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameStart() {
        if (!isStart) {
            Invoke("Starting", 1.5f);
            //isButton.enabled = false;
            audioSource.PlayOneShot(sound1);
            isStart = true;
        }
    }

    void Starting() {
        SceneManager.LoadScene(StartScene);
    }

    public void GameQuit() {
        Debug.Log("ÉQÅ[ÉÄèIóπ");
    }

    public void Title() {
        if (!isStart) {
            Invoke("TitleBack", 1.5f);
            //isButton.enabled = false;
            //audioSource.PlayOneShot(sound1);
            isStart = true;
        }
    }

    void TitleBack() {
        SceneManager.LoadScene(TitleScene);
    }
}
