using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameramove : MonoBehaviour
{


    // 实现伴随玩家的位置变化
    // 实现自动上下浮动

    public GameObject playermovement;

    // 是否晃动镜头
    public bool is_shock = false;
    // 震动的强度
    float shockAmount = 5f;
    // 震动的时间
    float shockDuration = 0.5f;
    // 震动的速度
    float shockSpeed = 2.0f;
    // 初始位置
    private Vector3 initialPosition;

    // 摄像机横向移动速度
    public float camera_horizontal_move_speed = 1f;
    // 摄像机纵向移动速度
    public float camera_jump_move_speed = 1f;

    int player_which_road;
    int player_ground_or_sky;

    [Range(5f,10f)]
    public float min_X_Angle = 9f;
    [Range(10f,15f)]
    public float max_X_Angle = 11f;
    [Range(0.5f,2f)]
    public float rotationSpeed = 0.5f;



    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        // 根据玩家横向位置变化摄像机横向位置
        Camera_Horizontal_Move();
        // 摄像机伴随玩家跳跃
        Camera_Jump_Move();
        // 镜头自动晃动
        Camera_Breathe_Move();
        // 震动镜头
        Camera_Shock();

    }

    void Camera_Shock(){
        if (is_shock){
            initialPosition = transform.position;

            if (shockDuration > 0){
                Vector3 shockPosition = initialPosition + Random.insideUnitSphere * shockAmount;

                transform.position = Vector3.Lerp(transform.position, shockPosition, Time.deltaTime * shockSpeed);

                shockDuration -= Time.deltaTime;
            }else{
                    is_shock = false;
                    shockDuration = 0.5f;
            }
        }
    }



    void Camera_Breathe_Move(){
        // 镜头自动晃动
        float currentXAngle = transform.rotation.eulerAngles.x;
        float newAngle = Mathf.Lerp(min_X_Angle, max_X_Angle, Mathf.PingPong(Time.time * rotationSpeed, 1f));
        transform.rotation = Quaternion.Euler(newAngle, 0f, 0f);
    }

    void Camera_Jump_Move(){
        // 获取玩家高度位置
        player_ground_or_sky = playermovement.GetComponent<PlayerMovement>().groundorsky;

        float step = camera_jump_move_speed * Time.deltaTime;

        if(player_ground_or_sky == 2){
            Vector3 targetPosition = new Vector3(transform.position.x, 5.5f, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
        }else{
            Vector3 targetPosition = new Vector3(transform.position.x, 4.5f, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
        }
    }


    void Camera_Horizontal_Move(){
        // 获取玩家横向位置
        player_which_road = playermovement.GetComponent<PlayerMovement>().road;

        // 根据玩家横向位置变化摄像机位置
        switch(player_which_road){
            case 1:
                MoveToTarget(-2f);
                break;
            case 2:
                MoveToTarget(-1.5f);
                break;
            case 3:
                MoveToTarget(-1f);
                break;
        }
    }


    // 控制玩家的横向移动至指定位置的x参数
    void MoveToTarget(float targetPosition_X){
        // 只动x轴，其他轴不变，便于跳跃时的横向移动
        float step = camera_horizontal_move_speed * Time.deltaTime;

        Vector3 targetPosition = new Vector3(targetPosition_X, transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
    }
}
