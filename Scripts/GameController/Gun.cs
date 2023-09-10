using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Gun : MonoBehaviour
{
   public Animator animator;
    private void OnEnable()
    {
        animator.Play("Up");
    }
    public void SetAni(int index)
    {
        animator.SetInteger(ConstName.Status, index);
        
    }
    public IEnumerator SetStay(float time)
    {
        yield return new WaitForSeconds(time);
        SetAni( 1);
    }
}
