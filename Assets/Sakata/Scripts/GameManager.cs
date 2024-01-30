using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int thisStage = 1;

    public static bool CutorialMode = true;
    public GameObject Cutorial;
    public GameObject CutorialUI;

    [HideInInspector]
    public int BossStage = 0; //0=???????? 1=?????? 2=?????I??

    [Space(10)]

    public string ClearScene;
    public string GameOverScene;

    // ここでFindWithTag("Player")を使えず、publicで導入する--WANG
    public GameObject Player;
    public GameObject ObsManager;

    [SerializeField]
    private GameObject boss;

    PlayerHPManager hpScript;

    Fade fadeScript;

    AudioSource audioSource;

    public bool BGMStart;
    public bool BGMStop;
    public bool Play;

    public AudioClip Stage1BGM;
    bool bossTrigger = true;

    void Start()
    {
        // ここでFindWithTag("Player")を使えず、publicで導入する--WANG
        // GameObject Player = GameObject.FindWithTag("Player");
        hpScript = Player.GetComponent<PlayerHPManager>();

        GameObject fade = GameObject.Find("Fade");
        fadeScript = fade.GetComponent<Fade>();

        audioSource = GetComponent<AudioSource>();

        BossStage = 0;
        fadeScript.FadeIn = true;

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
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Play)
        {

            if (hpScript.hp <= 0)
            {
                StartCoroutine("GameOver");
            }

            if (BGMStart)
            {
                audioSource.volume -= 0.5f * Time.deltaTime;
                if (audioSource.volume <= 0)
                {
                    BGM();
                }
            }
            if (BGMStop)
            {
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
            if (audioSource.volume < 0.3f && !BGMStart)
            {
                audioSource.volume += 0.001f;
                if (audioSource.volume > 0.3f)
                {
                    audioSource.volume = 0.3f;
                }
            }
        }
    }

    IEnumerator GameOver() {

        yield return new WaitForSeconds(2);

        SceneManager.LoadScene(GameOverScene);
    }

    void GimmickStart() {
    }

    public void CutorialFinish()
    {
        Destroy(Cutorial);
        Destroy(CutorialUI);
        Player.GetComponent<PlayerMovement>().isMove = true;
        //timer
        ObsManager.SetActive(true);
        ObsManager.GetComponent<ObsManager>().isPause = false;
        audioSource.enabled = true;
    }
    void StartBossBattle()
    {
        bossTrigger = false;
        Vector3 playerPos = new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z);
        boss.transform.position = new Vector3(playerPos.x, playerPos.y + 15, playerPos.z + 5);
        //boss.GetComponent<SnowManController>().startAttacking = true;
    }

        void BGM()
        {
            audioSource.volume = 0;
            if (BossStage == 0)
            {
                switch (thisStage)
                {
                    case 1:
                        if (audioSource == null)
                        {
                            break;
                        }
                        Debug.Log("stage1");
                        audioSource.clip = Stage1BGM;
                        audioSource.Play();
                        BGMStart = false;
                        break;
                    case 2:
                        if (audioSource == null)
                        {
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
            else if (BossStage > 0)
            {
                switch (BossStage)
                {
                    case 1:
                        if (audioSource == null)
                        {
                            break;
                        }
                        Debug.Log("boss1");
                        audioSource.clip = Stage1BGM;
                        audioSource.Play();
                        BGMStart = false;
                        break;
                    case 2:
                        if (audioSource == null)
                        {
                            break;
                        }
                        Debug.Log("boss2");
                        audioSource.clip = Stage1BGM;
                        audioSource.Play();
                        BGMStart = false;
                        break;
                    case 3:
                        if (audioSource == null)
                        {
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
