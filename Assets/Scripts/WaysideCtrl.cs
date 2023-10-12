using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaysideCtrl : MonoBehaviour
{   
    [Header("InitTranGroup")]
    public GameObject point1;
    public GameObject point2;

    [Header("PrefabParameterValue")]
    public GameObject prefabtree;
    public float prefabmoveSpeed = 6.0f;
    public float roadinitfrequency = 2.0f;
    public float destroyDistance = 60.0f;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("PrefabInit", 0.0f, roadinitfrequency);
    }

    // Update is called once per frame
    void Update()
    {
        PrefabisMoving();
    }

    void PrefabInit(){
        InitPrefab(point1);
        InitPrefab(point2);
    }

    void InitPrefab(GameObject targetPositionGameObject){
        GameObject generatedObject = Instantiate(prefabtree, targetPositionGameObject.transform.position, Quaternion.identity);

        // 随机生成颜色
        Renderer renderer = generatedObject.GetComponent<Renderer>();
        if (renderer != null){
            renderer.material.color = new Color(Random.value, Random.value, Random.value);
        }

        // 设置销毁时间
        Destroy(generatedObject, destroyDistance / prefabmoveSpeed);
    }

    void PrefabisMoving(){
        // 移动生成的预制体(Tag为Wayside)
        GameObject[] generatedObjects = GameObject.FindGameObjectsWithTag("Wayside");

        foreach (GameObject obj in generatedObjects){
            obj.transform.Translate(Vector3.forward * -prefabmoveSpeed * Time.deltaTime);

            // 检查距离并销毁
            if (Vector3.Distance(transform.position, obj.transform.position) >= destroyDistance){
                Destroy(obj);
            }
        } 
    }
}
