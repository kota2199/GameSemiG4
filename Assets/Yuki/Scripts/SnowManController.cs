using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowManController : MonoBehaviour
{
    private Transform player; // �v���C���[��Transform���w��
    [SerializeField]
    private float attackInterval = 5.0f; // �U���̊Ԋu�i�b�j
    [SerializeField]
    private float attackSpeed = 5.0f; // �U���̑��x
    [SerializeField]
    private Vector3 attackPositionOffset = new Vector3(0, 5, 0); // �v���C���[�Ɍ������ʒu�̃I�t�Z�b�g

    private Vector3 originalPosition;
    private Vector3 attackStartPosition;
    private Vector3 attackEndPosition;
    private bool isAttacking = false;
    private float elapsedTime = 0.0f;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        originalPosition = transform.position;
        attackStartPosition = transform.position;
    }

    private void Update()
    {
        attackStartPosition = new Vector3(player.transform.position.x, player.transform.position.y + 1, player.transform.position.z + 10);
        if (!isAttacking)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= attackInterval)
            {
                // �U�����J�n
                isAttacking = true;
                elapsedTime = 0.0f;
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
                isAttacking = false;
            }
        }

        if (!isAttacking)
        {
            // �A�Ғ�
            float step = attackSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, attackStartPosition, step);

            // �A�Ҋ���
            if (Vector3.Distance(transform.position, attackStartPosition) < 0.1f)
            {
                transform.position = attackStartPosition;
            }
        }
    }
}
