
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMovePlayerMap4 : MonoBehaviour
{
    public bool IsMove = true;
    private void OnTriggerEnter(Collider other)
    {
        if (IsMove)
        {
            return;
        }
        if(other.CompareTag(ConstName.Player_Tag))
        {
            IsMove = true;
            GameController.ins.playerMovement.SetVelocity();
        }
    }
    
}
