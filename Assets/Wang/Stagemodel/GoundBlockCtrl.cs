using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoundBlockCtrl : MonoBehaviour
{
    // 这个脚本挂在PlaneInitCtrl上，需要在下方绑定前几个地块生成的位置以及地块的预制件

    [Header("InitTranGroup")]
    public GameObject GroundBlockInitObject1;
    public GameObject GroundBlockInitObject2;
    public GameObject GroundBlockInitObject3;
    public GameObject GroundBlockInitObject4;
    public GameObject GroundBlockInitObject5;
    public GameObject GroundBlockInitObject6;
    public GameObject GroundBlockInitObject_last;

    [Header("GroundBlockPrefab List")]
    public GameObject[] GroundBlockPrefab_Inside;
    public GameObject[] GroundBlockPrefab_Outside;
    // 地块移动速度
    float InsideBlockmoveSpeed = 10.0f;
    float OutsideBlockmoveSpeed = 8.0f;
    // 生成间隔时间
    float roadinitfrequency = 5.5f;
    // 地块销毁距离
    float destroyPositionZ = -100;

    // 生成思路，按照顺序生成内侧与外侧的场地环境模型，内侧的速度为10f，外侧的为8f？
    // 设定生成列表
    // 写一个函数，每次调用时自动从list中生成下一个模型

    // public bool isInit = false;
    int Inside_Loop = 0;
    int Outside_Loop = 0;

    // Start is called before the first frame update
    void Start()
    {
        InsidePrefabInit(GroundBlockInitObject1);
        InsidePrefabInit(GroundBlockInitObject2);
        OutsidePrefabInit(GroundBlockInitObject2);
        InsidePrefabInit(GroundBlockInitObject3);
        InsidePrefabInit(GroundBlockInitObject4);
        OutsidePrefabInit(GroundBlockInitObject4);
        InsidePrefabInit(GroundBlockInitObject5);
        InsidePrefabInit(GroundBlockInitObject6);

        // 每2秒生成一个预制体作为后续的地面（如果地块移动速度改变，生成间隔速度也改变）
        InvokeRepeating("PrefabInit", 0.0f, roadinitfrequency);
    }

    // Update is called once per frame
    void Update()
    {
        InsidePrefabisMoving();
        OutsidePrefabisMoving();

        // 测试用
        // if(isInit){
        //     InsidePrefabInit(GroundBlockInitObject_last);
        //     OutsidePrefabInit(GroundBlockInitObject_last);
        //     isInit = false;
        // }
    }

    // 持续生成
    void PrefabInit(){
        InsidePrefabInit(GroundBlockInitObject_last);
        OutsidePrefabInit(GroundBlockInitObject_last);
    }

    // 指定位置按顺序生成InsidePrefab
    void InsidePrefabInit(GameObject targetPositionGameObject){
        GameObject generatedObject = Instantiate(GroundBlockPrefab_Inside[Inside_Loop], targetPositionGameObject.transform.position, Quaternion.identity);
        Inside_Loop += 1;

        if(Inside_Loop >= GroundBlockPrefab_Inside.Length){
            Inside_Loop = 0;
        }
    }

    // 指定位置按顺序生成InsidePrefab
    void OutsidePrefabInit(GameObject targetPositionGameObject){
        GameObject generatedObject = Instantiate(GroundBlockPrefab_Outside[Outside_Loop], targetPositionGameObject.transform.position, Quaternion.identity);
        Outside_Loop += 1;

        if(Outside_Loop >= GroundBlockPrefab_Outside.Length){
            Outside_Loop = 0;
        }
    }

    // Inside移动和销毁
    void InsidePrefabisMoving(){
        // 移动生成的预制体(Tag为Roadway)
        GameObject[] generatedObjects_Inside = GameObject.FindGameObjectsWithTag("Roadway");

        foreach (GameObject obj in generatedObjects_Inside){
            obj.transform.Translate(Vector3.forward * -InsideBlockmoveSpeed * Time.deltaTime);

            // 当移动超过指定位置则销毁
            if(obj.transform.position.z <= destroyPositionZ){
                Destroy(obj);
            }
        } 
    }

    // Outside移动和销毁
    void OutsidePrefabisMoving(){
        // 移动生成的预制体
        GameObject[] generatedObjects_Outside = GameObject.FindGameObjectsWithTag("Wayside");

        foreach (GameObject obj in generatedObjects_Outside){
            obj.transform.Translate(Vector3.forward * -OutsideBlockmoveSpeed * Time.deltaTime);

            // 当移动超过指定位置则销毁
            if(obj.transform.position.z <= destroyPositionZ){
                Destroy(obj);
            }
        } 
    }
}
