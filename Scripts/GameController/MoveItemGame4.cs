using DG.Tweening;
using GoogleMobileAds.Api;
using SWS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveItemGame4 : MonoBehaviour
{
    public splineMove pm;
    public int PointStart;
    public float Speed = 10;
    public Rigidbody[] rbs;
    public Transform VoBong;
    public FlyItem FlyItem;
    public bool IsTrue = false;
    private bool CheckFirst = false;
    public void SetStart()
    {
        if (!CheckFirst)
        {
            pm.startPoint = PointStart;
            pm.StartMove();
        }
        else
        {
            pm.Resume();
        }
        CheckFirst = true;  
    }
    public void SetPause()
    {
        pm.Pause();
    }
    public void SetReve(int id)
    {
        switch (id)
        {
            case 0:
                pm.Reverse();
                StartCoroutine(SetReveAgain());
                break;
            case 1:
                float speed = 10;
                DOTween.To(() => speed, x => speed = x, 6, 1f).SetEase(Ease.InOutBack).OnUpdate(() =>
                {
                    pm.ChangeSpeed(speed);
                });

                break;
            case 2:
                float speed2 = 10;
                DOTween.To(() => speed2, x => speed2 = x, 4, 1f).SetEase(Ease.InOutBack).OnUpdate(() =>
                {
                    pm.ChangeSpeed(speed2);
                }); break;
        }
    }
    IEnumerator SetReveAgain()
    {
        yield return new WaitForSeconds(0.5f);
        pm.Reverse();
    }
    public void SetBreak()
    {
        if (FlyItem != null)
        {
            FlyItem.Fly();

        }
        AudioManagers.Ins.SetSfx(AudioManagers.Ins.Kinhvo, 1);
        VoBong.parent = null;
        for (int i = 0; i < rbs.Length; i++)
        {
            rbs[i].isKinematic = false;
        }
        /*if (IsTrue)
        {
            EventUi.instance.ExistMiniGame4();
        }*/
    }
}
