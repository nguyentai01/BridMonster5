using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllSheriff : MonoBehaviour
{
    public ProccessSheriff ProccessSheriff;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ConstName.SetToadSter))
        {
            int id = Int32.Parse(other.gameObject.name[other.name.Length - 1].ToString());
            other.gameObject.SetActive(false);
            switch (id)
            {
                case 1:
                    if (!GameController.ins.CheckPlayer)
                        EventUi.instance.SetPlayer();
                    Porccees1();
                    break;
            }
        }
        else if (other.CompareTag(ConstName.Door) && !ProccessSheriff.checkOpenDoor1)
        {
            ProccessSheriff.checkOpenDoor1 = true;
            ProccessSheriff.RotationCameraBot(MapController.Ins.Map1.Proccess1());
            StartCoroutine(GameController.ins.SetMusic(AudioManagers.Ins.story2, 1, 0));
        }
    }
    private void Porccees1()
    {
        
        ProccessSheriff.RotationCameraBot(GameController.ins.Proccess1());
        
     
    }
}
