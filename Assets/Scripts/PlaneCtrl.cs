using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneCtrl : MonoBehaviour
{
    // 10月29日更新日志：优化地块生成的程序(原cs文件已备份)

    [Header("PlaneInitTranGroup")]
    public GameObject point1;

    [Header("PlaneParameterValue")]
    public GameObject prefabroadway;
    public GameObject prefabroadside;

    // 初始地块数量（=保持有几个地块）
    [Range(5f,10f)]
    public int plot_amount_max = 5;
    // ！！！！暂时不要随意改动下面这组数字，随意更改可能导致连续生成的路面断裂（一个标准地块大小为10x10，标准移动速度为10，生成间隔为1秒）
    float roadmoveSpeed = 10.0f;
    float roadinitfrequency = 1.0f;
    

    void Start(){
        // 初期生成玩家脚下的部分
        PrefabInitGamestart();

        // 每2秒生成一个预制体作为后续的地面（如果地块移动速度改变，生成间隔速度也改变）
        InvokeRepeating("PrefabInit", 0.0f, roadinitfrequency); 
    }

    void Update(){
        // 移动生成的预制体(Tag为roadway)
        PrefabisMoving();
    }

    // 初始化生成玩家脚下的plot
    void PrefabInitGamestart(){
        // 通过一个循环判定，在初始时生成指定数量的plot
        int plot_amount = 0;

        while(plot_amount < plot_amount_max){
            // 计算生成位置
            Vector3 position = new Vector3(point1.transform.position.x, point1.transform.position.y, point1.transform.position.z + 10 * plot_amount);
            // 生成一个锚定物体points
            GameObject points = Instantiate(prefabroadway, position, Quaternion.identity);
            // 根据锚定物体生成初始的plot
            RoadPrefab(points);   
            plot_amount += 1;
        }
    }

    // 持续生成plot
    void PrefabInit(){
        // 计算生成位置
        Vector3 position = new Vector3(point1.transform.position.x, point1.transform.position.y, point1.transform.position.z + 10 * plot_amount_max);
        // 生成一个锚定物体points
        GameObject points = Instantiate(prefabroadway, position, Quaternion.identity);

        RoadPrefab(points);
    }

    // plot生成
    void RoadPrefab(GameObject targetPositionGameObject){
        GameObject generatedObject = Instantiate(prefabroadway, targetPositionGameObject.transform.position, Quaternion.identity);
        GameObject generatedObject2 = Instantiate(prefabroadside, targetPositionGameObject.transform.position, Quaternion.identity);

        // 随机生成颜色
        Renderer renderer = generatedObject.GetComponent<Renderer>();
        if (renderer != null){
            renderer.material.color = new Color(Random.value, Random.value, Random.value);
        }
    }

    // plot移动和销毁
    void PrefabisMoving(){
        // 移动生成的预制体(Tag为Roadway)
        GameObject[] generatedObjects = GameObject.FindGameObjectsWithTag("Roadway");

        foreach (GameObject obj in generatedObjects){
            obj.transform.Translate(Vector3.forward * -roadmoveSpeed * Time.deltaTime);

            // 销毁距离，这个数字不能低于地块数量x11（销毁距离过短会造成plot生成后即被摧毁）
            float destroyDistance = plot_amount_max * 11;

            if (Vector3.Distance(transform.position, obj.transform.position) >= destroyDistance){
                Destroy(obj);
            }
        } 
    }
}
