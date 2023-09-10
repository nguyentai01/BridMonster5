using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProccessTru : MonoBehaviour
{
    public int id = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ConstName.Player_Tag))
        {
            GameController.ins.SetUpPlayer(true,id);
        }
    }
}
