using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLightCtrl : MonoBehaviour
{

    public Light directionalLight;
    public float rotationSpeed = 10.0f;

    // Update is called once per frame
    void Update()
    {
        // 获取当前的光源方向
        Vector3 currentRotation = directionalLight.transform.rotation.eulerAngles;

        currentRotation.y += rotationSpeed * Time.deltaTime;

        directionalLight.transform.rotation = Quaternion.Euler(currentRotation);
    }
}
