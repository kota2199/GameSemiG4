using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogCtrl : MonoBehaviour
{

    public bool fog_is_true = true;
    [Range(0.01f,0.03f)]
    public float fogDensity = 0.025f;

    // Update is called once per frame
    void Update(){
        RenderSettings.fog = fog_is_true;
        RenderSettings.fogDensity = fogDensity;

    }
}
