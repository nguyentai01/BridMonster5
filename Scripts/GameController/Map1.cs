using SWS;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Map1 : MonoBehaviour
{
    public GameObject Controller;
    public Pin[] pins;
    public OpenDoor[] openDoors;
    public PathManager[] pathToadSters;
    public Btn_OpenDoor[] BtnOpenDoors;
    public GameObject RotationCamera;
    public GameObject Tivi;
    public GameObject[] SetToadSters;
    public ProccessGame1 ProccessGame1;
    public GameObject[] Tutorials;
    public Transform ReviveSheriff;

    public GameObject[] Cong8s;
    public GameObject[] Dans;
    public GameObject HoSo;

    public GameObject arrowHuyHieu;
    public GameObject BiaDan;
    public BiaDan[] BiaDans;
    public MoveElevetor MoveElevetor;
    public Transform LanCanWin;

    public Transform Proccess1()
    {
        BtnOpenDoors[0].SetUnLockButton();
        StartCoroutine(SetOpenDoor());
        SetToadSters[0].SetActive(true);
        return BtnOpenDoors[0].targetlook;
    }/*
    public void */
    private IEnumerator SetOpenDoor()
    {
        yield return new WaitForSeconds(1);
        RotationCamera.SetActive(true);
    }
    public void SetOpenDoor1()
    {
        
        Tutorials[0].SetActive(false);
    }
    public void SetOpenCard()
    {
        Tutorials[1].SetActive(false);
    }
    public void GetAllCong8()
    {
        for (int i = 0; i < Cong8s.Length; i++)
        {
            Cong8s[i].SetActive(false);
        }

    }
    public void GetX1Cong8()
    {

        Cong8s[0].SetActive(false);


    }
    public void GetX1Dan()
    {
        for (int i = 0; i < Dans.Length; i++)
        {
            if (Dans[i].activeSelf)
            {
                Dans[i].SetActive(false);
                break;
            }
        }
    }
    public void GetHoSo()
    {
        HoSo.SetActive(false);
    }
    public BiaDan GetBiaDan()
    {
        for (int i = 0; i < BiaDans.Length; i++)
        {
            if (!BiaDans[i].IsFire)
            {
                return BiaDans[i];
            }
        }
        return null;
    }
}
