using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHPManager : MonoBehaviour
{
    [Header("HP")]
    [Range(0, 3)]
    public int hp = 3;

    // プレイヤーのリアルタイムのHPパラメーターとここのhpをシンクロした
    public GameObject SkateBoard_size;

    // Update is called once per frame
    void Update()
    {
        hp = SkateBoard_size.GetComponent<SkatebdCtrl>().sketeboard_HPmax3;
        // Debug.Log(hp);
    }
}
