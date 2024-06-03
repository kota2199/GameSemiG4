using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnowManController : MonoBehaviour
{
    [SerializeField]
    private float attackInterval = 8.0f;
    [SerializeField]
    private int setInterval = 20;
    [SerializeField]
    private float attackSpeed = 5.0f;

    private int attackCount; 
    [SerializeField]
    private int maxAttackCount = 3;

    [SerializeField]
    private Vector3 attackPositionOffset = new Vector3(0, 5, 0);

    [SerializeField]
    private Transform player;

    [SerializeField]
    private TimerCounter timerCounter;

    private bool isAttacking = false;

    private bool readyToAttack = false;

    private Vector3 standByPosition;

    private Vector3 attackStartPosition;
    private Vector3 attackEndPosition;

    private bool attackToPlayer_p1 = false;
    private bool returnToBase_p1 = false;

    private float elapsedTime = 0.0f;

    public int speedOfObs = 0;

    [SerializeField]
    private int amountOfSpeedUp = 1;

    [SerializeField]
    private int maxHP = 3;

    [SerializeField]
    private int myHP;

    private bool ableToAttack = false;

    [SerializeField]
    private GameObject hpUi;

    [SerializeField]
    private UI_Boss ui_boss;

    [SerializeField]
    private SkatebdCtrl skatebdCtrl;

    [SerializeField]
    private UI_Key ui_key;

    [SerializeField]
    private ObsManager obsManager;

    private bool isReturnToBase = false;

    [SerializeField]
    private GameObject darumaObj;

    [SerializeField]
    private Texture tex_blue, tex_red, tex_purple;

    private void Start()
    {
        standByPosition = transform.position;
        attackStartPosition = player.transform.position + new Vector3(0,5,12);

        myHP = maxHP;

        attackCount = maxAttackCount;

        StartCoroutine(Attack());
    }


    //攻撃の準備開始
    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(setInterval);

        //// (Assets\Wang\Obstructions\Obstraction/ObsManager.isPause)がtrueになるまではボス戦に移行しない。
        if (!obsManager.isPause)
        {
            SetBossTexture();
        }
        else
        {
            StartCoroutine(Attack());
        }
    }

    //レベルに応じてモデルのテクスチャを変更する
    public void SetBossTexture()
    {
        if(maxAttackCount <= 5)
        {
            darumaObj.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", tex_blue);
        }
        else if (maxAttackCount <= 9)
        {
            darumaObj.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", tex_red);
        }
        else if (maxAttackCount <= 13)
        {
            darumaObj.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", tex_purple);
        }

        hpUi.SetActive(true);
        isAttacking = true;
    }

    private void Update()
    {

        //攻撃フラグがtrueであれば

        if (isAttacking)
        {
            //Playerの前に、Playerを向いた方向でセットする
            if (!readyToAttack && Vector3.Distance(transform.position, attackStartPosition) > 0.1f)
            {
                float step = attackSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, attackStartPosition, step);
            }
            else
            {
                readyToAttack = true;
            }

            //所定の位置についたので攻撃開始
            if (readyToAttack)
            {
                ui_boss.BossHPSlider.maxValue = maxAttackCount;
                ui_boss.BossHPSlider.value = attackCount;

                transform.LookAt(player);

                if (!attackToPlayer_p1)
                {
                    //1. 攻撃間のインターバルが所定のインターバルを超えたら、そのときのPlayerの座標を攻撃の目標地点に設定し、攻撃スタート
                    elapsedTime += Time.deltaTime;
                    if (elapsedTime >= attackInterval)
                    {
                        elapsedTime = 0.0f;
                        attackEndPosition = player.transform.position;
                        attackToPlayer_p1 = true;
                        ableToAttack = true;
                    }
                }
                else
                {
                    //2. 攻撃の目標地点に到達するまで攻撃目標地点に向かって進む。到達したら攻撃目標地点から元いた場所に戻る。
                    if (Vector3.Distance(transform.position, attackEndPosition + attackPositionOffset) > 0.05f)
                    {
                        float step = attackSpeed * Time.deltaTime;
                        transform.position = Vector3.MoveTowards(transform.position, attackEndPosition + attackPositionOffset, step);
                    }
                    else
                    {
                        attackToPlayer_p1 = false;
                        returnToBase_p1 = true;
                    }
                }

                //3. 最初にセットされいた座標に戻る。
                if (returnToBase_p1)
                {
                    float step = attackSpeed * Time.deltaTime;
                    transform.position = Vector3.MoveTowards(transform.position, attackStartPosition, step);

                    //4. 最初にセットされていた座標に戻って、攻撃回数が残っていれば1に戻り、残っていない場合は画面外にはける
                    if (Vector3.Distance(transform.position, attackStartPosition) < 0.1f)
                    {
                        if (attackCount <= 0)
                        {
                            returnToBase_p1 = false;
                            ableToAttack = false;

                            isReturnToBase = true;
                        }
                        else
                        {
                            attackCount--;
                            transform.position = attackStartPosition;
                            returnToBase_p1 = false;
                            ableToAttack = false;
                        }
                    }
                }

                //5. 画面外にはけるフラグがtrueであれば、所定の座標に移動し、到達したらフラグをfalseにし、次の攻撃を開始する
                if (isReturnToBase)
                {
                    if (Vector3.Distance(transform.position, standByPosition) > 10f)
                    {
                        float step = attackSpeed * Time.deltaTime;
                        transform.position = Vector3.MoveTowards(transform.position, standByPosition, step);
                    }
                    else
                    {
                        hpUi.SetActive(false);
                        isReturnToBase = false;
                        isAttacking = false;
                        readyToAttack = false;

                        maxAttackCount += 2;
                        speedOfObs += 5;
                        attackCount = maxAttackCount;

                        StartCoroutine(Attack());
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && ableToAttack)
        {
            if (other.CompareTag("Player"))
            {
                // (Assets\Wang\Obstructions\ObsPrefab\Obs_Ctrl.cs\105)
                //Playerにダメージ
                other.GetComponent<SkatebdCtrl>().is_dameged = true;
                //スノボをサイズを小さくする
                skatebdCtrl.GetComponent<SkatebdCtrl>().sketeboard_HP -= 1;
            }
        }
    }
}
