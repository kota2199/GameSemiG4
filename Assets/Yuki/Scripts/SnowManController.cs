using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowManController : MonoBehaviour
{
    [SerializeField]
    private float attackInterval = 5.0f; // �U���̊Ԋu�i�b�j
    [SerializeField]
    private float attackSpeed = 5.0f; // �U���̑��x
    [SerializeField]
    private Vector3 attackPositionOffset = new Vector3(0, 5, 0); // �v���C���[�Ɍ������ʒu�̃I�t�Z�b�g

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

    private void Start()
    {
        originalPosition = transform.position;
        attackStartPosition = transform.position;

        StartCoroutine(attack());
    }

    private IEnumerator attack()
    {
        //�{�Ԃ�while�Ȃ��B�{�X���|�ꂽ����s
        while (true)
        {
            yield return new WaitForSeconds(20);
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
        yield return new WaitForSeconds(5);
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
        if (isAttacking)
        {
            if (Mode == attackMode.pattern1)
            {
                attackStartPosition = new Vector3(player.transform.position.x, player.transform.position.y + 4, player.transform.position.z + 15);

                if (!attackToPlayer_p1)
                {
                    elapsedTime += Time.deltaTime;
                    if (elapsedTime >= attackInterval)
                    {
                        // �U�����J�n
                        elapsedTime = 0.0f;
                        attackToPlayer_p1 = true;
                        attackStartPosition = transform.position;
                        attackEndPosition = player.transform.position;
                    }
                }
                else
                {
                    if (Vector3.Distance(transform.position, attackEndPosition + attackPositionOffset) > 0.1f)
                    {
                        // �v���C���[�̕���������
                        transform.LookAt(player);

                        // �v���C���[�Ɍ������čU����
                        float step = attackSpeed * Time.deltaTime;
                        transform.position = Vector3.MoveTowards(transform.position, attackEndPosition + attackPositionOffset, step);
                    }
                    else
                    {
                        // �U�����I�����A�A�҂��J�n
                        attackToPlayer_p1 = false;
                        returnToBase_p1 = true;
                    }
                }

                if (returnToBase_p1)
                {
                    // �A�Ғ�
                    float step = attackSpeed * Time.deltaTime;
                    transform.position = Vector3.MoveTowards(transform.position, attackStartPosition, step);

                    // �A�Ҋ���
                    if (Vector3.Distance(transform.position, attackStartPosition) < 0.1f)
                    {
                        transform.position = attackStartPosition;
                        returnToBase_p1 = false;
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
