/*using System;*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProccessBotMap2 : MonoBehaviour
{
    public RandomMovement RandomMovement;
    public bool checkPlayerMove = false;
    public bool CheckMove1 = true;
    public bool isWin = false;
    public Animator animator;
    private void OnTriggerStay(Collider other)
    {
        if (!checkPlayerMove || !CheckMove1 || isWin)
        {
            return;
        }
        if (other.CompareTag(ConstName.Player_Tag))
        {
            RandomMovement.Move1(other.gameObject.transform.position);
        }
    }
    public void SetAni(int status)
    {
        if(status == 0)
        {
            status = RanDomAni();
        }
        animator.SetInteger(ConstName.Status,status);
    }

    private int RanDomAni()
    {
        return Random.Range(1, 3)==1?0:4;
    }
 
}
