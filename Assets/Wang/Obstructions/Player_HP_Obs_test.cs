using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_HP_Obs_test : MonoBehaviour
{
    // 用于测试碰撞对玩家的生命值改变

    public bool game_is_over = false;

    [Header("SketeBoard Paramter")]
    public float sketeboard_HP_max = 100.0f;
    public float sketeboard_HP_min = 0.0f;
    public float sketeboard_HP;

    public bool is_dameged = false;
    public bool is_healed = false;

    [Header("Attack List")]
    public float sketeboard_attack = 100.0f;

    [Header("Damage List")]
    // example => damegeobject s name
    public float example01_damege = -10.0f;
    public float example02_damege = -20.0f;

    [Header("Heal List")]
    // example => damegeobject s name
    public float example01_heal = 10.0f;
    public float example02_heal = 25.0f;

    // Start is called before the first frame update
    void Start()
    {
        // 初始化HP
        sketeboard_HP_initialization();
    }

    // Update is called once per frame
    void Update()
    {
        // 受伤判定
        if(is_dameged){
            SketeBoard_IsDameged(example01_damege);
            is_dameged = false;
        }

        // 治疗判定
        if(is_healed){
            SketeBoard_IsHealed(example02_heal);
            is_healed = false;
        }
    }

    public void sketeboard_HP_initialization(){
        // 初始化HP
        sketeboard_HP = sketeboard_HP_max;

        // Debug.Log(sketeboard_HP);

        game_is_over = false;
    }

    public void SketeBoard_IsDameged(float damege_value){
        sketeboard_HP += damege_value;
        Sketeboard_HP_Limit();
        Debug.Log(sketeboard_HP);
    }

    public void SketeBoard_IsHealed(float heal_value){
        sketeboard_HP += heal_value;
        Sketeboard_HP_Limit();
        Debug.Log(sketeboard_HP);
    }

    public void Sketeboard_HP_Limit(){
        if(sketeboard_HP > sketeboard_HP_max){
            sketeboard_HP = sketeboard_HP_max;
        }

        if(sketeboard_HP < sketeboard_HP_min){
            sketeboard_HP = sketeboard_HP_min;
        }
    }
}
