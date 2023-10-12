using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneCtrl : MonoBehaviour
{

    [Header("PlaneInitTranGroup")]
    public GameObject point1;
    public GameObject point2;
    public GameObject point3;
    public GameObject point4;
    public GameObject point5;

    [Header("PlaneParameterValue")]
    public GameObject prefabroadway;
    public GameObject prefabroadside;
    public float roadmoveSpeed = 10.0f;
    // 新地块生成时间这里需要设定一个函数保证不同速度下道路的接合
    public float roadinitfrequency = 1.0f;
    public float destroyDistance = 60.0f;



    // Start is called before the first frame update
    void Start()
    {
        // 初期生成玩家脚下的部分
        PrefabInitGamestart();

        // 每2秒生成一个预制体作为后续的地面
        InvokeRepeating("PrefabInit", 0.0f, roadinitfrequency); 
    }

    // Update is called once per frame
    void Update(){
        // 移动生成的预制体(Tag为road)
        PrefabisMoving();
    }

    void PrefabInitGamestart(){
        RoadPrefab(point1);
        RoadPrefab(point2);
        RoadPrefab(point3);
        RoadPrefab(point4);
    }

    void PrefabInit(){
        RoadPrefab(point5);
    }



    void RoadPrefab(GameObject targetPositionGameObject){
        GameObject generatedObject = Instantiate(prefabroadway, targetPositionGameObject.transform.position, Quaternion.identity);
        GameObject generatedObject2 = Instantiate(prefabroadside, targetPositionGameObject.transform.position, Quaternion.identity);

        // 随机生成颜色
        Renderer renderer = generatedObject.GetComponent<Renderer>();
        if (renderer != null){
            renderer.material.color = new Color(Random.value, Random.value, Random.value);
        }


        // 设置销毁时间
        Destroy(generatedObject, destroyDistance / roadmoveSpeed);
        Destroy(generatedObject2, destroyDistance / roadmoveSpeed);
    }

    void PrefabisMoving(){
        // 移动生成的预制体(Tag为Roadway)
        GameObject[] generatedObjects = GameObject.FindGameObjectsWithTag("Roadway");

        foreach (GameObject obj in generatedObjects){
            obj.transform.Translate(Vector3.forward * -roadmoveSpeed * Time.deltaTime);

            // 检查距离并销毁
            if (Vector3.Distance(transform.position, obj.transform.position) >= destroyDistance){
                Destroy(obj);
            }
        } 
    }


}
