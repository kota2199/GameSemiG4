using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Key : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            UI_Key UI_Item;
            GameObject obj = GameObject.Find("UI_Item");
            UI_Item = obj.GetComponent<UI_Key>();
            UI_Item.OnGet = true;
            Destroy(this.gameObject);
        }
    }
}
