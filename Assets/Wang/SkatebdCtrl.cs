using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkatebdCtrl : MonoBehaviour
{
    public bool game_is_over = false;

    public bool is_attacked = false;
    public bool is_dameged = false;
    public bool is_healed = false;

    [Header("SketeBoard Paramter")]
    [Range(0, 3)]
    public int sketeboard_HPmax3;
    public float sketeboard_HP2 = 0.7f;
    public float sketeboard_HP1 = 0.4f;
    // 血量上限为3时，低于70%则为2，低于40%则为1

    public float sketeboard_HP_max = 100.0f;
    public float sketeboard_HP_min = 0.0f;
    public float sketeboard_HP;
    public float sketeboard_size_max = 1.0f;
    public float sketeboard_size_min = 0.5f;

    [Header("SketeBoard Damage Gameobject")]
    public GameObject sketeboard_sizectrl;
    public GameObject sketeboard_original;
    public GameObject sketeboard_damaged1;
    public GameObject sketeboard_damaged2;
    public GameObject sketeboard_dying;

    public GameObject sketeboard_buff;

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

    public UI_HP ui_hp;

    // Start is called before the first frame update
    void Start(){
        // 初始化HP
        sketeboard_HP_initialization();

        sketeboard_HPmax3 = 3;
        ui_hp.UIUpdate(sketeboard_HPmax3);
    }

    // Update is called once per frame
    void Update(){
        // 失败判定
        If_IsLose();
        
        // Debug.Log(sketeboard_HPmax3);

        // 跟随100上限的HP数值，调整3上限的HP数值
        if(sketeboard_HP > 0){
            if(sketeboard_HP <= sketeboard_HP_max * sketeboard_HP1){
                sketeboard_HPmax3 = 1;
        ui_hp.UIUpdate(sketeboard_HPmax3);
            }else{
                if(sketeboard_HP <= sketeboard_HP_max * sketeboard_HP2){
                    sketeboard_HPmax3 = 2;
                }else{
                    sketeboard_HPmax3 = 3;
                }
            }
        }else{
            sketeboard_HPmax3 = 0;
        }


        ui_hp.UIUpdate(sketeboard_HPmax3);

        // 跟随HP的百分比调整滑板大小
        Sketeboard_Size_Ctrl();

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

    
    // 跟随生命值改变滑板大小状态的

    public void sketeboard_HP_initialization(){
        // 初始化HP
        sketeboard_HP = sketeboard_HP_max;

        // Debug.Log(sketeboard_HP);

        game_is_over = false;
    }

    public void If_IsLose(){
        if(sketeboard_HP <= sketeboard_HP_min && !game_is_over){
            game_is_over = true;
            Debug.Log("Gameover");
        }
    }

    public void SketeBoard_IsDameged(float damege_value){
        sketeboard_HP += damege_value;
        Sketeboard_HP_Limit();
    }

    public void SketeBoard_IsHealed(float heal_value){
        sketeboard_HP += heal_value;
        Sketeboard_HP_Limit();
    }

    public void Sketeboard_HP_Limit(){
        if(sketeboard_HP > sketeboard_HP_max){
            sketeboard_HP = sketeboard_HP_max;
        }

        if(sketeboard_HP < sketeboard_HP_min){
            sketeboard_HP = sketeboard_HP_min;
        }
    }

    public void Sketeboard_Size_Ctrl(){
        // 跟随HP的百分比调整滑板大小
        float sizescale = (sketeboard_size_max - sketeboard_size_min) * sketeboard_HP / sketeboard_HP_max + sketeboard_size_min;
        Vector3 newScale = new Vector3(sizescale, 1, sizescale);
        transform.localScale = newScale;
    }



}
