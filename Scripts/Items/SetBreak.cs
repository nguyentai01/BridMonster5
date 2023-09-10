using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBreak : MonoBehaviour
{
    public Rigidbody[] bodys;


    public void SetBreakGame()
    {
        AudioManagers.Ins.SetSfx(AudioManagers.Ins.Kinhvo, 1);
        for(int i=0;i<bodys.Length;i++) {

            bodys[i].isKinematic = false;
        
        }
    }
}
