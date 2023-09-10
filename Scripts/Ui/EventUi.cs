using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventUi : MonoBehaviour
{
    public Items item;
    public int idItem = -1;
    public bool FirstPin = false;
    public bool FirstDroneClick = false;
    public bool FadeCamera = false;

    public string CheckEvent = "";
    private Dictionary<int, System.Action> actions;


    public int idPin;

    public int IdSavePoint;
    public int CountHp;
    public string nameBot;


    public DroneMovement drone;
    public Transform DronePl;
    public CameraFilterPack_Oculus_NightVision2 camFiter;
    public CameraFilterPack_TV_80 camFiter2;

    public GameObject player;

    public KeyUi[] KeyUis;

    public SetCamControl setCamControlDrone;
    public Transform[] Pos;
    public PlayerMovement PlayerMovement;
    public ProccessRayCast ProccessRayCast;
    public bool CheckFirstTele = false;
    public bool CheckFirstDrone = false;
    public Btn_OpenDoor openDoor;
    public int AddressPlayer = 0;
    public GameObject TargetFire;

    public BulletFire[] BulletFires;
    public GameObject[] ParentBullets;

    public RayCastGun RayCastGun;
    public Gun gunSet;

    public int IdBullet = 0;
    private bool checkFirstCong = false;
    private bool checkFire = false;
    private int checkFirstGet = 0;
    private bool checkDelay = false;
    private bool CheckShow = true;

    public int idSach = -1;
    public int idViTri = -1;
    private string name;
    private ProccessBook ProccessBook;
    private IEnumerator SetTime;
    private int rate = 0;
    public static EventUi instance { get; private set; }
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        actions = new Dictionary<int, System.Action>();

        actions[1] = GetController;
        actions[2] = GetPin;
        actions[3] = OpenDoorDrone;
        actions[5] = Gun /*1*/; actions[4] = Cong8 /*0*/; actions[6] = HoSo /*4*/; actions[7] = HuyHieu /*3*/; actions[8] = Dan /*2*/;
        actions[0] = GetCard;
        actions[9] = OpenDoor;
        actions[10] = CardOpenTu;
        actions[11] = GetSach;
        actions[12] = SetSach;
        actions[13] = LaySach;
        actions[14] = DatSach;
        actions[15] = TruMove;
        actions[16] = SetNumber;
        actions[17] = ControllerMap3;
        actions[18] = MiniGame4;
        actions[19] = OpenDoorDrone2;
        actions[20] = GetManhRua;
        actions[21] = SetMayDap;
        actions[22] = SetCanGat;
        actions[23] = SettTronVuong;
        actions[24] = GettTronVuong;
        idPin = -1;
        IdSavePoint = -1;
        if (Pref.GetPointLoadChapter() != 0)
        {
            checkFirstGet = 1;
            Pref.SetFirstGet(1); return;
        }
        checkFirstGet = Pref.GetFirstGet();

    }
    private void GetController()
    {

        UiManager.ins.dieuKhien.SetActive(true);
        GameController.ins.CheckDieuKhien = true;
        GameController.ins.CheckGetDrone();
        item.gameObject.SetActive(false);
        AudioManagers.Ins.SetSfx(AudioManagers.Ins.PickItem, 1);
        UiManager.ins.SetPopUp(item.IdPopUp, ConstName.CheckWatchAdsPopUp[(int)item.EnumGetGift]);
        FireBaseSet.SetItem(ConstName.ControllerItem);
    }
    private void GetPin()
    {
        int idPin = Int32.Parse(item.gameObject.name[item.gameObject.name.Length - 1].ToString());
        GameController.ins.SetPin(idPin);
        item.gameObject.SetActive(false);
        if (!FirstPin)
        {
            UiManager.ins.SetPopUp(0, ConstName.CheckWatchAdsPopUp[1]);
            FirstPin = true;
            FireBaseSet.SetItem(ConstName.Pinitems + "_1");
        }
        else
        {
            UiManager.ins.SetPopUp(1, ConstName.CheckWatchAdsPopUp[0]);
            FireBaseSet.SetItem(ConstName.Pinitems + "_2");

        }
        AudioManagers.Ins.SetSfx(AudioManagers.Ins.PickItem, 1);
    }
    private void OpenDoorDrone()
    {

        Btn_OpenDoor btn = item.gameObject.GetComponent<Btn_OpenDoor>();
        GameController.ins.SetDroneMove(btn.target);
        switch (btn.IdDoor)
        {
            case 1:
                GameController.ins.OpenDoorDrone1();
                MapController.Ins.Map1.SetOpenDoor1();
                UiManager.ins.DisPlayTutorial(false);
                break;

            case 10:
                StartCoroutine(GameController.ins.ProccessPlayer.RotationCamera(MapController.Ins.Map3.SetFinishMap3(), 0, true));

                break;
        }


    }
    private void OpenDoorDrone2()
    {

        OpenCot cot = item.gameObject.GetComponent<OpenCot>();
        GameController.ins.SetDroneMove(cot.target);

    }
    private void OpenDoor()
    {

        if (!openDoor.Open)
        {
            openDoor.OpenDoor.Fn_OpenDoor();


        }
    }
    public IEnumerator startPopUp(int id, string gift, float time)
    {
        yield return new WaitForSeconds(time);
        UiManager.ins.SetPopUp(id, gift);
    }
    private void GetCard()
    {
        GameController.ins.SetCard((int)item.IdCard);
        UiManager.ins.DisPlayerData();
        item.gameObject.SetActive(false);
        KeyUis[0].gameObject.SetActive(true);
        AudioManagers.Ins.SetSfx(AudioManagers.Ins.PickItem, 1);
        Pref.SetData(6, 1);
        int idPop = item.IdPopUp;
        if (idPop != -1)
        {
            UiManager.ins.SetPopUp(idPop, ConstName.CheckWatchAdsPopUp[0]);
        }

        FireBaseSet.SetItem(ConstName.CardItem);
    }
    private void CardOpenTu()
    {
        GameController.ins.cardTu++;
        UiManager.ins.DisPlayerData();
        item.gameObject.SetActive(false);
        SetKey(0);
        AudioManagers.Ins.SetSfx(AudioManagers.Ins.PickItem, 1);
        Pref.SetData(5, 1);
        UiManager.ins.SetPopUp(5, ConstName.CheckWatchAdsPopUp[0]);
    }
    private void Gun()
    {
        SetKey(2);
        GameController.ins.SetData(1);
        item.gameObject.SetActive(false);
        AudioManagers.Ins.SetSfx(AudioManagers.Ins.PickItem, 1);
        Pref.SetData(1, 1);
        UiManager.ins.SetPopUp(item.IdPopUp, ConstName.CheckWatchAdsPopUp[0]);
        FireBaseSet.SetItem(ConstName.GunItem);

    }
    private void Cong8()
    {
        SetKey(1);
        GameController.ins.SetData(0);
        item.gameObject.SetActive(false);
        AudioManagers.Ins.SetSfx(AudioManagers.Ins.PickItem, 1);
        Pref.SetData(0, 1);
        if (!checkFirstCong)
        {
            checkFirstCong = true;
            UiManager.ins.SetPopUp(3, ConstName.CheckWatchAdsPopUp[2]);
        }
        FireBaseSet.SetItem(ConstName.CongItem);
    }
    private void HoSo()
    {
        SetKey(5);
        GameController.ins.SetData(4);
        item.gameObject.SetActive(false);
        AudioManagers.Ins.SetSfx(AudioManagers.Ins.PickItem, 1);
        Pref.SetData(4, 1);

        FireBaseSet.SetItem(ConstName.HoSoItem);

    }
    private void HuyHieu()
    {
        SetKey(4);
        GameController.ins.SetData(3);
        item.gameObject.SetActive(false);
        AudioManagers.Ins.SetSfx(AudioManagers.Ins.PickItem, 1);
        Pref.SetData(3, 1);
        UiManager.ins.SetPopUp(item.IdPopUp, ConstName.CheckWatchAdsPopUp[0]);
        MapController.Ins.Map1.arrowHuyHieu.SetActive(false);
        FireBaseSet.SetItem(ConstName.HuyHieuItem);
    }
    private void Dan()
    {
        SetKey(3);
        GameController.ins.SetData(2);
        item.gameObject.SetActive(false);
        AudioManagers.Ins.SetSfx(AudioManagers.Ins.PickItem, 1);
        Pref.SetData(2, 1);
        if (Pref.GetData(2) <= 3)
        {
            UiManager.ins.SetPopUp(4, ConstName.CheckWatchAdsPopUp[3]);
        }
        if (Pref.GetData(2) <= 5)
        {
            FireBaseSet.SetItem(Pref.GetData(2));

        }

    }
    private void GetSach()
    {
        if (idSach == -1)
        {
            AudioManagers.Ins.SetSfx(AudioManagers.Ins.PickItem, 1);
            idSach = Int32.Parse(item.gameObject.name[item.gameObject.name.Length - 1].ToString());
            item.gameObject.SetActive(false);
            UiManager.ins.SetSach(true, idSach);
            if (MapController.Ins.Map2.GetSach() >= 3)
            {
                UiManager.ins.SetPopUp(17, ConstName.CheckWatchAdsPopUp[9]);
            }
        }
    }
    public int GetSachByAds()
    {
        UiManager.ins.SetSach(false, idSach);
        AudioManagers.Ins.SetSfx(AudioManagers.Ins.CurrentBook, 1);
        int id = idSach;
        idSach = -1;
        return id;
    }
    private void SetSach()
    {

        DungSach ds = item.GetComponent<DungSach>();

        if (idSach != -1)
        {
            if (ds.id == idSach)
            {
                UiManager.ins.SetSach(false, idSach);
                AudioManagers.Ins.SetSfx(AudioManagers.Ins.CurrentBook, 1);
                GameController.ins.SetSach(idSach);

                idSach = -1;
                ds.SetWin();
                if (MapController.Ins.Map2.GetSach() >= 2)
                {
                    UiManager.ins.SetPopUp(18, ConstName.CheckWatchAdsPopUp[10]);
                }
            }
            else
            {
                AudioManagers.Ins.SetSfx(AudioManagers.Ins.WrongBook, 1);
                MapController.Ins.Map2.RandomMovement.Move1(item.transform.position);
            }
        }
    }
    private void LaySach()
    {
        if (idSach == -1)
        {
            MapController.Ins.Map2.ViTris.SetActive(true);
            AudioManagers.Ins.SetSfx(AudioManagers.Ins.PickItem, 1);
            ProccessBook = item.GetComponent<ProccessBook>();
            ProccessBook.viTri.IsNull = true;
            GameController.ins.ProccessListBook(false, ProccessBook.gameObject);
            name = item.gameObject.name;
            idSach = Int32.Parse(name.Substring(name.IndexOf('h') + 1, 1).ToString());
            item.gameObject.SetActive(false);
            item = null;
            UiManager.ins.SetSach(true, idSach + 5);
            if (GameController.ins.GetSaiViTri() > 4)
            {
                UiManager.ins.SetPopUp(19, ConstName.CheckWatchAdsPopUp[11]);
            }
        }
    }
    private int CheckSachTrue(ProccessBook pk)
    {
        if (ProccessBook.isTrue)
        {
            if (idSach != idViTri)
            {

                pk.isTrue = false;

                return 1;
            }
            else
            {
                pk.SetTrue();
                return 0;
            }
        }
        else if (!ProccessBook.isTrue)
        {
            if (idSach == idViTri)
            {
                pk.SetTrue();
                return -1;

            }
            else
            {
                pk.isTrue = false;
                return 0;
            }
        }

        return 0;
    }
    private void DatSach()
    {
        if (idSach != -1)
        {
            MapController.Ins.Map2.ViTris.SetActive(false);
            name = item.gameObject.name;
            idViTri = Int32.Parse(name.Substring(name.IndexOf('X') + 1, 1).ToString());
            GameObject g = Instantiate(MapController.Ins.Map2.Sachs[idSach - 1], item.transform);
            g.transform.localPosition = new Vector3(0, 0, 0);
            g.transform.parent = MapController.Ins.Map2.GiaSach;
            ProccessBook pk = g.GetComponent<ProccessBook>();
            pk.viTri = item.GetComponent<ViTri>();
            pk.viTri.IsNull = false;
            GameController.ins.SetVitri(CheckSachTrue(pk), g);
            ProccessBook = null;
            UiManager.ins.SetSach(false, idSach + 5);
            idSach = -1;
            idViTri = -1;
            item = null;
        }

    }
    public void ProccessGetBook()
    {
        UiManager.ins.SetSach(false, idSach + 5);
        idSach = -1;
        idViTri = -1;
        item = null;
    }
    public void SetKey(int id)
    {
        KeyUis[id].gameObject.SetActive(true);
    }
    private void TruMove()
    {
        Btn_OpenDoor btn = item.gameObject.GetComponent<Btn_OpenDoor>();
        GameController.ins.SetDroneMove(btn.target);
        UiManager.ins.DisPlayTutorial(false);
    }
    private void SetNumber()
    {
        MapController.Ins.Map3.Elevator.SetMove((int)item.Number);
    }
    private void ControllerMap3()
    {
        GameController.ins.SetMiniGame3(true);
        MapController.Ins.Map3.MiniGame3.SetMove(true);

        StartCoroutine(UiManager.ins.SetUiMiniGame3(0.5f, true));
        
    }
    private void MiniGame4()
    {
        GameController.ins.SetMiniGame4(true);
        StartCoroutine(UiManager.ins.SetUiMiniGame4(0, true));
        StartCoroutine(MapController.Ins.Map3.MiniGame4.SetStart());
        UiManager.ins.SetMinigame3();
        /*item.gameObject.tag = ConstName.None;*/
    }
    private void GetManhRua()
    {

        if (ProccessMap3.Instance.SetData((int)item.ManhRua))
        {
            
            item.gameObject.SetActive(false);
            UiManager.ins.GetManhRuaMiss();
            AudioManagers.Ins.SetSfx(AudioManagers.Ins.PickItem, 1);
        }
;
    }
    private void SetMayDap()
    {
        if (ProccessMap3.Instance.GetRua())
        {

            item.GetComponent<MayDap>().SetDap(ProccessMap3.Instance.id);
            UiManager.ins.GetManhRua();
            item.gameObject.tag = ConstName.None;
        }
    }
    private void SetCanGat()
    {
        item.GetComponent<CanGat>().SetMayDap();

        item.gameObject.tag = ConstName.None;
    }
    private void SettTronVuong()
    {
        ProccessMap3.Instance.SetTronVuong((int)item.TronVuong);
        Pref.SetDataTronVuong((int)item.TronVuong + "");
        item.gameObject.SetActive(false);
        AudioManagers.Ins.SetSfx(AudioManagers.Ins.PickItem, 1);
    }
    private void GettTronVuong()
    {
        if (ProccessMap3.Instance.GetTronVuong((int)item.TronVuong))
        {
            MapController.Ins.Map3.tuLanh.SetTrue((int)item.TronVuong);
            item.gameObject.tag = ConstName.None;
            UiManager.ins.SetTronVuong();
            AudioManagers.Ins.SetSfx(AudioManagers.Ins.WinMap, 1);
        }

    }
    public void ClickInteract()
    {
        if (actions.ContainsKey(idItem))
        {
            if (checkFirstGet == 0)
            {
                StartCoroutine(UiManager.ins.Settutorial(false, 1, 0));
                Pref.SetFirstGet(1);
                checkFirstGet = 1;
            }
            actions[idItem].Invoke();
            UiManager.ins.Hide_BtnInteract();
            idItem = -1;
        }
    }
    public void ClickMoveDrone()
    {
        AudioManagers.Ins.SetSfx(AudioManagers.Ins.ButtonClick, 1);
        actions[(int)item.IdItem].Invoke();
        UiManager.ins.DisPlayBtn_DroneMove(false);
        idItem = -1;
    }
    public void X2_Speed()
    {
        AudioManagers.Ins.SetSfx(AudioManagers.Ins.ButtonClick, 1);
#if UNITY_EDITOR
        UiManager.ins.Speed(true);
        PlayerMovement.speed *= 2;
        StartCoroutine(ResetSpeed());
        ShowRate.ins.SetReward();

        /* StopTimeFire();*/
#endif


        ManagerAds.ins.ShowRewarded((x) =>
        {
            if (x)
            {

                UiManager.ins.Speed(true);
                PlayerMovement.speed *= 2;
                StartCoroutine(ResetSpeed());
                ShowRate.ins.SetReward();

                FireBaseSet.SetRivive(ConstName.Check_SpeedUp_);
            }
        });
    }
    public void Get_X2_Speed()
    {
        UiManager.ins.Speed(true);
        PlayerMovement.speed *= 2;
        StartCoroutine(ResetSpeed());
    }
    private IEnumerator ResetSpeed()
    {
        yield return new WaitForSeconds(15);
        UiManager.ins.Speed(false);

        PlayerMovement.speed /= 2;
    }
    public void Drone()
    {
        AudioManagers.Ins.SetSfx(AudioManagers.Ins.ButtonClick, 1);


        if (GameController.ins.CheckPlayer)
        {
            SetDrone();
        }
        else
        {
            SetPlayer();
        }
    }
    public void SetDrone()
    {
        setCamControlDrone.moveCamPath = false;

        //drone
        UiManager.ins.Player(false);
        /*player.SetActive(false);*/
        drone.canMove = true;
        GameController.ins.SetDrone();
        StartCoroutine(SetPlayerActive(false,0));

    }
    public void SetPlayer()
    {
        UiManager.ins.Player(true);
        ProccessRayCast.enabled = true;

        GameController.ins.SetPlayer();
        setCamControlDrone.moveCamPath = true;
        StartCoroutine(SetPlayerActive(true,0));
        drone.canMove = false;
        /* player.SetActive(true);*/
    }
    public IEnumerator SetPlayerActive(bool status,float time)
    {
        yield return new WaitForSeconds(time);
        if (status)
        {

            camFiter.enabled = false;
            camFiter2.enabled = false;
        }
        else
        {
            if (FadeCamera)
            {
                camFiter2.enabled = true;
            }
            else
            {
                camFiter.enabled = true;
            }
            UiManager.ins.DisPlayBtn_DroneMove(false);
            ProccessRayCast.enabled = false;
        }
    }

    private IEnumerator PauseGame(bool check)
    {
        yield return new WaitForSeconds(0.6f);

        SetPause(0);

    }
    public void ContinueGame()
    {
        SetPause(1);
    }
    public void Revive()
    {
#if UNITY_EDITOR
        UiManager.ins.SetAcGameOver(false);
        ShowRate.ins.SetReward();
        UiManager.ins.SetTest("REVIVE");
        if (Pref.GetPointSave() == 1)
        {
            GameController.ins.SetRevive1();
            UiManager.ins.SetUiMission(1);
            SaveGame.instance.SetSavePlayer();
        }
        else
        {
            Invoke("SetLoadScene", 0.1f);

            Pref.SetDataKeyCard(GameController.ins.card);
        }
       

#endif
        ManagerAds.ins.ShowRewarded((_Comp) =>
        {
            if (_Comp)
            {
               

                UiManager.ins.SetAcGameOver(false);
                UiManager.ins.SetTest("REVIVE");
                if (Pref.GetPointSave() == 1)
                {

                    GameController.ins.SetRevive1();
                    UiManager.ins.SetUiMission(1);
                    SaveGame.instance.SetSavePlayer();
                    ShowRate.ins.SetReward();
                }
                else
                {
                    Invoke("SetLoadScene", 0.1f);
                    Pref.SetDataKeyCard(GameController.ins.card);
                    ShowRate.ins.SetReward();

                }
                switch (Pref.GetPointSave())
                {
                    case 1:
                        FireBaseSet.SetRivive(ConstName.CheckRivive + "1_1"); break;
                    case 2:
                        FireBaseSet.SetRivive(ConstName.CheckRivive + "1_2"); break;
                    case 3:
                        FireBaseSet.SetRivive(ConstName.CheckRivive + "2_1");
                        break;
                    case 4:
                    case 5:
                        FireBaseSet.SetRivive(ConstName.CheckRivive + "2_2"); break;


                    case 7:
                        FireBaseSet.SetRivive(ConstName.CheckRivive + "3_2"); break;
                    case 8:
                        FireBaseSet.SetRivive(ConstName.CheckRivive + "3_5"); break;
                    case 9:
                        FireBaseSet.SetRivive(ConstName.CheckRivive + "3_4"); break;

                }


            }
        });
    }
    private void SetLoadScene()
    {
        SceneManager.LoadScene(2);
    }
    public void Pause()
    {
        StartCoroutine(PauseGame(false));
        UiManager.ins.SetPauseGame(true);
        AudioManagers.Ins.Music.Pause();
        StartCoroutine(GameController.ins.SetPause(true, 0));

        AudioManagers.Ins.SetSfx(AudioManagers.Ins.ButtonClick, 1);

    }
    public void ClosedPopup()
    {
        SetPause(1);
        if (CheckShow)
        {
            StartCoroutine(CheckShowInter());
        }
        PopUpmanager.Instance.SetPopUp();
        AudioManagers.Ins.SetSfx(AudioManagers.Ins.ButtonClick, 1);
        PopUpmanager.Instance.ClosedPopup();
       UiManager.ins.SetTest("Closed");
    }
    private IEnumerator CheckShowInter()
    {
        ManagerAds.ins.ShowInterstitial();
        CheckShow = false;
        yield return new WaitForSeconds(ManagerAds.ins.timeShowAds);
        CheckShow = true;
    }
    public void Btn_Continue()
    {


        UiManager.ins.SetPauseGame(false);
        SetPause(1);
        AudioManagers.Ins.Music.UnPause();

        StartCoroutine(GameController.ins.SetPause(false, 0));
        AudioManagers.Ins.SetSfx(AudioManagers.Ins.ButtonClick, 1);

    }
    public void SetPause(int status)
    {
        Time.timeScale = status;

    }
    public void Btn_Home()
    {
        /*RunInter.ins.RunInterIn()*/
        ;
        ManagerAds.ins.ShowInterstitial();
        AudioManagers.Ins.SFX.Stop();
        AudioManagers.Ins.SetSfx(AudioManagers.Ins.ButtonClick, 1);
        SetPause(1);
        SceneManager.LoadScene(1);

    }
    public void Btn_Tele()
    {
        AudioManagers.Ins.SetSfx(AudioManagers.Ins.ButtonClick, 1);
        DronePl.position = Pos[AddressPlayer - 1].position;
        if (!CheckFirstTele)
        {
            CheckFirstTele = true;
        }
    }
    public void Btn_Fire()
    {
        int CountBullet = GameController.ins.data[2];
        if (IdBullet >= CountBullet || checkFire)
        {
            return;
        }
        //Show PopUp
        BiaDan bd;

        Vector3 target = RayCastGun.SetRayCast(out bd);
        if (bd != null)
        {
            bd.IsFire = true;
            GameController.ins.SetFire();
        }
        gunSet.SetAni(2);
        TimeLeft.ins.StopTimeFire();
        target = target == Vector3.zero ? TargetFire.transform.position : target;
        BulletFires[IdBullet].FireBullet(target);
        checkFire = true;

        IdBullet++;
        UiManager.ins.SetDataBullet(CountBullet - IdBullet);
        switch (CountBullet - IdBullet)
        {
            case 0:
                // +1 bullet
                if (GameController.ins.FireFire < 3 && !checkDelay)
                {
                    UiManager.ins.SetPopUp(16, ConstName.CheckWatchAdsPopUp[8]);
                }
                break;
            case 1:
                if (GameController.ins.FireFire == 0 && !checkDelay)
                {
                    UiManager.ins.SetPopUp(16, ConstName.CheckWatchAdsPopUp[8]);
                }
                break;
            case 2:

            case 3:
            case 4:
            case 5:
                if (!checkDelay)
                {
                    checkDelay = true;
                    UiManager.ins.SetPopUp(15, ConstName.CheckWatchAdsPopUp[7]);
                    StartCoroutine(DelayPopUp());
                }
                break;
        }
        StartCoroutine(SetBullet());
    }

    private IEnumerator SetBullet()
    {
        yield return new WaitForSeconds(1);
        if (IdBullet < GameController.ins.data[2])
        {//lap dan
            UiManager.ins.SetFireBtn(true, 0);
            StartCoroutine(SetFireBullet());
        }
        else
        {
            //Het dan
            StartCoroutine(SetWinMap());
        }
    }
    private IEnumerator DelayPopUp()
    {
        yield return new WaitForSeconds(8);
        checkDelay = false;
    }
    public void SetX2Shoot()
    {
        IdBullet++;
        UiManager.ins.SetDataBullet(GameController.ins.data[2] - IdBullet);
    }
    private IEnumerator SetFireBullet()
    {
        yield return new WaitForSeconds(0.5f);
        gunSet.SetAni(3);
        StartCoroutine(gunSet.SetStay(2));
        yield return new WaitForSeconds(1f);
        ParentBullets[IdBullet].SetActive(true);
        yield return new WaitForSeconds(2f);
        TimeLeft.ins.StartTimeFire();
        checkFire = false;
    }

    public IEnumerator SetWinMap()
    {
        yield return new WaitForSeconds(1);
        if (GameController.ins.FireFire >= 3)
        {
            //win
            MapController.Ins.ProccessFinishMap1();
            AudioManagers.Ins.SetSfx(AudioManagers.Ins.WinMap, 1);
            UiManager.ins.SetWin1();
        }
        else
        {
            GameController.ins.Proccess3();
            AudioManagers.Ins.SetSfx(AudioManagers.Ins.LosedMap, 1);


        }
    }
    public void SetItem(Transform select)
    {
        Items item = select.GetComponent<Items>();
        idItem = (int)item.IdItem;
        this.item = item;
        if (idItem == 14 && idSach == -1)
        {
            return;
        }
        UiManager.ins.DisPlayBtn_interact();

        if (checkFirstGet == 0)
        {
            StartCoroutine(UiManager.ins.Settutorial(true, 1, 0.2f));
        }
        if (idItem == 9)
        {
            openDoor = select.GetComponent<Btn_OpenDoor>();
        }
    }
    public void ResetEventUi()
    {
        idItem = -1;
        item = null;
        openDoor = null;
    }
    public void SetDroneStart()
    {
        DronePl.position = Pos[AddressPlayer].position;
        CheckFirstDrone = true;
        CheckFirstTele = true;
    }
    public void WatchAdsPopUp()
    {
        AudioManagers.Ins.SetSfx(AudioManagers.Ins.ButtonClick, 1);
#if UNITY_EDITOR
        SetPause(1);
        PopUpmanager.Instance.WatchAds();
        ShowRate.ins.SetReward();
        UiManager.ins.SetTest("POPUP");
#endif
        ManagerAds.ins.ShowRewarded((_Compl) =>
        {
            if (_Compl)
            {
                SetPause(1);

                PopUpmanager.Instance.WatchAds();
                ShowRate.ins.SetReward();
                UiManager.ins.SetTest("POPUP");

            }
        });

    }
    public void Rate(int rate)
    {
        this.rate = rate;
        UiManager.ins.SetRate(rate);
    }
    public void Btn_MaybeLate()
    {
        UiManager.ins.SetPopUpRate(false);
        ShowRate.ins.StartTimeRate(480, false);

    }
    public void Btn_Close()
    {
        Pref.SetRate(1);
        UiManager.ins.SetPopUpRate(false);
        FireBaseSet.SetRateLog(rate);

    }
    public void ExistMiniGame3()
    {
        GameController.ins.SetMiniGame3(false);
        MapController.Ins.Map3.MiniGame3.SetMove(false);
        UiManager.ins.ExistMini3();

    }
    public void ExistMiniGame4()
    {
        GameController.ins.SetMiniGame4(false);
        MapController.Ins.Map3.MiniGame4.StopMiniGame4();
        UiManager.ins.ExistMini4();

    }
}
