using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HP : MonoBehaviour
{

    public Image Red;
    public Image Green;
    public Image Blue;

    public bool Hit_HP;
    public bool Heal_HP;

    public AudioClip HPDeadSound;
    public AudioClip HPHealSound;
    AudioSource audioSource;

    PlayerHPManager hpManager;

    public GameObject Player;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        hpManager = Player.GetComponent<PlayerHPManager>();

        anim = GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();

        switch (hpManager.hp) {
            case 1:
                Red.enabled = true;
                Green.enabled = false;
                Blue.enabled = false;
                break;
            case 2:
                Red.enabled = true;
                Green.enabled = true;
                Blue.enabled = false;
                break;
            case 3:
                Red.enabled = true;
                Green.enabled = true;
                Blue.enabled = true;
                break;
            default:
                Red.enabled = false;
                Green.enabled = false;
                Blue.enabled = false;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Hit_HP) {
            switch (hpManager.hp) {
                case 1:
                    anim.Play("HP3_DeadAnim");
                    audioSource.PlayOneShot(HPDeadSound);
                    Hit_HP = false;
                    break;
                case 2:
                    anim.Play("HP2_DeadAnim");
                    audioSource.PlayOneShot(HPDeadSound);
                    Hit_HP = false;
                    break;
                case 3:
                    anim.Play("HP1_DeadAnim");
                    audioSource.PlayOneShot(HPDeadSound);
                    Hit_HP = false;
                    break;
                default:
                    Hit_HP = false;
                    break;
            }
        }
        if (Heal_HP) {
            switch (hpManager.hp) {
                case 1:
                    anim.Play("HP2_HealAnim");
                    Debug.Log("HP2_HealAnim");
                    audioSource.PlayOneShot(HPHealSound);
                    Heal_HP = false;
                    break;
                case 2:
                    anim.Play("HP1_HealAnim");
                    audioSource.PlayOneShot(HPHealSound);
                    Heal_HP = false;
                    break;
                default:
                    Heal_HP = false;
                    break;
            }
        }
    }

    public void UIUpdate() {
        switch (hpManager.hp) {
            case 1:
                Red.enabled = true;
                Green.enabled = false;
                Blue.enabled = false;
                break;
            case 2:
                Red.enabled = true;
                Green.enabled = true;
                Blue.enabled = false;
                break;
            case 3:
                Red.enabled = true;
                Green.enabled = true;
                Blue.enabled = true;
                break;
            default:
                Red.enabled = false;
                Green.enabled = false;
                Blue.enabled = false;
                break;
        }   
    }
}
