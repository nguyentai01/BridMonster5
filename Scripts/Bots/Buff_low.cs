using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Buff_low : MonoBehaviour
{
    public NavMeshAgent nav;
    public Animator animator;
    public RaoChanController roaChanController;
    private bool PhaCua = false;
    public Transform[] Pillars;
    public Vector3 TargetAttack;
    public bool IsAttack = false;
    public int IdTruHuc;
    public OpenDoor door;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ConstName.RaoChan) && !PhaCua)
        {
            PhaCua = true;
            roaChanController.SetAttackCua();
            SetAni(2);
            StartCoroutine(Run(3));
        }
        else if (other.CompareTag(ConstName.SetBuff))
        {
            door.Fn_CloseDoor();
            other.gameObject.SetActive(false);
            StopAgent();
            SetAni(3);
            Invoke("StartGame",2);
        }
    }
    public void StartGame()
    {
        if (GameController.ins.IsAttackPillar())
        {
            IdTruHuc = GameController.ins.idTru;
            TargetAttack = Pillars[IdTruHuc].position;
            StartCoroutine(ProccessAttack());
        }
        else
        {
            TargetAttack = GameController.ins.ProccessPlayer.transform.position;
            StartCoroutine(ProccessAttack());
        }
    }
    public IEnumerator ProccessAttack()
    {

        Vector3 dir = (Vector3)transform.position - TargetAttack;

        transform.DORotate(new Vector3(0, 180 + (Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg), 0), 1);

        yield return new WaitForSeconds(5);
        IsAttack = true;
        SetAni(4);
        transform.DOMove(new Vector3(TargetAttack.x,transform.position.y, TargetAttack.z), 0.5f).SetEase(Ease.InCubic).OnComplete(() =>
        {
            if (IdTruHuc == GameController.ins.idTru)
            {
                GameController.ins.idTru = -1;
            }
            IsAttack = false;
            SetAni(5);
            StartGame();
        });
    }
    public IEnumerator Run(float time)
    {
        yield return new WaitForSeconds(time);
        SetAni(1);
        nav.enabled = true;
        nav.SetDestination(GameController.ins.ProccessPlayer.transform.position);
    }
    public void StopAgent()
    {
        nav.enabled = false;
    }
    public void SetAni(int status)
    {
        animator.SetInteger(ConstName.Status, status);
    }
    private IEnumerator AttackDoor(float time)
    {
        yield return new WaitForSeconds(time);
        SetAni(4);
        transform.DOMove(MapController.Ins.Map2.TargetRaoChan.transform.position, 0.5f).SetEase(Ease.InCubic).OnComplete(() =>
        {
            AudioManagers.Ins.SetSfx(AudioManagers.Ins.TrauHucCua, 1);
            SetAni(0);
            StartCoroutine(Run(2));
        });
    }
    public void SetStart()
    {
        SetAni(3);
        StartCoroutine(AttackDoor(2));
    }
    public void SetStopBuff()
    {
        StopAllCoroutines();
        TargetAttack = GameController.ins.ProccessPlayer.transform.position;
        StartCoroutine(ProccessAttack());
    }
}
