using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static int thisStage = 1;
    public static float PlayTime;
    public int BossStage = 0;
    [HideInInspector]
    public bool BossFinish = false;
    public bool Play;
    public Text Timer;

    public static bool CutorialMode = true;
    public GameObject Cutorial;
    public GameObject CutorialUI;

    [Space(5)]

    [Header("TimeCount")]
    public float BossTimeCount;
    public float BossTimeCountMax = 30f;
    public Text BossCountTimer;

    [Space(10)]

    public string ClearScene;
    public string GameOverScene;

    // ここでFindWithTag("Player")を使えず、publicで導入する--WANG
    public GameObject Player;
    public GameObject ObsManager;

    PlayerHPManager hpScript;

    Fade fadeScript;

    AudioSource audioSource;
    public bool BGMStart;
    public bool BGMStop;
    /*
    private bool BGMPitchUp;
    private float BGMPitch;
    */
    public AudioClip Stage1BGM;

    void Start()
    {
        // ここでFindWithTag("Player")を使えず、publicで導入する--WANG
        // GameObject Player = GameObject.FindWithTag("Player");
        hpScript = Player.GetComponent<PlayerHPManager>();

        GameObject fade = GameObject.Find("Fade");
        fadeScript = fade.GetComponent<Fade>();

        audioSource = GetComponent<AudioSource>();

        BossStage = 0;
        PlayTime = 0.0f;
        fadeScript.FadeIn = true;
        BossTimeCount = BossTimeCountMax;

        if (!CutorialMode) {
            Destroy(Cutorial);
            Destroy(CutorialUI);
            Player.GetComponent<PlayerMovement>().isMove = true;
            ObsManager.SetActive(true);
            ObsManager.GetComponent<ObsManager>().isPause = false;
            audioSource.enabled = false;
            BGMStart = true;
            Play = true;
        } else {
            audioSource.enabled = true;
            Play = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Play) {
            if (BossStage == 0) {
                BossTimeCount -= Time.deltaTime;
                if (BossTimeCount <= 0f) {
                    BossStage = 1;
                    BossTimeCount = BossTimeCountMax;
                    BGMStart = true;
                }
            }

            if (hpScript.hp <= 0) {
                StartCoroutine("GameOver");
            }

            if (BossStage > 0) {
                ObsManager.GetComponent<ObsManager>().isPause = true;
                if (BossFinish) {
                    StartCoroutine("Interval");
                    BossFinish = false;
                }
            }

            if (BGMStart) {
                audioSource.volume -= 0.5f * Time.deltaTime;
                if (audioSource.volume <= 0) {
                    BGM();
                }
            }
            if (BGMStop) {
                audioSource.Stop();
                BGMStop = false;
            }
            /*
            if (BGMPitchUp) {
                audioSource.pitch += 0.001f;
                if(audioSource.pitch >= BGMPitch) {
                    audioSource.pitch = BGMPitch;
                    BGMPitchUp = false;
                }
            }
            */
            if (audioSource.volume < 0.3f && !BGMStart) {
                audioSource.volume += 0.001f;
                if (audioSource.volume > 0.3f) {
                    audioSource.volume = 0.3f;
                }
            }

            PlayTime += Time.deltaTime;
            Timer.text = "PlayTime : " + PlayTime.ToString("f2");
            BossCountTimer.text = BossTimeCount.ToString("f0");
            if (BossTimeCount > 6) {
                Timer.color = new Color(0, 0, 0, 1);
                BossCountTimer.color = Color.clear;
            } else {
                BossCountTimer.color = Color.red;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            BossFinish = true;
        }
    }

    IEnumerator GameOver() {
        Debug.Log("�v���C���[���|���A�j���[�V�����H");

        yield return new WaitForSeconds(5);

        SceneManager.LoadScene(GameOverScene);
    }

    IEnumerator Interval() {
        BGMStop = true;
        float Pitch = audioSource.pitch;

        yield return new WaitForSeconds(2.5f);

        BossStage = 0;
        thisStage += 1;
        //BGMPitch = Mathf.Min(Pitch += 0.05f, 2);
        ObsManager.GetComponent<ObsManager>().isPause = false;
        BGMStart = true;

        yield return new WaitForSeconds(2f);

        //BGMPitchUp = true;
    }

    public void CutorialFinish() {
        Destroy(Cutorial);
        Destroy(CutorialUI);
        Player.GetComponent<PlayerMovement>().isMove = true;
        ObsManager.SetActive(true);
        ObsManager.GetComponent<ObsManager>().isPause = false;
        audioSource.enabled = true;
        BGMStart = true;
        Play = true;
    }

    void BGM() {
        audioSource.volume = 0;
        if (BossStage == 0) {
            switch (thisStage) {
                case 1:
                    if (audioSource == null) {
                        break;
                    }
                    Debug.Log("stage1");
                    audioSource.clip = Stage1BGM;
                    audioSource.Play();
                    BGMStart = false;
                    break;
                case 2:
                    if (audioSource == null) {
                        break;
                    }
                    Debug.Log("stage2");
                    audioSource.clip = Stage1BGM;
                    audioSource.Play();
                    BGMStart = false;
                    break;
                default:
                    break;
            }
        }
        else if (BossStage > 0) {
            switch (BossStage) {
                case 1:
                    if (audioSource == null) {
                        break;
                    }
                    Debug.Log("boss1");
                    audioSource.clip = Stage1BGM;
                    audioSource.Play();
                    BGMStart = false;
                    break;
                case 2:
                    if (audioSource == null) {
                        break;
                    }
                    Debug.Log("boss2");
                    audioSource.clip = Stage1BGM;
                    audioSource.Play();
                    BGMStart = false;
                    break;
                case 3:
                    if (audioSource == null) {
                        break;
                    }
                    audioSource.clip = Stage1BGM;
                    audioSource.Play();
                    BGMStart = false;
                    break;
                default:
                    break;
            }
        }
    }
}
