using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int thisStage = 1;

    [HideInInspector]
    public int BossStage = 0; //0=???????? 1=?????? 2=?????I??

    [Header("Key???l????")]
    public int KeyCount;
    [Header("Key?????W?l????")]
    public int KeyMax;

    [Space(10)]

    public string ClearScene;
    public string GameOverScene;


    PlayerHPManager hpScript;

    Fade fadeScript;

    void Start()
    {
        GameObject Player = GameObject.FindWithTag("Player");
        hpScript = Player.GetComponent<PlayerHPManager>();

        GameObject fade = GameObject.Find("Fade");
        fadeScript = fade.GetComponent<Fade>();

        BossStage = 0;
        fadeScript.FadeIn = true;
        Invoke("GimmickStart", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        if(KeyCount >= KeyMax) {
            BossStage = 1;
            KeyCount = 0;
        }

        if(hpScript.hp <= 0) {
            StartCoroutine("GameOver");
        }

        if(BossStage == 2) {
            thisStage += 1;
            SceneManager.LoadScene("Stage" + thisStage);
        }
    }

    IEnumerator GameOver() {
        Debug.Log("プレイヤーが倒れるアニメーション？");

        yield return new WaitForSeconds(5);

        SceneManager.LoadScene(GameOverScene);
    }

    void GimmickStart() {
        Debug.Log("障害物生成開始");
    }
}
