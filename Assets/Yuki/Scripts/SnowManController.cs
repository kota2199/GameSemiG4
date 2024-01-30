using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnowManController : MonoBehaviour
{
    [SerializeField]
    private float attackInterval = 8.0f; // ?U???????u?i?b?j
    [SerializeField]
    private int setInterval = 20;
    [SerializeField]
    private float attackSpeed = 5.0f; // ?U???????x

    private int attackCount; 
    [SerializeField]
    private int maxAttackCount = 3;

    [SerializeField]
    private Vector3 attackPositionOffset = new Vector3(0, 5, 0); // ?v???C???[???????????u???I?t?Z?b?g

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

    public enum attackMode
    {
        pattern1, pattern2, pattern3
    }

    public attackMode Mode;

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

    private bool isReturnToBase = false;

    private void Start()
    {
        standByPosition = transform.position;
        attackStartPosition = player.transform.position + new Vector3(0,5,15);

        myHP = maxHP;

        attackCount = maxAttackCount;

        StartCoroutine(attack());
    }

    private IEnumerator attack()
    {
        yield return new WaitForSeconds(setInterval);
        SetAttackMode(0);
        //???????????????????
        //if (attackCount < 2)
        //{
        //    SetAttackMode(attackCount);
        //    attackCount++;
        //}
        //else
        //{
        //    int index = Random.Range(0, 3);
        //    SetAttackMode(index);
        //}
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
        hpUi.SetActive(true);
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
            //ReadyToAttack
            if (!readyToAttack && Vector3.Distance(transform.position, attackStartPosition) > 0.1f)
            {
                float step = attackSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, attackStartPosition, step);
            }
            else
            {
                readyToAttack = true;
            }

            if (readyToAttack)
            {
                ui_boss.BossHPSlider.maxValue = maxAttackCount;
                ui_boss.BossHPSlider.value = attackCount;
                //Attack mode is 1.
                if (Mode == attackMode.pattern1)
                {
                    transform.LookAt(player);
                    if (!attackToPlayer_p1)
                    {
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
                        if (Vector3.Distance(transform.position, attackEndPosition + attackPositionOffset) > 0.05f)
                        {
                            //transform.LookAt(player);

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
                            if(attackCount <= 0)
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

                    if (isReturnToBase)
                    {
                        if (Vector3.Distance(transform.position, standByPosition) > 10f)
                        {
                            Debug.Log("Distance" + Vector3.Distance(transform.position, standByPosition));
                            float step = attackSpeed * Time.deltaTime;
                            transform.position = Vector3.MoveTowards(transform.position, standByPosition, step);
                        }
                        else
                        {
                            hpUi.SetActive(true);
                            isReturnToBase = false;
                            isAttacking = false;
                            readyToAttack = false;

                            maxAttackCount += 2;
                            speedOfObs += 5;
                            attackCount = maxAttackCount;

                            StartCoroutine(attack());
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && ableToAttack)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("is_dameged");
                // (Assets\Wang\SkatebdCtrl.cs\115)
                //other.GetComponent<SkatebdCtrl>().SketeBoard_IsDameged(-50);
                // (Assets\Wang\Obstructions\ObsPrefab\Obs_Ctrl.cs\105)
                other.GetComponent<SkatebdCtrl>().is_dameged = true;
                skatebdCtrl.GetComponent<SkatebdCtrl>().sketeboard_HP -= 1;

                //ui_key.MinusItemCount();
            }
            //???????????????????????
            //else if (other.CompareTag("Player") && !ableToAttack)
            //{
            //    myHP--;
            //    if (myHP <= 0)
            //    {
            //        isAttacking = false;
            //        //FlayAway
            //        //hpUi.SetActive(false);
            //        maxHP += 2;
            //        myHP = maxHP;
            //    }
            //}
        }
    }
}
