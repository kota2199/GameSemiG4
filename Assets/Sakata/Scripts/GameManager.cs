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

    [Header("Key???l????")]
    public int KeyCount;
    [Header("Key?????W?l????")]
    public int KeyMax;

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
        Invoke("GimmickStart", 2f);

        if (!CutorialMode) {
            Destroy(Cutorial);
            Destroy(CutorialUI);
            Player.GetComponent<PlayerMovement>().isMove = true;
            ObsManager.SetActive(true);
            ObsManager.GetComponent<ObsManager>().isPause = false;
            audioSource.enabled = false;
        } else {
            audioSource.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(KeyCount >= KeyMax) {
            BossStage = 1;
            KeyCount = 0;
            if (bossTrigger)
            {
                Debug.Log("BOSS");
                StartBossBattle();
            }
        }

        if(hpScript.hp <= 0) {
            StartCoroutine("GameOver");
        }

        if(BossStage == 2) {
            thisStage += 1;
            SceneManager.LoadScene("Stage" + thisStage);
        }

        switch (thisStage) {
            case 1:
                if(audioSource == null) {
                    break;
                }
                audioSource.Play();
                audioSource.clip = Stage1BGM;
                break;
            case 2:
                if (audioSource == null) {
                    break;
                }
                audioSource.Play();
                audioSource.clip = Stage1BGM;
                break;
            default:
                break;
        }
    }

    IEnumerator GameOver() {
        Debug.Log("�v���C���[���|���A�j���[�V�����H");

        yield return new WaitForSeconds(2);

        SceneManager.LoadScene(GameOverScene);
    }

    void GimmickStart() {
        Debug.Log("��Q�������J�n");
    }

    public void CutorialFinish()
    {
        Destroy(Cutorial);
        Destroy(CutorialUI);
        Player.GetComponent<PlayerMovement>().isMove = true;
        ObsManager.SetActive(true);
        ObsManager.GetComponent<ObsManager>().isPause = false;
        audioSource.enabled = true;

    }
    void StartBossBattle()
    {
        bossTrigger = false;
        Vector3 playerPos = new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z);
        boss.transform.position = new Vector3(playerPos.x, playerPos.y + 15, playerPos.z + 5);
        boss.GetComponent<SnowManController>().startAttacking = true;
    }
}
