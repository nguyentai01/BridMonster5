using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;

public class Map3 : MonoBehaviour
{
    public ControllerElevator Elevator;
    public ProccessMinigame3_3 MiniGame3;
    public ProccessMap3 MainMap3;

    public ProccessMiniGame4 MiniGame4;
    public GameObject Bullet;
    public WaterHeal WaterHeal;
    public TuLanh tuLanh;
    public GameObject[] ManhRuas;
    public Btn_OpenDoor[] OpenDoors;
    public GameObject[] KeyCards;
    public Transform Target;
    public GameObject ConRua;
    public Rua2Dau rua;
    public Btn_OpenDoor BtnWin;
    public OpenDoor CuaWIn;
    public Transform ManhRua;
    public Transform TayCam;
    public Transform BanDieuKhien;

    public void SetManhRua(int[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == 1)
            {
                ManhRuas[i].SetActive(false);   
            }
        }
    }
    public void SetOpen()
    {

        string[] OpenDoor = Pref.GetDataDoorMap3();
        if (OpenDoor != null)
        {
            if (OpenDoor.Length <= 1)
            {
                return;
            }
            for (int i = 0; i < OpenDoor.Length - 1; i++)
            {
                OpenDoors[Int32.Parse(OpenDoor[i])].SetOpenLoadGame();
                KeyCards[4- Int32.Parse(OpenDoor[i])].SetActive(false);
            }
        }
      
    }
    public void SetKeyCard(int[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if ((array[i] >= 1))
            {
                KeyCards[i].SetActive(false);
            }
        }
    }
    public Transform SetFinishMap3()
    {
        WaterHeal.SetScaleFinish();
        return Target;
    }
   public Transform SetCuaWin()
    {
        CuaWIn.SetAniDoor(1);
        return CuaWIn.transform;
    }
    public int GetCoutManhRua()
    {
        int x = 0;
        for (int i = 0; i < ManhRuas.Length; i++)
        {
            if (!ManhRuas[i].activeSelf)
            {
                x++;
            }
        }
        return x;
    }

    
}
