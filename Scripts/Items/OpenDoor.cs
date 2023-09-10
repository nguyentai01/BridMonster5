using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public Animator animator;
    public Btn_OpenDoor Btn_OpenDoor;
    public bool Card;
    
    public bool DoubleButtonOpen;
    public Btn_OpenDoor[] Btn_OpenDoors;
    public bool log = false;
    public int idPop = -1;
    public EnumGetGift EnumGetGift;

    public GameObject walls;
    public int idCard =0;
    public int idDoor = -1;
    private void Start()
    {
        if (Btn_OpenDoor != null)
        {
            idCard = (int)Btn_OpenDoor.IdCard;

        }
    }
    public void Fn_OpenDoor()
    {
        if (DoubleButtonOpen)
        {
            if (Btn_OpenDoors[0].Open && Btn_OpenDoors[1].Open)
            {
                conditionOpen2();
            }
        }
        else
        {
            conditionOpen();
        }
    }
    public void Fn_CloseDoor()
    {
        animator.SetInteger("status", 2);
    }
    public void Fn_OpenAni()
    {
        if (idPop != -1)
        {
            StartCoroutine(EventUi.instance.startPopUp(idPop, ConstName.CheckWatchAdsPopUp[(int) EnumGetGift], 0));
        }
      
        if (Btn_OpenDoor != null)
        {
            if (Btn_OpenDoor.IdDoor == 6)
            {
                UiManager.ins.SetUiMission(8);
               
            }
            Btn_OpenDoor.gameObject.tag = ConstName.None;
        }
        StartCoroutine(SetAni(1));
    }
    public IEnumerator SetAni(int status)
    {
        yield return new WaitForSeconds(0f);
        SetAniDoor(status);

    }
    public void SetAniDoor(int status)
    {
        animator.SetInteger("status", status);
    }
    private void conditionOpen()
    {

        if (Card && GameController.ins.card[idCard] > 0 )
        {

            if (Btn_OpenDoor != null)
            {
                Btn_OpenDoor.SetUnLock();
            }
            AudioManagers.Ins.SetSfx(AudioManagers.Ins.BtnOpen, 1);
            MapController.Ins.Map1.SetOpenCard();
            GameController.ins.card[idCard]--;
            Pref.SetData(6, -1);
            UiManager.ins.DisPlayerData();

            Fn_OpenAni();
            if (walls != null)
            {
                walls.SetActive(false);
            }
            if (idDoor != -1)
            {
                FireBaseSet.SetFirebase(ConstName.click_button_keycard + idDoor + "");

            }
            SaveDataMap3();
        }
     
        else if (!Card)
        {
            if (Btn_OpenDoor != null)
            {
                Btn_OpenDoor.SetUnLock();
            }
            Fn_OpenAni();
        }
        else
        {
            AudioManagers.Ins.SetSfx(AudioManagers.Ins.Door_error, 1);
        }
        




        /* if (ManagerDataItems.Instance.GetDataItems(1) >= ItemCondition1 && ManagerDataItems.Instance.GetDataItems(2) >= ItemCondition2)
         {
             animator.SetInteger("status", 1);
             ManagerDataItems.Instance.TakeDataItems(ItemCondition1, ItemCondition2);
         }*/

    }
    private void SaveDataMap3()
    {
        switch (idDoor)
        {
            case 8:Pref.SetDataDoorMap3(0+"");break;
            case 9: Pref.SetDataDoorMap3(1 + ""); break;
            case 10: Pref.SetDataDoorMap3(2 + ""); break;
            case 11: Pref.SetDataDoorMap3(3 + ""); break;
            case 12: Pref.SetDataDoorMap3(4 + ""); break;

        }
    }
    private void conditionOpen2()
    {

        Fn_OpenAni();
        Btn_OpenDoors[0].SetUnLock();
        Btn_OpenDoors[1].SetUnLock();

    }
    public void OpenNow()
    {
        SetAniDoor(1);
        Btn_OpenDoor.SetUnLock();
        Btn_OpenDoor.gameObject.tag = ConstName.None;
    }
}
