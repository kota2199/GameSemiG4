using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int thisStage = 1;

    [HideInInspector]
    public int BossStage = 0; //0=”ñí“¬’† 1=í“¬’† 2=í“¬I—¹

    [Header("Key‚ÌŠl“¾”")]
    public int KeyCount;
    [Header("Key‚Ì–Ú•WŠl“¾”")]
    public int KeyMax;

    [Space(10)]

    public string ClearScene;
    public string GameOverScene;


    Test_Player PlayerScript;

    void Start()
    {
        GameObject Player = GameObject.Find("Demo_Player");
        PlayerScript = Player.GetComponent<Test_Player>();

        BossStage = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(KeyCount >= KeyMax) {
            BossStage = 1;
            KeyCount = 0;
        }

        if(PlayerScript.HP <= 0) {
            SceneManager.LoadScene(GameOverScene);
        }

        if(BossStage == 2) {
            thisStage += 1;
            SceneManager.LoadScene("Stage" + thisStage);
        }
    }
}
