using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Key : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {

            UI_Key KeyScript;
            GameObject obj = GameObject.Find("UI_Key");
            KeyScript = obj.GetComponent<UI_Key>();
            KeyScript.OnGet = true;

            Destroy(this.gameObject);
        }
    }
}