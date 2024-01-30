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



    public AudioClip ItemGetSound;
    public AudioClip ItemLostSound;
    AudioSource audioSource;

    private Animator anim;

    [SerializeField]
    private int itemCount = 0;

    [SerializeField]
    private PlayerMovement playerMovement;

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
        //else if(GMScript.BossStage == 1){
        //    anim.Play("KeyCompleteAnim");
        //}


        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            OnLost = true;

        if (Input.GetKeyDown(KeyCode.Keypad1)) {
            itemCount = 1;
        }
        if (Input.GetKeyDown(KeyCode.Keypad2)) {
            itemCount = 2;
        }
        if (Input.GetKeyDown(KeyCode.Keypad3)) {
            itemCount = 3;

        }
    }

    public void AddItemCount()
    {
        switch (itemCount)
        {
            case 1:
                audioSource.PlayOneShot(KeyItemSound);
                Key1.sprite = CompItem_1;
                anim.Play("Key1Anim");
                //GMScript.KeyCount += 1;
                itemCount++;
                playerMovement.valueOfSpeedUp = itemCount;
                break;
            case 2:
                audioSource.PlayOneShot(KeyItemSound);
                Key2.sprite = CompItem_2;
                anim.Play("Key2Anim");
                //GMScript.KeyCount += 1;
                itemCount++;
                playerMovement.valueOfSpeedUp = itemCount;
                break;
            case 3:
                audioSource.PlayOneShot(KeyItemSound);
                Key3.sprite = CompItem_3;
                anim.Play("Key3Anim");
                //GMScript.KeyCount += 1;
                itemCount++;
                playerMovement.valueOfSpeedUp = itemCount;
                break;
            default:
                break;
        }
    }

    public void MinusItemCount()
    {
        itemCount = 1;
        Key1.sprite = item_1;
        Key2.sprite = item_2;
        Key3.sprite = item_3;
        playerMovement.valueOfSpeedUp = itemCount;
    }
}
