using Cinemachine;
using DG.Tweening;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController ins { get; private set; }
    public bool CheckDieuKhien = false;
    public bool CheckPlayer = true;
    public bool CheckDrone = false;

    public DroneMovement drone;
    public SetAniDrone AniDrone;

    public PlayerMovement playerMovement;
    public SetCamControl setCam;
    public ProccessSheriff ProccessSheriff;
    public ProccessPlayer ProccessPlayer;
    public CinemachineBrain CinemachineBrain;
    public ProccessRayCast ProccessRayCast;
    public CinemachineVirtualCamera VirtualCamera;
    public AudioSource aus;
    public AudioSource aus2;
    public GameObject[] cameras;
    public List<int> IdTrus = new List<int>();

    public int[] Sach = new int[6];
    public bool IsSach = false;

    public int[] data;//Cong - Sung - Dan - Huy Hieu - HoSo
    public int[] card;
    public int cardTu = 0;
    private bool CheckTime = false;
    public Pin[] pins;
    public bool checkOpenFirst = false;
    public int SetTimeSup = 5;
    private int SetTimeleft;
    public bool checkWin = false;
    public int FireFire = 0;
    public string[] s;
    public int SetSaiViTri = 0;
    public bool IsUp = false;
    public bool IsDown = true;
    public int idTru;
    public int IdDieuKhien = -1;
    public List<GameObject> Book = new List<GameObject>();
    private void Awake()
    {
        ins = this;
    }
    private void Start()
    {
        SetTimeleft = SetTimeSup;
        /*aus2.Pause();*/
        AudioManagers.Ins.SetMusic(AudioManagers.Ins.BackGroudGame, 1);
        pins = MapController.Ins.Map1.pins;
        /* UiManager.ins.SetUiMission(true, ConstName.Mission1 );*/

        SetPlay();
    }
    private void SetPlay()
    {
        switch (Pref.GetPointSave())
        {
            case 0:
                FireBaseSet.SetStartGame(1);
                FireBaseSet.SetPlayGame("1_0"); break;
            case 1:
            case 2:
                FireBaseSet.SetPlayGame("1_1"); break;
            case 3:
                FireBaseSet.SetPlayGame("2_1");
                FireBaseSet.SetStartGame(2); break;
        }
    }
    public void SetCard(int id)
    {
        card[id]++;
    }
    public void SetFire()
    {
        FireFire++;

    }
    public void SetPin(int id)
    {

        pins[id].CheckPin = true;
        CheckGetDrone();
    }
    public bool CheckDataFinish()
    {
        int[] data2;
        data2 = Array.FindAll(data, (x) => x == 0);
 
        if (data2.Length == 0)
        {
            CheckTime = true;
            return true;
        }
        return false;
    }
    public void SetData(int index)
    {
        data[index]++;
        UiManager.ins.SetData(data);


        if (CheckData())
        {
            if ((SetTimeleft == SetTimeSup))
            {

                StartRunTime();
            }
        }
    }

    public void SetDataBullet(int index)
    {
        data[2] += index;
        UiManager.ins.SetData(data);
    }
    public void SetAll(int id, int index)
    {
        data[id] = index;
        UiManager.ins.SetData(data);
    }
    public bool CheckData()
    {
        int[] data2;
        data2 = Array.FindAll(data, (x) => x == 0);
        if (data2.Length == 1)
        {
            CheckTime = true;
            return true;
        }
        if (data2.Length == 0)
        {
            MapController.Ins.Map1.ProccessGame1.SetAudioWarring(true);
            if (data[0] == 3 && data[2] == 5)
            {
                SetFinish();
            }


        }
        return false;
    }
    public void SetRevive1()
    {
        checkWin = false;

        StartRunTime();
    }
    public void StartRunTime()
    {
        /*ProccessSheriff.gameObject.SetActive(false);*/
        ProccessSheriff.ProccessRevive1();
        StartCoroutine(RunTime());
    }
    private IEnumerator RunTime()
    {
        GameObject g = MapController.Ins.Map1.arrowHuyHieu;
        if (data[3] == 0 && !g.activeSelf)
        {
            g.SetActive(true);
        }
        SetTimeleft = SetTimeSup;
        StartCoroutine(SetTimeRun());
        yield return new WaitForSeconds(SetTimeSup);
        SetFinish();
    }
    private void SetFinish()
    {
        if (!checkWin)
        {
            checkWin = true;
            if (!CheckPlayer)
            {
                EventUi.instance.SetPlayer();
            }
            StartCoroutine(ProccessPlayer.ContinueMovePlayer(false, 0));
            
            ProccessSheriff.Proccess2();
            Proccess2();
            StartCoroutine(ProccessPlayer.SetCam(false, 6));
            UiManager.ins.SetUiMission(false);
            /*  if (!CheckData())
              {
                  StartCoroutine(SetAudioWin1());
              }*/
        }

    }

    private IEnumerator SetTimeRun()
    {
        MapController.Ins.Map1.ProccessGame1.SetAudioWarring(false);
        UiManager.ins.SetActiveTime(true);
        while (SetTimeleft > 0 && !checkWin)
        {
            yield return new WaitForSeconds(1);
            SetTimeleft--;

            UiManager.ins.SetTimeRun(SetTimeleft);

        }
    }
    public void CheckGetDrone()
    {
        if (pins[1].CheckPin && pins[0].CheckPin && CheckDieuKhien)
        {

            CheckDrone = true;
            UiManager.ins.Btn_Drone(true);
            SetAniDrone.instance.setAni();
        }
    }
    public IEnumerator ProcceessGame1()
    {
        StartCoroutine(MapController.Ins.Map1.openDoors[0].SetAni(1));
        /*MapController.Ins.Map1.openDoors[0].Btn_OpenDoor.SetUnLock();*/
        yield return new WaitForSeconds(1);
        ProccessSheriff.StartMove();

    }
    public int GetCard()
    {
        return card.Aggregate((total, next) => total + next);
    }
    public IEnumerator SetMusic(AudioClip ac, float volue, float time)
    {
        yield return new WaitForSeconds(time);
        aus.clip = ac;
        aus.volume = volue;
        aus.Play();
    }
    public IEnumerator SetPause(bool pause, float time)
    {
        yield return new WaitForSeconds(time);
        if (pause)
        {
            aus.Pause();
            yield break;
        }
        aus.UnPause();
    }
    public bool test(out int data)
    {
        data = 1;
        return true;
    }
    public void SetPinAll()
    {
        pins[0].CheckPin = true;
        pins[1].CheckPin = true;
        pins[0].gameObject.SetActive(false);
        pins[1].gameObject.SetActive(false);

        CheckGetDrone();
    }
    public void SetX2Dan()
    {

        SetData(2);
        MapController.Ins.Map1.GetX1Dan();
    }
    public IEnumerator SetShowCam3(float start, float finish, bool callback)
    {
        yield return new WaitForSeconds(1.5f);
        float flex = 0;
        DOTween.To(x => flex = x, start, finish, 0.5f).OnUpdate(() =>
        {
            VirtualCamera.m_Lens.FieldOfView = flex;

        }).OnComplete(() =>
        {
            if (callback)
            {
                StartCoroutine(SetShowCam3(20, 50, false));

            }
            else
            {
                StartCoroutine(UiManager.ins.SetFire(true, 1,0));
            }
        });
    }
    public IEnumerator ProccessCam(int id1, int id2, float time)
    {

        yield return new WaitForSeconds(time);
        SetCam(id1, id2);
    }
    public void SetCam(int id1, int id2)
    {
        cameras[id1].SetActive(false);
        cameras[id2].SetActive(true);
    }
    public IEnumerator BlendCamera(float time, float flex)
    {
        yield return new WaitForSeconds(time);
        CinemachineBrain.m_DefaultBlend.m_Time = flex;
    }
    public void SetOpenDoor1()
    {
        ProccessSheriff.SetOpenDoor1();
        StartCoroutine(ProccessPlayer.ContinueMovePlayer(true, 0));
    }
    public void Proccess3()
    {
        StartCoroutine(ProccessPlayer.RotationCamera(ProccessSheriff.transform, 0, false));
        StartCoroutine(ProccessSheriff.FirePlayer());
    }
    public void Proccess2()
    {
        MapController.Ins.Map1.ProccessGame1.SetAudioWarring(true);
        StartCoroutine(BlendCamera(0, 2));
        StartCoroutine(BlendCamera(8f, 0));
        StartCoroutine(ProccessCam(0, 3, 0));
        StartCoroutine(ProccessCam(3, 0, 6));
        StartCoroutine(playerMovement.SetPausePlayer(false, 1));
        StartCoroutine(playerMovement.SetPausePlayer(true, 12));
        MapController.Ins.Map1.openDoors[1].Fn_OpenAni();
    }
    public void ProccessPlayer2Up()
    {
        StartCoroutine(BlendCamera(1, 0));
        StartCoroutine(ProccessCam(5, 0, 0));
    }
    public void ProccessPlayer2()
    {
        StartCoroutine(BlendCamera(0, 1));
        StartCoroutine(ProccessCam(0, 5, 0));
    }
    public void ProccessPlayer1()
    {
        StartCoroutine(BlendCamera(0, 2));
        StartCoroutine(BlendCamera(6f, 0));

        StartCoroutine(ProccessCam(0, 4, 0));
        StartCoroutine(ProccessCam(4, 0, 4));
    }
    public Transform Proccess1()
    {

        StartCoroutine(SetMusic(AudioManagers.Ins.story3, 1, 0));

        UiManager.ins.SetUiMission(false);
        StartCoroutine(ProccessPlayer.RotationCamera(MapController.Ins.Map1.openDoors[1].transform, 0f, false));
        StartCoroutine(BlendCamera(0, 2));
        StartCoroutine(BlendCamera(4, 1));
        StartCoroutine(BlendCamera(17.2f, 0));

        StartCoroutine(ProccessCam(0, 2, 0));
        StartCoroutine(ProccessCam(2, 6, 4f));
        StartCoroutine(ProccessCam(6, 7, 6.5f));
        StartCoroutine(ProccessCam(7, 8, 9f));
        StartCoroutine(ProccessCam(8, 9, 11.5f));
        StartCoroutine(ProccessCam(9, 10, 13));
        StartCoroutine(ProccessCam(2, 0, 15));

        StartCoroutine(MapController.Ins.Map1.ProccessGame1.ProccessGame(15));
        StartCoroutine(ProccessPlayer.ContinueMovePlayer(true, 26));
        return MapController.Ins.Map1.Tivi.transform;

    }
    public void ProccessMap2_1()
    {
        StartCoroutine(BlendCamera(0, 0.5f));
        StartCoroutine(BlendCamera(6, 0));

        StartCoroutine(ProccessCam(0, 11, 0));
        StartCoroutine(ProccessCam(11, 12, 0.5f));
        StartCoroutine(ProccessCam(12, 11, 5f));
        StartCoroutine(ProccessCam(11, 0, 5.5f));
    }
    public void SetMiniGame3(bool status)
    {

        playerMovement.canMove = !status;
        if (status)
        {

            StartCoroutine(BlendCamera(0, 0.5f));
            StartCoroutine(ProccessCam(0, 14, 0));
        }
        else
        {
            StartCoroutine(BlendCamera(0.5f, 0f));
            StartCoroutine(ProccessCam(14, 0, 0));
        }
    }
    public void SetMiniGame4(bool status)
    {

        /*playerMovement.canMove = !status;*/
        if (status)
        {

            StartCoroutine(BlendCamera(0, 0.5f));
            StartCoroutine(ProccessCam(0, 15, 0));
            StartCoroutine(SetCamTarget(0.6f, 15,new Vector3(-8.198f, 0,0)));

        }
        else
        {
            StartCoroutine(BlendCamera(0.5f, 0f));
            StartCoroutine(ProccessCam(15, 0, 0));
            StartCoroutine(SetCamTarget(0, 0,new Vector3(0,0,0)));
        }
    }
    private IEnumerator SetCamTarget(float time,int id,Vector3 a)
    {
        yield return new WaitForSeconds(time);
        setCam.camTarget = cameras[id].transform;
        setCam.tempCamTarget = a;
    }
    public void SetSach(int id)
    {
        Sach[id]++;
        Pref.SetDataBook(id + "");
        int index = GetSach();
        UiManager.ins.SetBeDaUi(6 - index);
        if (index == 0)
        {
            //win
            StartCoroutine(ProccessPlayer.ContinueMovePlayer(false, 0));
            StartCoroutine(ProccessPlayer.ContinueMovePlayer(true, 4));
            StartCoroutine(BlendCamera(0, 1f));
            StartCoroutine(BlendCamera(4, 0));
            StartCoroutine(ProccessCam(0, 13, 0));
            StartCoroutine(ProccessCam(13, 0, 3f));
            UiManager.ins.SetUiMission(6);
            AudioManagers.Ins.SetSfx(AudioManagers.Ins.WinMap, 1);
            MapController.Ins.Map2.SetWin();
        }

    }
    public int GetSach()
    {
        return Array.FindAll(Sach, (x) => x == 0).Length;
    }
    public void GetDataBook()
    {
        s = Pref.GetDataBook();
        if (s != null)
        {
            if (s.Length <= 1)
            {
                return;
            }
            for (int i = 0; i < s.Length - 1; i++)
            {
                Sach[Int32.Parse(s[i])]++;
            }
            MapController.Ins.Map2.SetTuSach(Sach);
        }
    }
    public void SetVitri(int x, GameObject g)
    {
        if (x < 0)
        {
            AudioManagers.Ins.SetSfx(AudioManagers.Ins.CurrentBook, 1);

        }
        else if (x >= 0)
        {
            ProccessListBook(true, g);
        }


        SetSaiViTri += x;
        UiManager.ins.SetSachSapXepUi(8 - SetSaiViTri);
        if (SetSaiViTri == 0)
        {
            StartCoroutine(ProccessPlayer.RotationCamera(MapController.Ins.Map2.ProccessItem.SetAni(1), 0, true));
            UiManager.ins.SetMission7();
            StartCoroutine(ProccessPlayer.ContinueMovePlayer(true, 1));
        }
    }
    public int GetSaiViTri()
    {
        return SetSaiViTri;
    }
    public void ProccessListBook(bool IsAdd, GameObject g)
    {
        if (IsAdd)
        {
            Book.Add(g);
            return;
        }
        Book.Remove(g);
    }
    public void SetAutoSortX2(out GameObject g)
    {
        g = Book[0];
        Book[0].SetActive(false);
        ProccessListBook(false, Book[0]);
    }
    public void SetDroneMove(Transform target)
    {
        drone.transform.position = playerMovement.transform.position + new Vector3(0, 6, 0);
        drone.transform.DOMove(target.position, 0.5f).SetEase(Ease.InOutBack);
    }
    public void OpenDoorDrone1()
    {
        ProccessPlayer2Up();
        SetOpenDoor1();
    }
    public void SetUpPlayer(bool status, int id)
    {
        IsUp = status;
        IsDown = !status;
        idTru = id;
        if (id == -1 && GetTruDestroy())
        {
            //
            playerMovement.setMove();
            
            /*StartCoroutine(ProccessPlayer.RotationCamera(MapController.Ins.Map2.buff_Low.transform, 0, false));*/
            UiManager.ins.SetUiPause(false);    
            MapController.Ins.Map2.buff_Low.SetStopBuff();
            StartCoroutine(ProccessPlayer.SetGameOverMap2());
        }
    }
    public bool IsAttackPillar()
    {
        return IsUp && idTru >= 0;
    }
    public void SetTruDestroy(int idTru)
    {
        IdTrus.Add(idTru);
    }
    public bool GetTruDestroy()
    {
        return IdTrus.Contains(2) || (IdTrus.Contains(1) && IdTrus.Contains(0));
    }
    public void SetPlayer()
    {
        SetCam(1, 0);
        CheckPlayer = true;
        ProccessPlayer.SetMovePlayer(true);
    }
    public void SetDrone()
    {
        SetCam(0, 1);
        CheckPlayer = false;
        ProccessPlayer.SetMovePlayer(false);
    }
    public void SetCard()
    {
       
            String[] CardKey = Pref.GetDataKeyCard();
            if (CardKey != null)
            {
                if (CardKey.Length <= 1)
                {
                    return;
                }
                for (int i = 0; i < CardKey.Length - 1; i++)
                {
                    card[Int32.Parse(CardKey[i])]++;

                }
            }
        MapController.Ins.Map3.SetKeyCard(card);
        UiManager.ins.DisPlayerData();
    }


    public void SetCamMap3()
    {
        StartCoroutine(ProccessPlayer.ContinueMovePlayer(false, 0));
        StartCoroutine(ProccessPlayer.ContinueMovePlayer(true, 2));

        StartCoroutine(BlendCamera(0, 0.5f));
        StartCoroutine(BlendCamera(2f, 0));

        StartCoroutine(ProccessCam(0, 16, 0));
        StartCoroutine(ProccessCam(16, 0, 1.5f));
    }
    public void SetCamMinigame3()
    {
        StartCoroutine(ProccessPlayer.ContinueMovePlayer(false, 0));
        StartCoroutine(ProccessPlayer.ContinueMovePlayer(true, 2.4f));

        StartCoroutine(BlendCamera(0, 0.3f));
        StartCoroutine(BlendCamera(2.4f, 0));

        StartCoroutine(ProccessCam(0, 17, 0));
        StartCoroutine(ProccessCam(17, 18, 1f));
        StartCoroutine(ProccessCam(18, 0, 2f));
    }
    public void SetCamMinigame1()
    {

        if (CheckPlayer)
        {
            StartCoroutine(ProccessPlayer.ContinueMovePlayer(false, 0));
            StartCoroutine(ProccessPlayer.ContinueMovePlayer(true, 1.3f));
            StartCoroutine(BlendCamera(0, 0.3f));
            StartCoroutine(BlendCamera(2f, 0));
            StartCoroutine(ProccessCam(0, 19, 0));
            StartCoroutine(ProccessCam(19, 0, 1.7f));
        }
        else
        {
            StartCoroutine(EventUi.instance.SetPlayerActive(true, 0));
            StartCoroutine(BlendCamera(0, 0.3f));
            StartCoroutine(BlendCamera(2f, 0));
            StartCoroutine(ProccessCam(1, 19, 0));
            StartCoroutine(ProccessCam(19, 1, 1.7f));
            StartCoroutine(EventUi.instance.SetPlayerActive(false, 2));

        }

    }
}
