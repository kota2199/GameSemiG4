using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Key : MonoBehaviour
{
    //THIS IS ACCELE ITEM SCRIPT.  

    [HideInInspector]
    public bool OnGet = false;
    public bool OnLost = false;
    public int ItemCount;


    [Space(10)]


    public AudioClip KeyItemSound;
    AudioSource audioSource;

    private Animator anim;

    [SerializeField]
    private PlayerMovement playerMovement;


    public AudioClip ItemGetSound;
    public AudioClip ItemLostSound;

    // Start is called before the first frame update
    void Start()
    {

        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        playerMovement.valueOfSpeedUp = ItemCount;

        if (OnGet)
        {
            OnLost = false;
            switch (ItemCount)
            {
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
        if (OnLost)
        {
            OnGet = false;
            switch (ItemCount)
            {
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
    }

    public void MinusItemCount()
    {
        ItemCount = 1;
    }
}
