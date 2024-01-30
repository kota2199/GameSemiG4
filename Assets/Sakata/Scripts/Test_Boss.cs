using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Boss : MonoBehaviour
{
    [Header("Boss??????HP")]
    public int MaxHP = 50;

    [Header("??????BossHP")]
    [Range(0, 50)]
    public int HP = 50;

    [Header("??????BossHP")]
    public string BossName;

    public bool BossTime = false;

    public GameObject TestBossObj;

    GameManager GMScript;

    // Start is called before the first frame update
    void Start()
    {
        //GameObject GameManager = GameObject.Find("GameManager");
        //GMScript = GameManager.GetComponent<GameManager>();

        //TestBossObj.SetActive(false);
        //HP = MaxHP;
    }

    // Update is called once per frame

    /*
    void Update()
    {
        //if (GMScript.BossStage == 1) {
        //    TestBossObj.SetActive(true);
        //    BossTime = true;
        //}
        //if(HP == 0 && BossTime) {
        //    GMScript.BossStage = 2;
        //    BossTime = false;
        //}
    }
    */
}
