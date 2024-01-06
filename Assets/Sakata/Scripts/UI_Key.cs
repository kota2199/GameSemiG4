using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Key : MonoBehaviour
{
    [HideInInspector] public int OnKey = 0;

    [Space(10)]

    public Image Key1;
    public Image Key2;
    public Image Key3;

    GameManager GMScript;

    public AudioClip KeyItemSound;
    AudioSource audioSource;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        GameObject GameManager = GameObject.Find("GameManager");
        GMScript = GameManager.GetComponent<GameManager>();

        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();

        Key1.color = new Color(0.6f, 0.6f, 0.6f, 1);
        Key2.color = new Color(0.6f, 0.6f, 0.6f, 1);
        Key3.color = new Color(0.6f, 0.6f, 0.6f, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if(GMScript.BossStage == 0) {
            switch (OnKey) {
                case 1:
                    Key1.color = new Color(1, 1, 1, 1);
                    audioSource.PlayOneShot(KeyItemSound);
                    anim.Play("Key1Anim");
                    GMScript.KeyCount += 1;
                    OnKey = 0;
                    break;
                case 2:
                    Key2.color = new Color(1, 1, 1, 1);
                    audioSource.PlayOneShot(KeyItemSound);
                    anim.Play("Key2Anim");
                    GMScript.KeyCount += 1;
                    OnKey = 0;
                    break;
                case 3:
                    Key3.color = new Color(1, 1, 1, 1);
                    audioSource.PlayOneShot(KeyItemSound);
                    anim.Play("Key3Anim");
                    GMScript.KeyCount += 1;
                    OnKey = 0;
                    break;
                default:
                    break;
            }
        }else if(GMScript.BossStage == 1){
            anim.Play("KeyCompleteAnim");
        }

        if (Input.GetKeyDown(KeyCode.Keypad1)) {
            OnKey = 1;
        }
        if (Input.GetKeyDown(KeyCode.Keypad2)) {
            OnKey = 2;
        }
        if (Input.GetKeyDown(KeyCode.Keypad3)) {
            OnKey = 3;
        }
    }
}
