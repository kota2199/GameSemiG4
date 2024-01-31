using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healitemmove : MonoBehaviour
{
    public float minHeight = 0.0f;
    public float maxHeight = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, 60f * Time.deltaTime);

        // 计算新的高度值
        float newY = Mathf.PingPong(Time.time * 1f, maxHeight - minHeight) + minHeight;

        // 更新物体的位置
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
