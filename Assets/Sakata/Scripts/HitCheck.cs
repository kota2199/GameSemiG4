using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCheck : MonoBehaviour
{
    public bool isHit = false;
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            isHit = true;
        }
    }
}
