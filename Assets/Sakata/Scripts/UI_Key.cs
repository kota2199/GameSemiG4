using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Key : MonoBehaviour
{
    [HideInInspector] 
    public bool OnGet = false;
    public bool OnLost = false;

    public int ItemCount;

    [Space(10)]

    /*
    public Image Key1;
    public Image Key2;
    public Image Key3;
    */

    //GameManager GMScript;

    public AudioClip ItemGetSound;
    public AudioClip ItemLostSound;
    AudioSource audioSource;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        ItemCount = 0;
        /*
        GameObject GameManager = GameObject.Find("GameManager");
        GMScript = GameManager.GetComponent<GameManager>();
        */

        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (OnGet) {
            OnLost = false;
            switch (ItemCount) {
                case 0:
                    audioSource.PlayOneShot(ItemGetSound);
                    anim.Play("ItemGet_Lv1Anim");
                    ItemCount = 1;
                    OnGet = false;
                    break;
                case 1:
                    audioSource.PlayOneShot(ItemGetSound);
                    anim.Play("ItemGet_Lv2Anim");
                    ItemCount = 2;
                    OnGet = false;
                    break;
                case 2:
                    audioSource.PlayOneShot(ItemGetSound);
                    anim.Play("ItemGet_Lv3Anim");
                    ItemCount = 3;
                    OnGet = false;
                    break;
                default:
                    OnGet = false;
                    break;
            }
        }
        if (OnLost) {
            OnGet = false;
            switch (ItemCount) {
                case 1:
                    //audioSource.PlayOneShot(KeyItemSound);
                    anim.Play("ItemLost_Lv1Anim");
                    ItemCount = 0;
                    OnLost = false;
                    break;
                case 2:
                    //audioSource.PlayOneShot(KeyItemSound);
                    anim.Play("ItemLost_Lv2Anim");
                    ItemCount = 0;
                    OnLost = false;
                    break;
                case 3:
                    //audioSource.PlayOneShot(KeyItemSound);
                    anim.Play("ItemLost_Lv3Anim");
                    ItemCount = 0;
                    OnLost = false;
                    break;
                default:
                    OnLost = false;
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            OnLost = true;
        }
    }
}
