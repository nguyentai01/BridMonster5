using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMovePlayer : MonoBehaviour
{
    public PlayerMovement PlayerMovement;
    public float time=0.5f;
    public void SetPlayerTrue(bool status)
    {
        if (status)
        {
            PlayerMovement.CheckMovePlayer2 = status;
            StopAllCoroutines();
            return;
        }
        StartCoroutine(CheckPause());

    }

    private IEnumerator CheckPause()
    {
        yield return new WaitForSeconds(time);
        PlayerMovement.CheckMovePlayer2 = false;
    }
}
