using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingEnemyController : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField]
    float power;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Launch();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Launch()
    {
        rb.AddForce(0, 0, -power, ForceMode.Impulse);
    }
}
