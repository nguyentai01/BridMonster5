using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProccessDrone : MonoBehaviour
{
  private bool checkRoomWeapon = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ConstName.DroneMove))
        {
            Items item = other.GetComponent<Items>();
            if ((int)item.IdItem == 3)
            {
                Btn_OpenDoor db = other.gameObject.GetComponent<Btn_OpenDoor>();
                if (!db.Open)
                {
                    int idPop = item.IdPopUp;
                    AudioManagers.Ins.SetSfx(AudioManagers.Ins.BtnOpen, 1);
                    db.SetOpen();
                    int idDoor = db.IdDoor;

                    if (idPop != -1)
                    {
                        if (idPop == 7 && !checkRoomWeapon)
                        {
                            startPopUp(item.IdPopUp, ConstName.CheckWatchAdsPopUp[(int)item.EnumGetGift]);
                            checkRoomWeapon = true;
                        }
                        else if (idPop != 7)
                        {
                            startPopUp(item.IdPopUp, ConstName.CheckWatchAdsPopUp[(int)item.EnumGetGift]);

                        }
                    }
                   if (db.IdDoor==5)
                    {
                        MapController.Ins.Map2.RandomMovement.SetRun(true);
                    }
                    FireBaseSet.SetDroneOpen(idDoor);
                }
            }
            else if ((int)item.IdItem == 15)
            {
                Btn_OpenDoor db = other.gameObject.GetComponent<Btn_OpenDoor>();
                AudioManagers.Ins.SetSfx(AudioManagers.Ins.BtnOpen, 1);
                db.OpenTru();
                GameController.ins.IdDieuKhien = db.idTru;
                HashSet<int> ints = new HashSet<int>() { 0,1,2,3,5,6,7,8};
                if (ints.Contains(db.idTru))
                {
                    UiManager.ins.SetPopUp(21, ConstName.CheckWatchAdsPopUp[12]);
                }
                FireBaseSet.SetDroneOpen(db.IdDoor);
            }

        }
       
        
    }
    private void startPopUp(int id, string gift)
    {
        UiManager.ins.SetPopUp(id, gift);
    }

}
