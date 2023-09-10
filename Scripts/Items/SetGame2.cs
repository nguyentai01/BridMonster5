using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGame2 : MonoBehaviour
{
    public bool IsJump = false;
    public bool IsTrap = false;
    private bool IsStart = false;
    public GameObject Parent;
    public Rigidbody RbParent;
    public GameObject SetNav;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ConstName.Player_Tag)&& IsTrap && !IsStart)
        {
            IsStart = true;

            Parent.transform.DOShakePosition(1f, 0.02f).SetEase(Ease.InOutSine).OnComplete(() =>
            {
                /*for (int i = 0; i < rbs.Length; i++)
                {
                    rbs[i].isKinematic = false;
                    clds[i].layer = LayerMask.NameToLayer("Drone");
                }*/
                SetNav.SetActive(false);
                RbParent.isKinematic = false;
            });
        }
    }
}
