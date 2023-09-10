using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager ins { get; private set; }
    public GameObject dieuKhien;
    public GameObject Hander;

    public GameObject Btn_interact;
    public GameObject Btn_DroneMove;
    public GameObject Btn_DroneMove2;

    public GameObject Jump;
    public GameObject[] tutorial1;

    public GameObject UiDroneBtn;
    public GameObject UiDroneTele;
    public GameObject UiInteract;
    public GameObject[] UiMission;
    public GameObject[] UiMissionIcon;
    public Text[] Counts;
    public GameObject MissionTitle;

    public GameObject UiMissionData;

    public GameObject GameOverUi;
    public GameObject GameFinishUi;

    public GameObject Btn_MainMenuOver;


    public GameObject Btn_x2Speed;
    public GameObject SpeedUi;
    public GameObject TutorialStart;
    public GameObject UiConvertMap;
    public GameObject UiPauseGame;
    public GameObject TimeSupUi;

    public Image UiConvertMapImg;

    public FloatingJoystick joystick;

    public GameObject[] GunPlayers;
    public GameObject[] BtnFires;
    public GameObject[] FireBtnReLoadUis;
    public GameObject[] Sachs;
    public GameObject[] ManhRuas;
    public GameObject ViTris;

    public Text CountCard;
    public Text TimeSupTxt;
    public Text[] dataMap1s;
    public Text CheckPop;
    public bool checkPopUp = false;
    public float timePopUp = 15;
    public GameObject[] StarRates;
    public GameObject Btn_MaybeLate;
    public GameObject Btn_Close;
    public GameObject PopUpRate;
    public GameObject BeDaUi;
    public GameObject Btn_ExistMiniGame3;
    public GameObject Btn_ExistMiniGame4;
    public Text BeDatxt;

    public GameObject TutorialMini3;

    public GameObject SachSapXepUi;
    public Text SachSapXepUiTxt;
    public GameObject TronVuongUi;
    public Text TronVuongTxt;

    public float timeCheckRate = 300;
    public int ReWardShow = 0;
    public Text TestGame;
    private int count = 5;
    private int Current = 0;

    AsyncOperation asyncLoad;
    private void Awake()
    {
        ins = this;

    }
    private void Start()
    {
        timePopUp = ManagerAds.ins.timeShowAds;
        if (Pref.GetPointLoadChapter() == 0)
        {
            Pref.SetFirstGet(0);
            SetTutorialStart(true);
        }
        StartCoroutine(StartConverMap(0, 0, 0));
    }

    public void DisPlayBtn_interact()
    {
        Btn_interact.SetActive(true);
    }
    public void Hide_BtnInteract()
    {
        Btn_interact.SetActive(false);
    }
    public void DisPlayBtn_DroneMove(bool status)
    {
        Btn_DroneMove.SetActive(status);
    }



    public void Btn_Drone(bool status)
    {
        UiDroneBtn.SetActive(status);
        UiDroneTele.SetActive(status);
    }
    public IEnumerator SetDrone(float time, bool status)
    {
        yield return new WaitForSeconds(time);
        Btn_Drone(status);
    }
    public void SetBtnDrone(bool status)
    {
        UiDroneTele.SetActive(status);
    }
    public void SetDrone(bool status)
    {
        Btn_interact.SetActive(false);
        Jump.SetActive(status);
    }


    public void Player(bool status)
    {
        SetBtnDrone(status);
        /* dieuKhien.SetActive(status);*/
        Hander.SetActive(status);
        SetDrone(status);
    }


    public void Speed(bool status)
    {
        SetSpeedUi(status);
        Set_Btn_x2Speed(!status);
    }

    public void Set_Btn_x2Speed(bool check)
    {
        Btn_x2Speed.SetActive(check);
    }



    public void SetSpeedUi(bool status)
    {
        SpeedUi.SetActive(status);
    }

    public void SetTutorialStart(bool status)
    {
        /*joystick.background.gameObject.SetActive(true);*/
        TutorialStart.SetActive(status);
    }
    public bool GetTutorial()
    {
        return TutorialStart.activeSelf;
    }
    public IEnumerator StartConverMap(float time, float Color, int index)
    {
        yield return new WaitForSeconds(time);
        UiConvertMap.SetActive(true);
        if (Color == 0)
        {
            UiConvertMapImg.color = new Color(0, 0, 0, 1);
        }
        if (index == 1)
        {
            asyncLoad = SceneManager.LoadSceneAsync(2);
            asyncLoad.allowSceneActivation = false;
        }
        UiConvertMapImg.DOColor(new Color(0, 0, 0, Color), 3).OnComplete(() =>
        {
            if (index == 1)
            {
                UiConvertMap.SetActive(false);


                asyncLoad.allowSceneActivation = true;
                return;
            }
            else if (index == 2)
            {
                UiConvertMap.SetActive(false);
                StartCoroutine(SetFinish(0));
            }
            else
            {
                UiConvertMap.SetActive(false);
            }

        });
    }
    public void DisPlayerData()
    {
        CountCard.text = "" + DisPlayCard();
    }
    public void DisPlayerdataMap1()
    {

    }
    private int DisPlayCard()
    {
        return GameController.ins.GetCard();
    }
    public void DisPlayTutorial(bool status)
    {
        GameController.ins.ProccessRayCast.IsRun = !status;
        EventUi.instance.item = MapController.Ins.Map1.BtnOpenDoors[0].gameObject.GetComponent<Items>();
        Btn_DroneMove2.gameObject.SetActive(status);
        StartCoroutine(Settutorial(status, 0, 0));
    }
    public IEnumerator Settutorial(bool status, int index, float time)
    {
        yield return new WaitForSeconds(time);
        tutorial1[index].SetActive(status);


        StartCoroutine(GameController.ins.ProccessPlayer.ContinueMovePlayer(!status, 0));
    }
    public void SetUiMission(int id)
    {
        MissionTitle.SetActive(true);
        for (int i = 0; i < UiMission.Length; i++)
        {
            UiMission[i].SetActive(i == id);
        }
        switch (id)
        {
            case 4:
                SetIconMission(1);
                break;
            case 5:

                SetBeDaUi(6 - GameController.ins.GetSach());
                SetIconMission(2);
                break;
            case 6:
                SetSachSapXepUi(0);
                SetIconMission(3);
                break;
            case 7:
                SetIconMission(0);
                break;
            case 8:
                SetIconMission(4);
                break;
            case 9:
                /* if (Pref.GetDataRua() != null)
                 {

                     SetCountMission(0, (Pref.GetDataRua().Length - 1) + "/6");
                 }
                 else
                 {
                     SetCountMission(0, "0/6");
                 }*/
                SetIconMission(5);
                break;
            case 10:
                SetIconMission(6);
                break;
            case 11:
                SetIconMission(6);
                break;
            case 12:
                SetIconMission(7);
                break;
        }


    }
    public void SetUiMission(int id1, int id2)
    {
        for (int i = 0; i < UiMission.Length; i++)
        {
            UiMission[i].SetActive(i == id1 || i == id2);
        }
        SetIconMission(5);
    }
    public void SetCountMission(int id, string s)
    {
        Counts[id].text = s;
    }
    public void SetIconMission(int id)
    {
        for (int i = 0; i < UiMissionIcon.Length; i++)
        {
            UiMissionIcon[i].SetActive(i == id);
        }

    }
    public void SetUiMission(bool status)
    {
        SetActiveTime(false);
        SetUiMissionContent(status);
    }
    public void SetUiMissionContent(bool status)
    {
        MissionTitle.SetActive(status);
    }
    public void SetData(int[] data)
    {
        SetUiMissionData(true);
        for (int i = 0; i < data.Length; i++)
        {

            if (i == 0)
            {
                dataMap1s[0].text = data[0].ToString() + "/3";
                continue;
            }
            else if (i == 2)
            {
                dataMap1s[2].text = data[2].ToString() + "/5";
                continue;
            }
            dataMap1s[i].text = data[i].ToString();
            continue;
        }
    }
    public void SetDataBullet(int index)
    {
        dataMap1s[2].text = index + "/5";
    }
    public void SetUiPause(bool status)
    {
        Jump.SetActive(status);
        if (GameController.ins.CheckDrone)
        {
            UiDroneBtn.SetActive(status);
            UiDroneTele.SetActive(status);
        }

    }

    public void SetUiMissionData(bool status)
    {
        UiMissionData.SetActive(status);
    }
    public void SetPauseGame(bool status)
    {
        UiPauseGame.SetActive(status);
    }
    public IEnumerator SetOver(float time)
    {
        AudioManagers.Ins.SetSfx(AudioManagers.Ins.LosedGame, 1);
        yield return new WaitForSeconds(time);
        GameController.ins.ProccessPlayer.gameObject.SetActive(false);
        SetAcGameOver(true);
        yield return new WaitForSeconds(3);
        Btn_MainMenuOver.SetActive(true);
    }
    public void SetAcGameOver(bool status)
    {
        GameOverUi.SetActive(status);
    }
    public IEnumerator SetFinish(float time)
    {
        /*SetRatePopUpStart();*/
        yield return new WaitForSeconds(time);

        GameFinishUi.SetActive(true);
        FireBaseSet.SetFirebase(ConstName.WinGame + "3");
    }
    public void LoadScene(int scene)
    {

        SceneManager.LoadScene(scene);
    }

    public IEnumerator PauseGame()
    {
        yield return new WaitForSeconds(0.3f);
        SetPause(0);
    }
    public void SetPause(int status)
    {
        Time.timeScale = status;

    }

    public void SetPopUp(int idSprite, string giftId)
    {

        HashSet<int> list = new HashSet<int>() { 18, 21 };
#if UNITY_EDITOR

        if (list.Contains(idSprite))
        {
            if (!CheckInternet.instance.checkInternet)
            {
                return;
            }
        }

        PopUpmanager.Instance.SetPopUp(idSprite, giftId);
        StartCoroutine(PauseGame());

#endif

        if (list.Contains(idSprite))
        {
            if (!CheckInternet.instance.checkInternet)
            {
                return;
            }
        }
        PopUpmanager.Instance.SetPopUp(idSprite, giftId);
        StartCoroutine(PauseGame());

    }


    public void SetActiveTime(bool status)
    {
        TimeSupUi.SetActive(status);
        TimeSupTxt.color = Color.white;
    }
    public void SetTimeRun(int time)
    {
        if (time > 9)
        {
            TimeSupTxt.text = "00:" + time;
        }
        else
        {
            if (time <= 5)
            {
                TimeSupTxt.color = Color.red;


            }

            TimeSupTxt.text = "00:0" + time;

        }
    }
    public IEnumerator SetFire(bool status, float time, int id)
    {
        GunPlayers[id].SetActive(status);
        yield return new WaitForSeconds(time);

        BtnFires[id].SetActive(status);

    }
    public void SetFireBtn(bool status, int id)
    {
        FireBtnReLoadUis[id].SetActive(status);
    }
    public void SetWin1()
    {

        StartCoroutine(SetFire(false, 0, 0));
        SetUiPause(true);
    }
    public void SetSach(bool status, int id)
    {
        Debug.Log("id " + id + " " + status);
        Sachs[id].SetActive(status);
    }
    public void SetRate(int id)
    {
        for (int i = 0; i < StarRates.Length; i++)
        {
            StarRates[i].gameObject.SetActive(i < id);
        }
        if (id <= 4)
        {
            Btn_MaybeLate.SetActive(false);
            Btn_Close.SetActive(true);

        }
        else
        {
            /*ShowRate.ins.StartTimeRate(480);*/
            Pref.SetRate(1);
            FireBaseSet.SetRateLog(5);
            SetPopUpRate(false);
            Application.OpenURL("https://play.google.com/store/apps/details?id=com.weup.bird.monster.life.challenge.chapter5");
        }
    }
    public void SetRatePopUpStart()
    {

        if (Pref.GetRate() == 0)
        {
            SetPopUpRate(true);
            Pref.SetShowRate(1);
        }
    }
    public void SetPopUpRate(bool status)
    {

        PopUpRate.SetActive(status);
        StartCoroutine(PauseGame(status ? 0 : 1));
    }
    private IEnumerator PauseGame(int id)
    {
        float time = id == 1 ? 0 : 0.6f;
        yield return new WaitForSeconds(time);
        SetPause(id);
    }
    public void SetBeDaUi(int index)
    {

        BeDaUi.gameObject.SetActive(!(index == 6));
        BeDatxt.text = "" + index + "/6";

    }
    public void SetSachSapXepUi(int index)
    {

        SachSapXepUi.gameObject.SetActive(!(index == 8));
        SachSapXepUiTxt.text = "" + index + "/8";

    }
    public void GetTronVuong()
    {
        Current++;
        TronVuongUi.gameObject.SetActive(!(count == 0));
        TronVuongTxt.text = "" + Current + "/" + count;
    }
    public void SetTronVuong()
    {
        Current -= 2;
        count--;
        GetTronVuong();
    }
    public void SetMission7()
    {
        SetPopUp(20, ConstName.CheckWatchAdsPopUp[0]);
        SetUiMission(7);
    }
    public void SetTest(string s)
    {
        TestGame.text = s;
    }
    public IEnumerator SetUiMiniGame3(float time, bool status)
    {
        SetUiPause(!status);
        yield return new WaitForSeconds(time);
        Btn_ExistMiniGame3.SetActive(status);
        SetOff(!status);
        SetUiMissionContent(!status);
        TutorialMini3.SetActive(status);
    }
    public void ExistMini4()
    {
        StartCoroutine(SetUiMiniGame4(0, false));
        SetUiMissionContent(true);
        SetOff(true);
    }
    public void ExistMini3()
    {
        StartCoroutine(SetUiMiniGame3(0, false));
        SetUiMissionContent(true);
        SetOff(true);
    }

    public IEnumerator SetUiMiniGame4(float time, bool status)
    {

        SetUiPause(!status);
        if (status)
        {
            StartCoroutine(SetFire(true, 1, 1));
        }
        else
        {
            StartCoroutine(SetFire(false, 0, 1));
        }
        yield return new WaitForSeconds(time);
        Btn_ExistMiniGame4.SetActive(status);

    }
    public void GetManhRua(int index, bool status)
    {
        ManhRuas[index].SetActive(status);
    }
    public void GetManhRua()
    {

        for (int i = 0; i < ManhRuas.Length; i++)
        {
            ManhRuas[i].SetActive(false);
        }
    }
    public void GetManhRuaMiss()
    {
        SetUiMission(10);
        /* SetCountMission(0, MapController.Ins.Map3.GetCoutManhRua() + "/6");*/
        SetPopUp(28, ConstName.CheckWatchAdsPopUp[0]);
    }
    public void SetOff(bool status)
    {
        dieuKhien.SetActive(status);
        SetUiPause(status);
    }
    public void SetMinigame3()
    {
        SetOff(false);
        SetUiMissionContent(false);
    }
}
