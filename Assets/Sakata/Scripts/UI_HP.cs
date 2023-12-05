using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HP : MonoBehaviour
{

    public Image Red;
    public Image Green;
    public Image Blue;

    PlayerHPManager hpManager;

    // Start is called before the first frame update
    void Start()
    {
        GameObject Player = GameObject.FindWithTag("Player");
        hpManager = Player.GetComponent<PlayerHPManager>();
    }

    // Update is called once per frame
    void Update()
    {
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
