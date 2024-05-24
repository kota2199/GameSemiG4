using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsManager : MonoBehaviour
{
    // 这个脚本请挂在Manager上，控制全部障碍物的生成顺序和生成位置。
    // healpool只会生成在两侧，而钥匙只生成在中间。
    // 障碍物种类越多，随机障碍物出现null的情况越小

    // 用于暂停障碍物生成系统，不生成新的障碍物
    public bool isPause = false;

    [Header("Obstacle Generation Order")]
    [Range(1,3)]
    public float Obs_Generation_Interval = 2f;

    // 用于伪循环生成关键道具
    // 在1~10以内随机一个数字，如果该结果为1，则生成钥匙，否则则生成“随机障碍物”，且下次在1~9以内随机。以此类推，每次随机的上限减一。直到结果为1，下一次的上限回到10
    int upperLimit = 10;

    // 同时生成第二个随机障碍物的概率%
    [Range(0,100)]
    public float Double_Obs_Percent = 50;

    [Range(1,3)]
    // 第一个障碍物生成于
    public float Which_Way_Generation = 2;
    float Which_Way_Generation_next;


    // 用于安置随机生成的障碍物
    [Header("Leftway_Randam_Obs Prefab List")]
    public GameObject[] Leftway_Random_Obs;
    [Header("Middleway_Randam_Obs Prefab List")]
    public GameObject[] Middleway_Random_Obs;
    [Header("Rightway_Randam_Obs Prefab List")]
    public GameObject[] Rightway_Random_Obs;

    // 用于放置特定的道具物品
    [Header("Leftway_Key_Obs Prefab")]
    public GameObject Leftway_Heal;
    [Header("Middleway_Key_Obs Prefab")]
    public GameObject Middleway_Key1;
    public GameObject Middleway_Key2;
    public GameObject Middleway_Key3;
    [Header("Rightway_Key_Obs Prefab")]
    public GameObject Rightway_Heal;

    bool key1_is_restart;
    bool key2_is_restart;
    // bool key3_is_restart;

    void Start(){
        // 每隔n秒调用决定下一个生成的物体类型的函数
        InvokeRepeating("Choose_Obs_Type", 0f, Obs_Generation_Interval);
        
        // 初始化key的生成记录
        Initialization_key_is_restart();
    }

    void Initialization_key_is_restart(){
        key1_is_restart = false;
        key2_is_restart = false;
        // key3_is_restart = false;
    }

    void Update(){
        // 暂停的原理是，将下一个物体的生成道路锁定为第0条路
        if(isPause){
            Which_Way_Generation = 0;
            Which_Way_Generation_next = 0;
        }else{
            Which_Way_Generation = 2;
            Which_Way_Generation_next = 2;
        }
    }

    // 每隔n秒执行一次生成程序
    // 首先决定下一个生成的物体，为“随机障碍物”or“指定物体”？

    // 如果要生成“随机障碍物”，则进入障碍物的生成逻辑。即，根据上一个障碍物的生成道路，然后使用对应的生成逻辑判定下一个障碍物将生成在哪条道路。
    // 决定生成道路后，在该道路全部的随机障碍物池中激活一个障碍物

    // 如果要生成指定物体，则直接激活指定物体。

    void Choose_Obs_Type(){
        // 决定下一个生成的物体为“随机障碍物”or“指定物体”
        // Debug.Log("Choose_Obs_Type");

        // 在1~10以内随机一个数字。
        // 若结果不为1，则生成“随机障碍物”，且下次随机取数的上限减1。
        // 若结果为1，则生成“关键道具”，且下次随机取数的上限为10。

        // 随机生成一个数字
        int randomNumber = Random.Range(1, upperLimit + 1);
        // Debug.Log(randomNumber+","+upperLimit);

        if(randomNumber == 1){
            // 执行生成关键道具的函数
            is_restartkey();

            upperLimit = 10;
        }else{
            // 执行生成“随机障碍物”的函数
            Obs_Random_Generation();

            upperLimit --;
            // 理论上来说upperLimit变为1时,一定会roll到1，upperLimit就会重置为10
        }
    }

    // 如果生成了key1则生成key2，以此类推
    void is_restartkey(){
        if (!isPause) {
            if (!key1_is_restart) {
                is_restart(Middleway_Key1);
                key1_is_restart = true;
            } else {
                if (!key2_is_restart) {
                    is_restart(Middleway_Key2);
                    is_restart(Rightway_Heal);
                    key2_is_restart = true;
                } else {
                    is_restart(Middleway_Key3);
                    is_restart(Leftway_Heal);
                    // key3_is_restart = true;
                    Initialization_key_is_restart();
                }
            }
        }
        
    }

    void Obs_Random_Generation(){
        // 即，根据上一个障碍物的生成道路，然后使用对应的生成逻辑判定下一个障碍物将生成在哪条道路。
        // 决定生成道路后，在该道路全部的随机障碍物池中激活一个障碍物

        Which_Way_Generation_next = Which_Way_Generation;

        switch(Which_Way_Generation_next){
            case 1:
                // 从list里随机挑选一个障碍物
                Try_Leftway_Random_Obs();
                
                // 如果在左边生成，则下一个必在中路生成
                Which_Way_Generation = 2;
                break;
            case 2:
                Try_Middleway_Random_Obs();

                // 如果在中间生成，则下一次必在左或右
                Which_Way_Generation = Random.Range(0, 2) == 0 ? 1 : 3;
                // Debug.Log(Which_Way_Generation);

                break;
            case 3:
                Try_Rightway_Random_Obs();

                // 如果在右边生成，则下一个必在中路生成
                Which_Way_Generation = 2;
                break;
        }
    }

    void is_restart(GameObject Obs){
        if(Obs){
            // 触发指定物体上的脚本中的restart(只会触发一次运动)
            Obs.GetComponent<Obs_Ctrl>().is2start = true;
        }
    }

    void Try_Leftway_Random_Obs(){
        // 从list里随机挑选一个障碍物
        is_restart(Leftway_Random_Obs[Random.Range(0, Leftway_Random_Obs.Length)]);

        // 有概率同时生成一个其他路的障碍物。
        float doublechance = Random.Range(0, 11);
        if (doublechance < Double_Obs_Percent / 10){
            float doublechance_whichway = Random.Range(1, 4);
            // 随机生成一个其他路的
            if(doublechance_whichway == 2){
                is_restart(Middleway_Random_Obs[Random.Range(0, Middleway_Random_Obs.Length)]);
                Debug.Log("doublechance_whichway == 2");
            }
            if(doublechance_whichway == 3){
                is_restart(Rightway_Random_Obs[Random.Range(0, Rightway_Random_Obs.Length)]);
                Debug.Log("doublechance_whichway == 3");
            }
        }
    }

    void Try_Middleway_Random_Obs(){
        is_restart(Middleway_Random_Obs[Random.Range(0, Middleway_Random_Obs.Length)]);

        // 有概率同时生成一个其他路的障碍物。
        float doublechance = Random.Range(0, 11);
        if (doublechance < Double_Obs_Percent / 10){
            float doublechance_whichway = Random.Range(0, 2) == 0 ? 1 : 3;;
            // 随机生成一个其他路的
            if(doublechance_whichway == 1){
                is_restart(Leftway_Random_Obs[Random.Range(0, Leftway_Random_Obs.Length)]);
                // Debug.Log("doublechance_whichway == 1");
            }
            if(doublechance_whichway == 3){
                is_restart(Rightway_Random_Obs[Random.Range(0, Rightway_Random_Obs.Length)]);
                // Debug.Log("doublechance_whichway == 3");
            }
        }
    }

    void Try_Rightway_Random_Obs(){
        is_restart(Rightway_Random_Obs[Random.Range(0, Rightway_Random_Obs.Length)]);

        // 有概率同时生成一个其他路的障碍物。
        float doublechance = Random.Range(0, 11);
        if (doublechance < Double_Obs_Percent / 10){
            float doublechance_whichway = Random.Range(0, 3);
            // 随机生成一个其他路的
            if(doublechance_whichway == 1){
                is_restart(Leftway_Random_Obs[Random.Range(0, Leftway_Random_Obs.Length)]);
            }
            if(doublechance_whichway == 2){
                is_restart(Middleway_Random_Obs[Random.Range(0, Middleway_Random_Obs.Length)]);
            }
        }
    }
}
