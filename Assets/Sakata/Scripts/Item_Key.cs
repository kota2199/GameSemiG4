using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Key : MonoBehaviour
{
    public int KeyNumber;

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            UI_Key UI_Item;
            GameObject obj = GameObject.Find("UI_Item");
            UI_Item = obj.GetComponent<UI_Key>();
            UI_Item.OnKey = KeyNumber;
            UI_Item.AddItemCount();
            Destroy(this.gameObject);
        }
    }
}
