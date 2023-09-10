using DG.Tweening;
using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Rua2Dau : MonoBehaviour
{
    public NavMeshAgent Agent;
    private Transform player;
    public Animator animator;
    public SetBreak SetBreak;
    private bool Run = false;
    private void FixedUpdate()
    {
        if (Run)
        {
            Agent.SetDestination(player.position);
        }
    }
    private void OnEnable()
    {
        player = GameController.ins.ProccessPlayer.transform;
        float height = 0;
        SetBreak.SetBreakGame(); 
        DOTween.To(() => height, x => height = x, 2, 2f).OnUpdate(() =>
        {
            transform.localScale = new Vector3(height, height, height);
        }).OnComplete(() =>
        {
            StartCoroutine(StartRua());
           
        });





        /*  transform.Doloc(new Vector3(20, 20, 20), 3).OnComplete(() =>
          {

          });*/
    }
    private IEnumerator StartRua()
    {
       
        AudioManagers.Ins.SetSfx(AudioManagers.Ins.TiengRong, 1);
        UiManager.ins.SetUiMission(12);
        yield return new WaitForSeconds(1);

        StartCoroutine(GameController.ins.ProccessPlayer.RotationCamera(MapController.Ins.Map3.SetCuaWin(), 0, true));
        yield return new WaitForSeconds(1);
        Run = true;
        SetAni(1);
        StartCoroutine(GameController.ins.ProccessPlayer.ContinueMovePlayer(true, 0));
    }
    public void SetAiRua(Transform pos)
    {
        Agent.SetDestination(pos.position);
    }
    private void SetAni(int status)
    {
        animator.SetInteger(ConstName.Status, status);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ConstName.Player_Tag)&& Run) {
            Run = false;
            Agent.enabled = false;
            StartCoroutine(GameController.ins.ProccessPlayer.RotationCamera(transform, 0, false));
            StartCoroutine(SetAttack());
        }

    }
    private IEnumerator SetAttack()
    {
        yield return new WaitForSeconds(0.5f);
        SetAni(2);
        AudioManagers.Ins.SetSfx(AudioManagers.Ins.QuaiTanCong, 1);
        CameraShake.instance.ShakeCamera();
    }
    public void StopRun()
    {
        Run = false;
        SetAni(0);
    }
}
