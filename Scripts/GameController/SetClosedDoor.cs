using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetClosedDoor : MonoBehaviour
{
    public OpenDoor OpenDoor;
    public bool checkUnlockLight = true;
    public bool CheckBot = true;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ConstName.Sheriff) && CheckBot)
        {

            OpenDoor.SetAniDoor(2);
            if (checkUnlockLight)
            {
                OpenDoor.Btn_OpenDoor.SetLock();

            }
            gameObject.SetActive(false);
        }
        else if (!CheckBot && other.CompareTag(ConstName.Player_Tag))
        {
            OpenDoor.SetAniDoor(2);
            if (checkUnlockLight)
            {
                OpenDoor.Btn_OpenDoor.SetLock();

            }
            gameObject.SetActive(false);
        }
    }

}
