using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotChilldren : MonoBehaviour
{
    public ProccessBotMap2 BotMap2;
    public RandomMovement RandomMovement;
    public bool isFinish = false;
    public PlayerMovement PlayerMovement;
    
   
    private void OnTriggerStay(Collider other)
    {

        if (other.CompareTag(ConstName.Player_Tag) && !isFinish)
        {
            //attack
            if (!BotMap2.checkPlayerMove&& !PlayerMovement.CheckMovePlayer2)
            {
                return;
            }
            RandomMovement.StopAgent();
            BotMap2.checkPlayerMove = false;
            BotMap2.CheckMove1 = false;
            RandomMovement.SetAttackPlayer(other.transform);
            StartCoroutine(GameController.ins.ProccessPlayer.RotationCamera(transform, 0, false));

        }
    }
}
