using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map2 : MonoBehaviour
{
    public MoveElevetor Thangmay;
    public ProccessBotMap2 BotMap;
    public RandomMovement RandomMovement;
    public OpenDoor[] openDoors;
    public DungSach[] TuSachs;
    public GameObject[] SachData;
    public GameObject[] Sachs;
    public ProccessItem ProccessItem;
    public Buff_low buff_Low;
    public RaoChanController RaoChanController;
    public Transform TargetDomDom;
    public Transform TargetRaoChan;
    public GameObject ViTris;
    public Transform GiaSach;
    public ViTri[] ViTri1;
    public ViTri[] ViTri2;
    public ViTri[] ViTri3;
    public Btn_OpenDoor[] btn_MovePillars;
    public GameObject VongBot;
    public GameObject CollierPlayer;
    public Transform DauBot2;

    public void SetWin()
    {
        RandomMovement.StopAgent();
        openDoors[1].SetAniDoor(1);
        RandomMovement.StopAudio();
        VongBot.SetActive(false);
    }
    public void SetTuSach(int[] id)
    {

        for (int i = 0; i < id.Length; i++)
        {
            if (id[i] == 0)
            {
                continue;
            }
            TuSachs[i].SetWin();
            SachData[i].SetActive(false);
        }

    }
    public int GetSach()
    {
        return Array.FindAll(SachData, (x) => x.activeSelf).Length;
    }
    public void Get1Book(int[] id)
    {
        for (int i = 0; i < id.Length; i++)
        {
            if (i == EventUi.instance.idSach)
            {
                continue;
            }
            else if (id[i] == 0)
            {
                SetSach(i);
                break;
            }

        }

        SetSach(EventUi.instance.GetSachByAds());
    }
    public void GetX2Book()
    {
        int index = 0;
        for (int i = 0; i < SachData.Length; i++)
        {
            if (SachData[i].activeSelf && index < 2)
            {
                index++;
                SetSach(i);
            }

        }

    }
    public IEnumerator ProccessDomDom()
    {
        yield return new WaitForSeconds(1);
        buff_Low.SetStart();
        CollierPlayer.SetActive(true);
    }
    public void SetSach(int id)
    {

        TuSachs[id].SetWin();
        SachData[id].SetActive(false);
        GameController.ins.SetSach(id);
    }
    public void AutoSortX2()
    {
        GameObject g;
        GameController.ins.SetAutoSortX2(out g);

        int id = Int32.Parse(g.name.Substring(g.name.IndexOf('h') + 1, 1).ToString());
        GetBook(id);
        GetBook(EventUi.instance.idSach);
        EventUi.instance.ProccessGetBook();
    }
    private void GetBook(int id)
    {
        switch (id)
        {
            case 1:
                for (int i = 0; i < ViTri1.Length; i++)
                {
                    if (ViTri1[i].IsNull)
                    {
                        GameObject book = Instantiate(Sachs[0], ViTri1[i].transform);
                        book.transform.localPosition = new Vector3(0, 0, 0);
                        book.transform.parent = MapController.Ins.Map2.GiaSach;
                        ViTri1[i].IsNull = false;
                        GameController.ins.SetVitri(-1, GameController.ins.Book[0]);
                        book.GetComponent<ProccessBook>().SetTrue();
                        break;
                    }
                }
                break;
            case 2:
                for (int i = 0; i < ViTri2.Length; i++)
                {
                    if (ViTri2[i].IsNull)
                    {
                        GameObject book = Instantiate(Sachs[1], ViTri2[i].transform);
                        book.transform.localPosition = new Vector3(0, 0, 0);
                        book.transform.parent = MapController.Ins.Map2.GiaSach;
                        ViTri2[i].IsNull = false;
                        GameController.ins.SetVitri(-1, GameController.ins.Book[0]);
                        book.GetComponent<ProccessBook>().SetTrue();
                        break;
                    }
                }
                break;
            case 3:
                for (int i = 0; i < ViTri3.Length; i++)
                {
                    if (ViTri3[i].IsNull)
                    {
                        GameObject book = Instantiate(Sachs[2], ViTri3[i].transform);
                        book.transform.localPosition = new Vector3(0, 0, 0);
                        book.transform.parent = MapController.Ins.Map2.GiaSach;
                        ViTri3[i].IsNull = false;
                        GameController.ins.SetVitri(-1, GameController.ins.Book[0]);
                        book.GetComponent<ProccessBook>().SetTrue();
                        break;
                    }
                }
                break;
        }
    }
    public void MovePillars()
    {
        switch (GameController.ins.IdDieuKhien)
        {
            case 0:
                btn_MovePillars[1].OpenTru();
                btn_MovePillars[5].OpenTru();
                break;
            case 1:
                btn_MovePillars[2].OpenTru();
                /*btn_MovePillars[3].OpenTru();*/
                break;
            case 2:
                btn_MovePillars[3].OpenTru();
               /* btn_MovePillars[4].OpenTru();*/
                break;
            case 3:
                /*btn_MovePillars[10].OpenTru();*/
                btn_MovePillars[4].OpenTru();
                break;
            case 5:
                btn_MovePillars[6].OpenTru();
                /*btn_MovePillars[7].OpenTru();*/
                break;
            case 6:
                btn_MovePillars[7].OpenTru();
                /*btn_MovePillars[8].OpenTru();*/
                break;
            case 7:
                /*btn_MovePillars[9].OpenTru();*/
                btn_MovePillars[8].OpenTru();
                break;
            case 8:
                btn_MovePillars[9].OpenTru();
                /*btn_MovePillars[10].OpenTru();*/
                break;
        }
    }
}
