using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsManager : MonoBehaviour
{
    // 这个脚本请挂在Manager上，控制全部障碍物的生成顺序和生成位置。
    
    [Header("Leftway_Obs Prefab List")]
    public GameObject[] Leftway_Obs;

    [Header("Middleway_Obs Prefab List")]
    public GameObject[] Middleway_Obs;

    [Header("Rightway_Obs Prefab List")]
    public GameObject[] Rightway_Obs;

    [Header("Leftway_Obs Restart Button")]
    public bool Leftway_is_restart0;
    public bool Leftway_is_restart1;
    public bool Leftway_is_restart2;

    [Header("Middleway_Obs Restart Button")]
    public bool Middleway_is_restart0;

    [Header("Rightway_Obs Restart Button")]
    public bool Rightway_is_restart0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    

    // Update is called once per frame
    void Update()
    {
        // 输入指定的bool和物体，bool为true时即可触发其上restart回到起点
        is_restart(Leftway_is_restart0, Leftway_Obs[0]);
        is_restart(Leftway_is_restart1, Leftway_Obs[1]);
        is_restart(Leftway_is_restart2, Leftway_Obs[2]);

    }

    void is_restart(bool is_restart, GameObject Obs){
        // 持续触发指定物体的restart
        if(is_restart){
            // 触发指定物体的restart
            Obs.GetComponent<Obs_Ctrl>().is2start = true;

            // 避免重复积累触发
            is_restart = false;

        }else{
            // 避免重复积累触发
            Obs.GetComponent<Obs_Ctrl>().is2start = false;
        }
    }

}
