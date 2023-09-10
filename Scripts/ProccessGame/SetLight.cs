using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLight : MonoBehaviour
{
    public bool IsOn = false;
    public Light lightMain;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ConstName.Player_Tag))
        {
            if (IsOn)
            {
                GameController.ins.ProccessPlayer.light.SetActive(false);
                lightMain.DOIntensity(0.8f, 0.5f);
            }
            else
            {
                GameController.ins.ProccessPlayer.light.SetActive(true);
                lightMain.DOIntensity(0f, 0.5f);
            }
        }
    }
}
