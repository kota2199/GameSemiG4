using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damegetest : MonoBehaviour
{

    public bool is_dameged = false;

    // Bossのダメージのデータ
    public float Boss_Attack_Damege = 20;

    // Stage1/PlayerCtrl/PlayerBody/SkateBoard_sizeのPrefabを置いてください
    public GameObject skatebdctrl;

    // Update is called once per frame
    void Update()
    {
        if(is_dameged){
            // skatebdctrl.GetComponent<SkatebdCtrl>().sketeboard_HPこれはプレイヤーのHP
            
            // プレイヤーのHPにダメージを
            
            // skatebdctrl.GetComponent<SkatebdCtrl>().sketeboard_HP -= Boss_Attack_Damege;

            skatebdctrl.GetComponent<SkatebdCtrl>().SketeBoard_IsDameged(-50);

            // すぐダメージされたHPのデータ
            // Debug.Log(skatebdctrl.GetComponent<SkatebdCtrl>().sketeboard_HP);

            is_dameged = false;
        }
    }

    // Bossは何かとコレクションしたとき
    void OnTriggerEnter(Collider other){

        // もしPlayerのTagを持っているものとコレクションした
        if (other.CompareTag("Player")){
            Debug.Log("is_dameged");

            // 方法一、この（）にダメージを入れ、負数を使ってください
            // (Assets\Wang\SkatebdCtrl.cs\115)
            other.GetComponent<SkatebdCtrl>().SketeBoard_IsDameged(-50);

            // 方法二、障害物と同じダメージのメソッドを使って（ダメージが１０点）
            // (Assets\Wang\Obstructions\ObsPrefab\Obs_Ctrl.cs\105)
            other.GetComponent<SkatebdCtrl>().is_dameged = true;

            // 方法三、Playerとコレクションすればメソッドを使わず、プレイヤーのHPにダメージを与える
            skatebdctrl.GetComponent<SkatebdCtrl>().sketeboard_HP -= Boss_Attack_Damege;

            Debug.Log(skatebdctrl.GetComponent<SkatebdCtrl>().sketeboard_HP);
        }
    }

}
