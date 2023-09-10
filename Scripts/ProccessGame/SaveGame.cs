using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class SaveGame : MonoBehaviour
{
    public static SaveGame instance { get; private set; }
    public int Point;
    public Transform Player;
    public Btn_OpenDoor[] btn_OpenDoors;
    public GameObject[] SetGameObjects;
    public Transform[] PointSavesPlayer;
    public ProccessPlayer player;
    public OpenDoor[] OpenDoors;
    public GameObject DieuKhien;
    public GameObject CardKey;
    public ProccessSheriff sheriff;
    
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        LoadSave();
    }
    public void LoadSave()
    {
        int check = Pref.GetCheckSave();
        Point = check == 0 ? Pref.GetPointLoadChapter() : Pref.GetPointSave();
        if (Point == 2)
        {
            Point = 1;
            Pref.SetPointLoadChapter(Point);
        }
        Pref.SetPointSave(Point);
        /* Point = Pref.check == 0();*/
        Player.position = PointSavesPlayer[Point].position;
        Player.gameObject.SetActive(true);
        switch (Point)
        {
            case 0:
                MapController.Ins.Map1.gameObject.SetActive(true);
                EventUi.instance.AddressPlayer = 0;
                UiManager.ins.SetUiMission(0);
                Instantiate(LoadData.ins.Maps[0], MapController.Ins.transform);
                break;
            case 1:
            case 2:
                MapController.Ins.Map1.gameObject.SetActive(true);
                EventUi.instance.AddressPlayer = 3;
                SetActive(3);
                SetSave1();
                UiManager.ins.SetUiMission(1);
                Instantiate(LoadData.ins.Maps[0], MapController.Ins.transform);
                break;
            case 3:
                UiManager.ins.SetUiMission(5);
                
                MapController.Ins.Map2.gameObject.SetActive(true);
                EventUi.instance.AddressPlayer = 12;
                player.ProccessSavePoint3(MapController.Ins.Map2.Thangmay.GetTransformAndMove(0));
                Instantiate(LoadData.ins.Maps[1], MapController.Ins.transform);
                SetSave2();
                UiManager.ins.SetUiPause(false);
                break;
            case 4:
                
                MapController.Ins.Map2.gameObject.SetActive(true);
                MapController.Ins.Map2.RandomMovement.SetRun(true);

                GameController.ins.GetDataBook();
                EventUi.instance.AddressPlayer = 16;
                Instantiate(LoadData.ins.Maps[1], MapController.Ins.transform);
                SetSave2();
                UiManager.ins.SetUiMission(5);
                break;
            case 5:
                UiManager.ins.SetUiMission(8);
                MapController.Ins.Map2.gameObject.SetActive(true);
                EventUi.instance.AddressPlayer = 19;
                Instantiate(LoadData.ins.Maps[1], MapController.Ins.transform);
                SetSave2();
                SetSave5();
                SetGameObjects[4].SetActive(false);
                break;
            case 6:
                
                SetMap3(21);
                SetSave2();
                UiManager.ins.SetUiMission(9);
                break;
            case 7:
                
                SetMap3(28);
                SetSave2();
                break;
            case 8:
               
                SetMap3(27);
                SetSave2();
                break;
            case 9:
                SetSave2();
                SetMap3(26);
                
                break;
            case 10:
                SetMap3(25);
                SetSave2();
                break;
            case 11:
                

                SetMap3(29);


                SetSave2();
                break;
        }

        if (Point != 0)
        {
            EventUi.instance.SetDroneStart();
            Pref.SetFirstGet(1);
        }


    }
    private void SetMap3(int id )
    {
        MapController.Ins.Map3.gameObject.SetActive(true);
        Instantiate(LoadData.ins.Maps[2], MapController.Ins.transform);
        EventUi.instance.AddressPlayer = id;

        

        if (Point > 6)
        {

            
            GameController.ins.SetCard();
            MapController.Ins.Map3.MainMap3.GetDataRua();
            MapController.Ins.Map3.SetOpen();
        }
        
    }
    public void SetActive(int index)
    {
        for (int i = 0; i <= index; i++)
        {
            if (SetGameObjects[i] == null)
            {
                continue;
            }
            SetGameObjects[i].SetActive(false);

        }
    }
    /*    public void SetActivePointSave(int index)
        {
            for (int i = 0; i <= index; i++)
            {
                if (PointSavesPlayer[i] == null)
                {
                    continue;
                }
                PointSavesPlayer[i].gameObject.SetActive(false);

            }
        }*/
    private void SetSave1()
    {
        GameController.ins.CheckDieuKhien = true;
        GameController.ins.CheckDrone = true;
        /*GameController.ins.pins[0].CheckPin = true;*/
        btn_OpenDoors[0].gameObject.tag = ConstName.None;
        UiManager.ins.Btn_Drone(true);
        SetAniDrone.instance.setAni();
        UiManager.ins.dieuKhien.SetActive(true);
        
        sheriff.ProccessRevive1();
    }
    private void SetSave2()
    {
        GameController.ins.CheckDieuKhien = true;
        GameController.ins.CheckDrone = true;
        UiManager.ins.Btn_Drone(true);
        SetAniDrone.instance.setAni();
        UiManager.ins.dieuKhien.SetActive(true);
    }
   
    public void SetSavePlayer()
    {
        int save = Pref.GetPointSave();
        player.Camera.transform.localPosition = new Vector3(0, 1.43f, 0);
        player.transform.position = new Vector3(PointSavesPlayer[save].position.x, player.transform.position.y, PointSavesPlayer[save].position.z);
        player.gameObject.SetActive(true);
        StartCoroutine(player.ContinueMovePlayer(true, 0));
        MapController.Ins.Map1.openDoors[1].Fn_CloseDoor();
    }
    private void SetSave5()
    {
        List<GameObject> list = new List<GameObject>();
        list = GameController.ins.Book;
        for (int i = 0; i < list.Count; i++)
        {
            list[i].gameObject.SetActive(false);
        }
        MapController.Ins.Map2.openDoors[2].OpenNow();
    }
}
