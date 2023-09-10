using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerInElevator : MonoBehaviour
{
    public ControllerElevator ControllerElevator;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ConstName.Player_Tag))
        {

            ControllerElevator.IsPlayr = true;


        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(ConstName.Player_Tag))
        {

            ControllerElevator.IsPlayr = false;


        }
    }
}
