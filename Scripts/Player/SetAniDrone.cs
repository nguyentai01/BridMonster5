using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAniDrone : MonoBehaviour
{
    public Animator Animator;

    public static SetAniDrone instance { get; private set; }
    private void Awake()
    {
        
        instance = this;
    }
    public void setAni()
    {
        Animator.SetInteger("status", 1);
    }
}
