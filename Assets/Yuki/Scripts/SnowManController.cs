using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnowManController : MonoBehaviour
{
    [SerializeField]
    private float attackInterval = 5.0f; // ?U???????u?i?b?j
    [SerializeField]
    private float attackSpeed = 5.0f; // ?U???????x
    [SerializeField]
    private Vector3 attackPositionOffset = new Vector3(0, 5, 0); // ?v???C???[???????????u???I?t?Z?b?g

    [SerializeField]
    private Transform player;

    [SerializeField]
    private TimerCounter timerCounter;

    private bool isAttacking = false;

    private Vector3 originalPosition;
    private Vector3 attackStartPosition;
    private Vector3 attackEndPosition;
    private bool attackToPlayer_p1 = false;
    private bool returnToBase_p1 = false;
    private float elapsedTime = 0.0f;

    public enum attackMode
    {
        pattern1, pattern2, pattern3
    }

    public attackMode Mode;

    private int attackCount = 0;

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

    private void Start()
    {
        originalPosition = transform.position;
        attackStartPosition = transform.position;

        myHP = maxHP;

        StartCoroutine(attack());
    }

    private IEnumerator attack()
    {
        //?{????while?????B?{?X???|?????????s
        while (true)
        {
            yield return new WaitForSeconds(20);
            hpUi.SetActive(true);
            if (attackCount < 2)
            {
                SetAttackMode(attackCount);
                attackCount++;
            }
            else
            {
                int index = Random.Range(0, 3);
                SetAttackMode(index);
            }
        }
        if(attackCount < 2)
        {
            SetAttackMode(attackCount);
            attackCount ++;
        }
        else
        {
            int index = Random.Range(0, 3);
            SetAttackMode(index);
        }
    }

    public void SetAttackMode(int a)
    {
        switch (a)
        {
            case 0:
                Mode = attackMode.pattern1;
                break;

            case 1:
                Mode = attackMode.pattern2;
                break;

            case 2:
                Mode = attackMode.pattern3;
                break;

            default:
            
                break;
        }
        isAttacking = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            myHP--;
        }
        if (isAttacking)
        {
            ui_boss.BossHPSlider.maxValue = maxHP;
            ui_boss.BossHPSlider.value = myHP;
            //Attack mode is 1.
            if (Mode == attackMode.pattern1)
            {
                attackStartPosition = new Vector3(player.transform.position.x, player.transform.position.y + 4, player.transform.position.z + 15);

                if (!attackToPlayer_p1)
                {
                    elapsedTime += Time.deltaTime;
                    if (elapsedTime >= attackInterval)
                    {
                        // ?U?????J?n
                        elapsedTime = 0.0f;
                        attackToPlayer_p1 = true;
                        ableToAttack = true;
                        attackStartPosition = transform.position;
                        attackEndPosition = player.transform.position;
                    }
                }
                else
                {
                    if (Vector3.Distance(transform.position, attackEndPosition + attackPositionOffset) > 0.1f)
                    {
                        // ?v???C???[????????????
                        transform.LookAt(player);

                        // ?v???C???[???????????U????
                        float step = attackSpeed * Time.deltaTime;
                        transform.position = Vector3.MoveTowards(transform.position, attackEndPosition + attackPositionOffset, step);
                    }
                    else
                    {
                        // ?U?????I?????A?A?????J?n
                        attackToPlayer_p1 = false;
                        returnToBase_p1 = true;
                    }
                }

                if (returnToBase_p1)
                {
                    // ?A????
                    float step = attackSpeed * Time.deltaTime;
                    transform.position = Vector3.MoveTowards(transform.position, attackStartPosition, step);

                    // ?A??????
                    if (Vector3.Distance(transform.position, attackStartPosition) < 0.1f)
                    {
                        transform.position = attackStartPosition;
                        returnToBase_p1 = false;
                        ableToAttack = false;
                    }
                }
            }

            if (Mode == attackMode.pattern2)
            {
                Debug.Log("Mode2");
            }

            if (Mode == attackMode.pattern3)
            {
                Debug.Log("Mode3");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isAttacking)
        {
            //PlayerHP--;
            // もしPlayerのTagを持っているものとコレクションした
            if (other.CompareTag("Player"))
            {
                Debug.Log("is_dameged");
                // 方法一、この（）にダメージを入れ、負数を使ってください
                // (Assets\Wang\SkatebdCtrl.cs\115)
                other.GetComponent<SkatebdCtrl>().SketeBoard_IsDameged(-50);
                // 方法二、障害物と同じダメージのメソッドを使って（ダメージが１０点）
                // (Assets\Wang\Obstructions\ObsPrefab\Obs_Ctrl.cs\105)
                other.GetComponent<SkatebdCtrl>().is_dameged = true;
                // 方法三、Playerとコレクションすればメソッドを使わず、プレイヤーのHPにダメージを与える
                skatebdCtrl.GetComponent<SkatebdCtrl>().sketeboard_HP -= 1;
                Debug.Log(skatebdCtrl.GetComponent<SkatebdCtrl>().sketeboard_HP);

                ui_key.MinusItemCount();
            }
            else if (other.CompareTag("Player") && !isAttacking)
            {
                myHP--;
                if (myHP <= 0)
                {
                    isAttacking = false;
                    //FlayAway
                    //hpUi.SetActive(false);
                    maxHP += 2;
                    myHP = maxHP;
                }
            }
        }
    }
}
