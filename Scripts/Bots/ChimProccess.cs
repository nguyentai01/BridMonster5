using DG.Tweening;
using SWS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChimProccess : MonoBehaviour
{
    public splineMove ToadSterPath;
    public Animator anim;
    public NavMeshAgent nav;
    public bool checkAttack = false;
    public int id = 0;
    public AudioSource au;
    public Transform pos;
    public ProccessSheriff sheriff;
    private bool checkCol = false;
    public void SetAni(int status)
    {
        anim.SetInteger(ConstName.Status, status);
    }
    private void OnEnable()
    {
        SetRivive();
    }
    public void StartMove()
    {
        ToadSterPath.StartMove();
        SetAni(1);
        if (au != null && Pref.GetSfx())
        {
            au.mute = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ConstName.Player_Tag) && !checkCol)
        {
            SetOff();

            if (checkAttack)
            {
                CameraShake.instance.ShakeCamera();
                if (id == 0)
                {
                    StartCoroutine(GameController.ins.ProccessPlayer.RotationCamera(transform, 0, false));
                    FireBaseSet.SetFirebase(ConstName.LosedGame + "1_1");
                }
                SetAni(2);
                AudioManagers.Ins.SetSfx(AudioManagers.Ins.ChimAttack, 1);
                StartCoroutine(UiManager.ins.SetOver(2));
                return;
            }
            if (id == 0)
            {
                SetAni(3);
                /*StartCoroutine(UiManager.ins.SetFinish());*/
                AudioManagers.Ins.SetSfx(AudioManagers.Ins.ChimWin, 1);
                StartCoroutine(GameController.ins.ProccessPlayer.RotationCamera(transform, 0, true));
                checkCol = true;
                StartCoroutine(SetMoveAgain());
                /*StartCoroutine(UiManager.ins.SetFinish());*/
            }

        }
    }
    public void SetRunToPlayer(Vector3 target)
    {
        nav.enabled = true;
        nav.SetDestination(target);
    }
    public void SetOff()
    {
        /*nav.SetDestination();*/
        nav.enabled = false;
    }
    private IEnumerator SetMoveAgain()
    {
        yield return new WaitForSeconds(4);
        SetAni(1);
        SetRunToPlayer(pos.position);
        StartCoroutine(SetFinishPath());
    }
    private IEnumerator SetFinishPath()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            float dis = Vector3.Distance(transform.position, pos.position);
            if (dis < 1)
            {

                transform.DOLocalRotate(new Vector3(0, 0, 0), 1).OnComplete(() =>
                {
                    StartCoroutine(GameController.ins.ProccessPlayer.ContinueMovePlayer(true, 0));
                    sheriff.Proccess3();
                    SetOff();
                    SetAni(0);
                });
                yield break;
            }
        }
    }
    public void CheckFinish()
    {
        if (GameController.ins.CheckData())
        {
            SetAni(1);
            checkAttack = true;
            SetRunToPlayer(GameController.ins.ProccessPlayer.transform.position);
            return;
        }
        if (id == 0)
        {
            checkAttack = false;
            SetAni(1);
            SetRunToPlayer(GameController.ins.ProccessPlayer.transform.position);
        }

        else
        {
            SetAni(0);
        }
    }
    public void FinishPath()
    {
        
        gameObject.SetActive(false);

        if (au != null)
        {
            au.mute = true;
        }
    }
    public void SetRivive()
    {
        if (pos != null)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            transform.position = pos.position;
        }
    }
    public void ProccessBird(int Status, Transform parent)
    {
        SetAni(Status);
        transform.parent = parent;
    }
}
