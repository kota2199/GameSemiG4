using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowManController : MonoBehaviour
{
    private Transform player; // vC[ÌTransformðwè
    [SerializeField]
    private float attackInterval = 5.0f; // UÌÔuibj
    [SerializeField]
    private float attackSpeed = 5.0f; // UÌ¬x
    [SerializeField]
    private Vector3 attackPositionOffset = new Vector3(0, 5, 0); // vC[Éü©¤ÊuÌItZbg

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
                // UðJn
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
                // vC[Ìûüðü­
                transform.LookAt(player);

                // vC[Éü©ÁÄU
                float step = attackSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, attackEndPosition + attackPositionOffset, step);
            }
            else
            {
                // UðI¹µAAÒðJn
                isAttacking = false;
            }
        }

        if (!isAttacking)
        {
            // AÒ
            float step = attackSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, attackStartPosition, step);

            // AÒ®¹
            if (Vector3.Distance(transform.position, attackStartPosition) < 0.1f)
            {
                transform.position = attackStartPosition;
            }
        }
    }
}
