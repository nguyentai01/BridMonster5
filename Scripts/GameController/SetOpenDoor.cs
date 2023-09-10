using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetOpenDoor : MonoBehaviour
{
    public OpenDoor OpenDoor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ConstName.Sheriff))
        {
            OpenDoor.SetAniDoor(1);
            OpenDoor.Btn_OpenDoor.SetUnLock();
            gameObject.SetActive(false);
        }
    }
}
