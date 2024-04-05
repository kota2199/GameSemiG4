using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingEnemyController : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField]
    private float power;
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
    private void Launch()
    {
        rb.AddForce(0, 0, -power, ForceMode.Impulse);
    }
}
