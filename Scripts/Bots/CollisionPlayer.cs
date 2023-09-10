using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionPlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(ConstName.Player_Tag))
        {
            StartCoroutine(GameController.ins.playerMovement.SetCoillisionPlayer(0, 2));
            StartCoroutine(GameController.ins.playerMovement.SetCoillisionPlayer(1, 0));

        }
    }
}
