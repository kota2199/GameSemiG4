using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("PlayerInitTranGroup")]
    public GameObject leftpoint;
    public GameObject middlepoint;
    public GameObject rightpoint;
    public GameObject groundpoint;
    public GameObject jumppoint;

    [Header("PlayerBodyPrefab")]
    public GameObject playerbodyPrefab;

    [Header("PlayerParameterValue")]
    // 玩家横向移动速度
    public float playerhorizontalmovespeed = 7.0f;
    // 玩家横向移动时微转身角度
    public float playerhorizontalrotationspeed = 5.0f;
    // 玩家跳跃速度
    public float playerjumpspeed = 8.0f;
    // 玩家坠落速度
    public float playerfallspeed = 15.0f;

    // 玩家跳跃中段减速高度
    [Range(0,1)]
    public float playerjumpmiddlespeedpoint = 0.5f;
    // 玩家跳跃中段减速幅度
    [Range(0,1)]
    public float playerjumpmiddlespeed = 0.7f;

    // 玩家跳跃末段减速高度
    [Range(0,1)]
    public float playerjumpattenuationspeedpoint = 0.8f;
    // 玩家跳跃末段减速幅度
    [Range(0,1)]
    public float playerjumpattenuationspeed = 0.99f;
    
    // 玩家坠落前期减速速度
    [Range(0,1)]
    public float playerfallattenuationspeedpoint = 0.7f;
    // 玩家坠落前期减速幅度
    [Range(0,1)]
    public float playerfallattenuationspeed = 0.5f;

    [Header("PlayerMoveCtrl")]
    public bool toleft = false;
    public bool toright = false;
    public bool tojump = false;
    public bool tofall = false;
    public bool isAuto2fall = true;

    [Header("PlayerOnWhichRoad")]
    // 用来记录玩家当前应该前进的位置
    [Range(1,3)]
    public int road = 2;
    [Range(1,2)]
    public int groundorsky = 1;

    //Sakata
    public bool isMove;

    public int valueOfSpeedUp = 1;

    // Update is called once per frame
    void Update()
    {
        if (isMove) {
            if (Input.GetKeyDown(KeyCode.A)) {
                toleft = true;
            }
            if (Input.GetKeyDown(KeyCode.D)) {
                toright = true;
            }

            if (Input.GetKeyDown(KeyCode.W)) {
                tojump = true;
            }

            if (Input.GetKeyDown(KeyCode.S)) {
                tofall = true;
            }
        }

            // 根据玩家输入计算路
            PlayerOnWhichRoad1();

        // 执行横向位置移动
        WhichRoad2Move2();

        // 空中的y参数为高度，地面的y参数为基础，二者之间就是角色的跳跃距离
        // 为优化跳跃手感，玩家的高度在超过总跳跃距离的一半时将减速
        // Debug.Log(jumppoint.transform.position.y - groundpoint.transform.position.y);

    }

    void JumpToTarget(){
        // 计算跳跃中速度开始衰减的点的高度Y值(第一段，小减速)
        float jumpspeeddownpointY1 = (jumppoint.transform.position.y - groundpoint.transform.position.y) * playerjumpmiddlespeedpoint + groundpoint.transform.position.y ;
        // Debug.Log(jumpspeeddownpointY1);
        // 计算跳跃中速度开始衰减的点的高度Y值(第二段，大减速，产生滞空效果)
        float jumpspeeddownpointY2 = (jumppoint.transform.position.y - groundpoint.transform.position.y) * playerjumpattenuationspeedpoint + groundpoint.transform.position.y ;
        // Debug.Log(jumpspeeddownpointY2);

        // 初段跳跃
        if(playerbodyPrefab.transform.position.y < jumpspeeddownpointY1){
            // Debug.Log("jump1");
            float step = playerjumpspeed * Time.deltaTime;

            Vector3 targetPosition = new Vector3(playerbodyPrefab.transform.position.x, jumppoint.transform.position.y, playerbodyPrefab.transform.position.z);
            playerbodyPrefab.transform.position = Vector3.MoveTowards(playerbodyPrefab.transform.position, targetPosition, step);
        }


        // 中段跳跃，小减速
        if(playerbodyPrefab.transform.position.y > jumpspeeddownpointY1 && playerbodyPrefab.transform.position.y < jumpspeeddownpointY2){
            // Debug.Log("jump2");
            float step = playerjumpspeed * playerjumpmiddlespeed * Time.deltaTime;

            // 为优化跳跃手感，玩家的高度在超过总跳跃距离的一半时将减速
            Vector3 targetPosition = new Vector3(playerbodyPrefab.transform.position.x, jumppoint.transform.position.y, playerbodyPrefab.transform.position.z);
            playerbodyPrefab.transform.position = Vector3.MoveTowards(playerbodyPrefab.transform.position, targetPosition, step);

        }

        // 末段跳跃，大减速
        if(playerbodyPrefab.transform.position.y > jumpspeeddownpointY2){
            // Debug.Log("jump3");
            float step = playerjumpspeed * playerjumpattenuationspeed * Time.deltaTime;

            // 为优化跳跃手感，玩家的高度在超过总跳跃距离的一半时将减速
            Vector3 targetPosition = new Vector3(playerbodyPrefab.transform.position.x, jumppoint.transform.position.y, playerbodyPrefab.transform.position.z);
            playerbodyPrefab.transform.position = Vector3.MoveTowards(playerbodyPrefab.transform.position, targetPosition, step);

        }


        // 当到达最高点时，是否自动激活坠落
        if(isAuto2fall && playerbodyPrefab.transform.position.y >= jumppoint.transform.position.y){
            tofall = true;
            // Debug.Log("fall");
        }
        
    }

    void FallToTarget(){
        // 计算坠落中速度开始衰减的点的高度Y值
        float jumpspeeddownpointY = (jumppoint.transform.position.y - groundpoint.transform.position.y) * playerfallattenuationspeedpoint + groundpoint.transform.position.y ;
        // Debug.Log(jumpspeeddownpointY);

        if(playerbodyPrefab.transform.position.y > jumpspeeddownpointY){
            float step = playerfallspeed * Time.deltaTime;

            Vector3 targetPosition = new Vector3(playerbodyPrefab.transform.position.x, groundpoint.transform.position.y, playerbodyPrefab.transform.position.z);
            playerbodyPrefab.transform.position = Vector3.MoveTowards(playerbodyPrefab.transform.position, targetPosition, step);

        }

        if(playerbodyPrefab.transform.position.y < jumpspeeddownpointY){
            float step = playerfallspeed * playerfallattenuationspeed * Time.deltaTime;

            Vector3 targetPosition = new Vector3(playerbodyPrefab.transform.position.x, groundpoint.transform.position.y, playerbodyPrefab.transform.position.z);
            playerbodyPrefab.transform.position = Vector3.MoveTowards(playerbodyPrefab.transform.position, targetPosition, step);

        }
          
    }

    // 根据玩家输入计算当前位置
    // 如果玩家在road1迅速向右两次，应该到road3
    // road在范围1~3
    void PlayerOnWhichRoad1(){
        if(toleft && road > 1){
            toleft = false;
            road -= 1;
            // Debug.Log("PlayerOn"+road);
        }else{
            toleft = false;
        }

        if(toright && road < 3){
            toright = false;
            road += 1;
            // Debug.Log("PlayerOn"+road);
        }else{
            toright = false;
        }

        if(tojump && groundorsky == 1){
            tojump = false;
            groundorsky += 1;
            // Debug.Log("PlayerOn"+groundorsky);
        }else{
            tojump = false;
        }

        if(tofall && groundorsky == 2){
            tofall = false;
            groundorsky -= 1;
            // Debug.Log("PlayerOn"+groundorsky);
        }else{
            tofall = false;
        }

    }

    void WhichRoad2Move2(){
        switch(road){
            case 1:
                MoveToTarget(leftpoint);
                break;
            case 2:
                MoveToTarget(middlepoint);
                break;
            case 3:
                MoveToTarget(rightpoint);
                break;
        }

        switch(groundorsky){
            case 1:
                FallToTarget();
                break;
            case 2:
                JumpToTarget();
                break;
        }


    }

    // 控制玩家的横向移动至指定位置的x参数
    void MoveToTarget(GameObject targetPositionGameObject){
        // 只动x轴，其他轴不变，便于跳跃时的横向移动
        float step = playerhorizontalmovespeed * Time.deltaTime * (valueOfSpeedUp + 1);

        Vector3 targetPosition = new Vector3(targetPositionGameObject.transform.position.x, playerbodyPrefab.transform.position.y, playerbodyPrefab.transform.position.z);
        playerbodyPrefab.transform.position = Vector3.MoveTowards(playerbodyPrefab.transform.position, targetPosition, step);
    }

}
