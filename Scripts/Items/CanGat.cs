using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanGat : MonoBehaviour
{
    public MayDap MayDap;
    public Animator Animator;
    public void SetMayDap()
    {
        MayDap.Ep();
        Animator.SetInteger(ConstName.Status, 1);
        UiManager.ins.SetPopUp(27, ConstName.CheckWatchAdsPopUp[0]);
    }
    public void SetAni()
    {
        Animator.SetInteger(ConstName.Status, 1);
    }
}
