using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obs_Ctrl : MonoBehaviour
{
    // （Manager/Obs_name<=/Obs_Colider）
    // 这个脚本请挂在每个障碍物上，控制物体自己的行动。
    // Manager负责控制所有障碍物的启动

    [Header("Transform")]
    // 道路(取其x值)
    public GameObject which_way;
    // 起点(取其z值)
    public GameObject start_point;
    // 终点(取其z值)
    public GameObject end_point;
    // 起点
    private Vector3 startPosition;
    // 终点
    private Vector3 endPosition;

    [Header("Speed")]
    // 初始速度（初始速度保证和地面移动速度相对静止移动）
    public float obs_initial_speed = 10f;
    // 附加速度（滚石等快速移动物体）
    [Range(0,20)]
    public float obs_add_speed = 0f;

    [Header("SketeBoard Paramter")]
    // 该物体碰撞玩家后实现的效果
    public bool is_damege;
    public bool is_heal;

    [Header("State")]
    // 是否在终点等待（指示性bool，也就判断是否在终点可以被回归起点）
    public bool is_waiting;
    // 回归起点的开关
    public bool is2start;


    public UI_HP ui_hp;

    [SerializeField]
    private SnowManController snowManCtrl;

    [SerializeField]
    private UI_Key ui_key;

    private GameManager manager;

    // 是否持续触发

    // 特殊效果触发位置

    void Start(){
        // 初始化该物体的起始点位置
        startPosition = new Vector3(which_way.transform.position.x, transform.position.y, start_point.transform.position.z);
        endPosition = new Vector3(which_way.transform.position.x, transform.position.y, end_point.transform.position.z);

        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update(){
        // 控制物体回归起始位置
        return2start();

        // 控制物体持续向终点移动
        ismoving();

        // 判定物体是否在终点等待
        iswaiting2start();
        // obs_add_speed = snowManCtrl.speedOfObs;
    }

    void iswaiting2start(){
        // 判定物体是否在终点等待
        if(transform.position == endPosition){
            is_waiting = true;
        }
    }

    void return2start(){
        // 控制物体回归起始位置
        // 如果正在等待且激活
        if(is2start && is_waiting){
            // 回到所属道路的起始位置
            transform.position = startPosition;
            is2start = false;
            is_waiting = false;
        }
    }

    void ismoving(){
        // 获取物体到目标的方向
        Vector3 direction = endPosition - transform.position;

        // 计算每帧应该移动的距离
        float moveSpeed = obs_initial_speed + obs_add_speed;
        float step = moveSpeed * Time.deltaTime;

        // 使用MoveTowards函数使物体朝着目标点移动
        if (!manager.isGameOver)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPosition, step);
        }
    }

    void OnTriggerEnter(Collider other){
        // 检查碰撞的物体是否带有"Player"标签,触发player的脚本进行伤害和治疗判定
        // player带上刚体rigid，本物体打开触发器

        // 对玩家的数值产生效果
        if (other.CompareTag("Player")){

            // 测试时player上没有脚本，所以报错无所谓
            if(is_damege){
                try{
                    other.GetComponent<SkatebdCtrl>().is_dameged = true;
                    //hithp
                }catch{
                    // 测试用的player的脚本 Player_HP_Obs_test
                    other.GetComponent<Player_HP_Obs_test>().is_dameged = true;
                    ui_hp.Hit_HP = true;
                }

                // Player_HP_Obs_test 测试用player的脚本
                // other.GetComponent<Player_HP_Obs_test>().is_dameged = true;

                // SkatebdCtrl
                // other.GetComponent<SkatebdCtrl>().is_dameged = true;
                //ui_key.MinusItemCount();
            }

            if(is_heal){
                Debug.Log("is_healed");

                try{
                    other.GetComponent<SkatebdCtrl>().is_healed = true;
                }catch{
                    other.GetComponent<Player_HP_Obs_test>().is_healed = true;
                }

                // Player_HP_Obs_test 测试用player的脚本
                // other.GetComponent<Player_HP_Obs_test>().is_healed = true;

                // SkatebdCtrl
                // other.GetComponent<SkatebdCtrl>().is_healed = true;
            }
        }
    }
}
