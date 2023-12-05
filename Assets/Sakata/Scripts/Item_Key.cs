using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Key : MonoBehaviour
{
    public int KeyNumber;

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            UI_Key KeyScript;
            GameObject obj = GameObject.Find("UI_Key");
            KeyScript = obj.GetComponent<UI_Key>();
            KeyScript.OnKey = KeyNumber;
            Destroy(this.gameObject);
        }
    }
}
