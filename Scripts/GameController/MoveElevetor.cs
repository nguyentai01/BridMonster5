using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveElevetor : MonoBehaviour
{
    private bool checkMove = false;
    public Animator animator;
    public Vector3 target;
    public bool CheckFinish = true;


    public IEnumerator MoveEle(Vector3 target, float time)
    {
        yield return new WaitForSeconds(time);
        transform.DOLocalMove(target, 5).OnComplete(() =>
        {
            UiManager.ins.SetUiPause(true);
        });
        AudioManagers.Ins.SetSfx(AudioManagers.Ins.ThangMay, 1);
    }
    public void MoveEle( float time)
    {
       StartCoroutine(MoveEle(target, time));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ConstName.Player_Tag) && !checkMove && CheckFinish)
        {
            checkMove = true;
            StartCoroutine(GameController.ins.playerMovement.SetPausePlayer(false, 0));
            other.gameObject.transform.parent = transform;
            /*SetAni(0);*/
            
            StartCoroutine(MoveEle(new Vector3(0, 0, 0f), 1));
            UiManager.ins.SetRatePopUpStart();
            Pref.SetPointLoadChapter(3);
            Pref.SetPointSave(3);
            StartCoroutine(UiManager.ins.StartConverMap(2, 1, 1));

            FireBaseSet.SetFirebase(ConstName.WinGame + "1");
        }
    }
    public void SetAni(int status)
    {
        animator.SetInteger(ConstName.Status, status);
    }
    public Transform GetTransformAndMove(float time)
    {
        MoveEle(time);
        return transform;
    }
}
