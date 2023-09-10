using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTru : MonoBehaviour
{
    public Rigidbody[] rbs;
    public GameObject[] clds;

    public GameObject JumpPlayer;
    private bool IsDestroy = false;
    public int IdTru = -1;
    public GameObject DieuKhien;
    public Transform TruMain;
   
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag(ConstName.Buff_Low) && !IsDestroy)
        {
            if (MapController.Ins.Map2.buff_Low.IsAttack)
            {
                IsDestroy = true;
                StartCoroutine(SetJump());
                AudioManagers.Ins.SetSfx(AudioManagers.Ins.HucTru, 1);
                TruMain.DOShakePosition(1f, 0.02f).SetEase(Ease.InOutSine).OnComplete(() =>
                {
                    for (int i = 0; i < rbs.Length; i++)
                    {
                        rbs[i].isKinematic = false;
                        clds[i].layer = LayerMask.NameToLayer("Drone");
                    }
                });
               
                if (IdTru != -1)
                {
                   
                        GameController.ins.SetTruDestroy(IdTru);

                    
                }
                if (DieuKhien != null)
                {
                    DieuKhien.SetActive(false);
                }
            }

        }
    }
    private IEnumerator SetJump()
    {
        yield return new WaitForSeconds(1);
        JumpPlayer.SetActive(false);
    }
}
