using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowManController : MonoBehaviour
{
    private Transform player; // プレイヤーのTransformを指定
    [SerializeField]
    private float attackInterval = 5.0f; // 攻撃の間隔（秒）
    [SerializeField]
    private float attackSpeed = 5.0f; // 攻撃の速度
    [SerializeField]
    private Vector3 attackPositionOffset = new Vector3(0, 5, 0); // プレイヤーに向かう位置のオフセット

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
                // 攻撃を開始
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
                // プレイヤーの方向を向く
                transform.LookAt(player);

                // プレイヤーに向かって攻撃中
                float step = attackSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, attackEndPosition + attackPositionOffset, step);
            }
            else
            {
                // 攻撃を終了し、帰還を開始
                isAttacking = false;
            }
        }

        if (!isAttacking)
        {
            // 帰還中
            float step = attackSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, attackStartPosition, step);

            // 帰還完了
            if (Vector3.Distance(transform.position, attackStartPosition) < 0.1f)
            {
                transform.position = attackStartPosition;
            }
        }
    }
}
