using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Boss : MonoBehaviour
{
    public Slider BossHPSlider;
    public Text BossHPName;
    public GameObject Folder;

    Test_Boss BossScript;

    // Start is called before the first frame update
    void Start()
    {
        GameObject Boss = GameObject.Find("Demo_Boss");
        if(Boss != null) {
            BossScript = Boss.GetComponent<Test_Boss>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (BossScript.BossTime) {
        //    Folder.SetActive(true);
        //    BossHPSlider.maxValue = BossScript.MaxHP;
        //    BossHPName.text = (BossScript.BossName);

        //    BossHPSlider.value = BossScript.HP;
        //} else {
        //    Folder.SetActive(false);
        //}

        if(BossScript == null) {
            Folder.SetActive(false);
        }
    }
}
