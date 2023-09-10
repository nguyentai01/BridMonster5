using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProccessItem : MonoBehaviour
{
    public Animator animator;
    public GameObject Lock;
    public Transform SetAni(int status)
    {
        Lock.SetActive(false);
        animator.SetInteger(ConstName.Status, status);
        return transform;
    }
}
