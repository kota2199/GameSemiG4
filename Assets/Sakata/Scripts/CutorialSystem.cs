using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutorialSystem : MonoBehaviour {
    public GameObject DemoTree;
    public GameObject DemoStone;
    public GameObject RunningSpeed;
    public GameObject Playerctrl;
    public GameObject ObsManager;
    public GameObject GameManager;

    public GameObject[] CutorialUI;
    public Text ArrowText;

    public Animator EnterTextAnim;

    private bool TreeMove = true;
    private bool StoneMove = false;
    private bool Right = false;
    private bool Left = false;

    private bool ArrowAnimRecast = false;

    public bool CutorialStart;
    private bool Skip = false;
    public int Step = 0;
    public int UIStep = 0;

    AudioSource audioSource;
    public AudioClip ClickSound;


    void Start() {
        audioSource = GetComponent<AudioSource>();
        Invoke("StepStart",1.5f);
        Invoke("SkipOK", 1f);
        CutorialStart = true;
        Playerctrl.GetComponent<PlayerMovement>().isMove = false;
        DemoStone.SetActive(false);
        ObsManager.SetActive(false);
        ObsManager.GetComponent<ObsManager>().isPause = true;
        ObsManager.GetComponent<ObsManager>().Which_Way_Generation = 0;
        StartCoroutine(ArrowAnim());
        Step = 0;
        UIStep = 0;
    }

    
    void Update() {
        Tree();
        Stone();
        if (!CutorialStart) {
            if (DemoTree.GetComponent<HitCheck>().isHit && Step <= 2 || DemoStone.GetComponent<HitCheck>().isHit && Step == 3) {
                CutorialStart = true;
            }
        }
        if (CutorialStart) {
            Cutorial();
        }
        if(Input.GetKeyDown(KeyCode.Space) && Skip) {
            TimeStart();
            GameManager.GetComponent<GameManager>().CutorialFinish();
            audioSource.PlayOneShot(ClickSound);
        }

        if (ArrowAnimRecast) {
            StartCoroutine(ArrowAnim());
            ArrowAnimRecast = false;
        }

        UICheck();
    }

    void Cutorial() {
        switch (Step) {
            case 1:
                Step1();
                break;
            case 2:
                Step2();
                break;
            case 3:
                Step3();
                break;
            case 4:
                Step4();
                break;
            default:
                break;
        }
    }

    void Step1() {
        TimeStop();
        EnterTextAnim.updateMode = AnimatorUpdateMode.UnscaledTime;
        if (Input.GetKeyDown(KeyCode.Return)) {
            UIStep += 1;
            audioSource.PlayOneShot(ClickSound);
        }
        if (UIStep >= 2) {
            CutorialStart = false;
            TimeStart();
            Step = 2;
        }
    }

    void Step2() {
        TimeStop();
        ObsManager.SetActive(true);
        if (Input.GetKeyDown(KeyCode.Return) && UIStep <= 2) {
            UIStep += 1;
            audioSource.PlayOneShot(ClickSound);
        }
        if(UIStep == 3) {
            if (Input.GetKeyDown(KeyCode.D)) {
                Right = true;
                Left = false;
                UIStep += 1;
                audioSource.PlayOneShot(ClickSound);
            }
            if (Input.GetKeyDown(KeyCode.A)) {
                Left = true;
                Right = false;
                UIStep += 1;
                audioSource.PlayOneShot(ClickSound);
            }
        }
        if (UIStep >= 4) {
            CutorialStart = false;
            TimeStart();
            if (Right) {
                Playerctrl.GetComponent<PlayerMovement>().toright = true;
            }
            if (Left) {
                Playerctrl.GetComponent<PlayerMovement>().toleft = true;
            }
            Invoke("StoneStart", 1f);
            Step = 3;
        }
    }

    void Step3() {
        TimeStop();
        TreeMove = false;
        if (Input.GetKeyDown(KeyCode.W)) {
            UIStep += 1;
            audioSource.PlayOneShot(ClickSound);
        }
        if (UIStep >= 5) {
            CutorialStart = false;
            TimeStart();
            Playerctrl.GetComponent<PlayerMovement>().tojump = true;
            Playerctrl.GetComponent<PlayerMovement>().isMove = true;
            StoneMove = true;
            Invoke("StartCommand", 2f);
            UIStep = 5;
            Step = 4;
        }
    }

    void Step4() {
        TimeStop();
        if (Input.GetKeyDown(KeyCode.Return)) {
            UIStep += 1;
            audioSource.PlayOneShot(ClickSound);
        }
        if (UIStep >= 6) {
            CutorialStart = false;
            TimeStart();
            StoneMove = false;
            GameManager.GetComponent<GameManager>().CutorialFinish();
            Step = 5;
        }
    }


    void UICheck() {
        switch (UIStep) {
            case 1:
                CutorialUI[0].SetActive(true);
                CutorialUI[1].SetActive(false);
                CutorialUI[2].SetActive(false);
                CutorialUI[3].SetActive(false);
                CutorialUI[4].SetActive(false);
                break;
            case 2:
                CutorialUI[0].SetActive(false);
                CutorialUI[1].SetActive(true);
                CutorialUI[2].SetActive(false);
                CutorialUI[3].SetActive(false);
                CutorialUI[4].SetActive(false);
                break;
            case 3:
                CutorialUI[0].SetActive(false);
                CutorialUI[1].SetActive(false);
                CutorialUI[2].SetActive(true);
                CutorialUI[3].SetActive(false);
                CutorialUI[4].SetActive(false);
                break;
            case 4:
                CutorialUI[0].SetActive(false);
                CutorialUI[1].SetActive(false);
                CutorialUI[2].SetActive(false);
                CutorialUI[3].SetActive(true);
                CutorialUI[4].SetActive(false);
                break;
            case 5:
                CutorialUI[0].SetActive(false);
                CutorialUI[1].SetActive(false);
                CutorialUI[2].SetActive(false);
                CutorialUI[3].SetActive(false);
                CutorialUI[4].SetActive(true);
                break;
            default:
                CutorialUI[0].SetActive(false);
                CutorialUI[1].SetActive(false);
                CutorialUI[2].SetActive(false);
                CutorialUI[3].SetActive(false);
                CutorialUI[4].SetActive(false);
                break;
        }

        if (!CutorialStart) {
            CutorialUI[0].SetActive(false);
            CutorialUI[1].SetActive(false);
            CutorialUI[2].SetActive(false);
            CutorialUI[3].SetActive(false);
            CutorialUI[4].SetActive(false);
        }
    }


    void StepStart() {
        Step = 1;
        UIStep = 1;
    }
    void StoneStart() {
        DemoStone.SetActive(true);
        if (Right) {
            Playerctrl.GetComponent<PlayerMovement>().toleft = true;
        } else {
            Playerctrl.GetComponent<PlayerMovement>().toright = true;
        }
        StoneMove = true;
    }


    void Tree() {
        Vector3 TreePos = DemoTree.transform.position;
        if (TreeMove) {
            TreePos.z -= 0.015f;
            DemoTree.transform.position = TreePos;
        }
    }
    void Stone() {
        Vector3 StonePos = DemoStone.transform.position;
        if (StoneMove) {
            StonePos.z -= 0.03f;
            DemoStone.transform.position = StonePos;
        }
    }


    void TimeStop() {
        RunningSpeed.GetComponent<Runningspeed>().isPause = true;
        Time.timeScale = 0f;
        TreeMove = false;
        StoneMove = false;
    }
    void TimeStart() {
        RunningSpeed.GetComponent<Runningspeed>().runningspeed = 1f;
        RunningSpeed.GetComponent<Runningspeed>().isPause = false;
        Time.timeScale = 1f;
        TreeMove = true;
    } 

    void StartCommand() {
        CutorialStart = true;
    }
    void SkipOK() {
        Skip = true;
    }

    IEnumerator ArrowAnim() {
        ArrowText.text = "";

        yield return new WaitForSecondsRealtime(1f);

        ArrowText.text = "Å®";

        yield return new WaitForSecondsRealtime(1f);

        ArrowText.text = "Å® Å®";

        yield return new WaitForSecondsRealtime(1f);

        ArrowAnimRecast = true;
    }
}
