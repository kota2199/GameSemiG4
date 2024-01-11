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
    private int CursorNum = 0;

    public AudioClip StartSound;
    public AudioClip CursorSound;
    public AudioClip SerectSound;
    AudioSource audioSource;

    private bool isStart = false;

    public Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        Cursor();
        if (Input.GetKeyDown(KeyCode.Return)) {
            switch (CursorNum) {
                case 0:
                    GameStart();
                    break;
                case 1:
                    Debug.Log("Setting Click");
                    audioSource.PlayOneShot(SerectSound);
                    break;
                case -1:
                    Debug.Log("Credit Click");
                    audioSource.PlayOneShot(SerectSound);
                    break;
                default:
                    break;
            }
        }
    }

    void Cursor(){
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
            if (CursorNum == 0) {
                anim.Play("SelectSettingAnim");
                audioSource.PlayOneShot(CursorSound);
                CursorNum = 1;
            }
            if (CursorNum == 1) {
                return;
            }
            if (CursorNum == -1) {
                anim.Play("SelectStartAnim");
                audioSource.PlayOneShot(CursorSound);
                CursorNum = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
            if (CursorNum == 0) {
                anim.Play("SelectCreditAnim");
                audioSource.PlayOneShot(CursorSound);
                CursorNum = -1;
            }
            if (CursorNum == 1) {
                anim.Play("SelectStartAnim");
                audioSource.PlayOneShot(CursorSound);
                CursorNum = 0;
            }
            if (CursorNum == -1) {
                return;
            }
        }
    }

    void GameStart() {
        if (!isStart) {
            Invoke("Starting", 1.5f);
            anim.Play("StartAnim");
            audioSource.PlayOneShot(StartSound);
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
            audioSource.PlayOneShot(SerectSound);
            isStart = true;
        }
    }

    void TitleBack() {
        SceneManager.LoadScene(TitleScene);
    }
}
