using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public bool FadeIn = false;
    public bool FadeOut = false;

    public float FadeTime;

    private float alpha;
    Image fade;

    void Start() {
        fade = GetComponent<Image>();
        alpha = fade.color.a;
    }

    void Update()
    {
        if (FadeIn) {
            FadeInSystem();
        }
        if (FadeOut) {
            FadeOutSystem();
        }
    }

    void FadeInSystem() {
        alpha -= FadeTime;
        fade.color = new Color(0, 0, 0, alpha);
        if (alpha <= 0) {
            FadeIn = false;
        }
    }

    void FadeOutSystem() {
        alpha += FadeTime;
        fade.color = new Color(0, 0, 0, alpha);
        if (alpha >= 1) {
            FadeOut = false;
        }
    }
}
