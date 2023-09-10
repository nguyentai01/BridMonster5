using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TruController : MonoBehaviour
{
    public bool IsTru = true;
    public SetMovePlayerMap4 SetMovePlayerMap4;
    public void Move()
    {

        AudioManagers.Ins.SetSfx(AudioManagers.Ins.NangTru, 1);
        if (IsTru)
        {
            MoveTru(new Vector3(transform.localPosition.x, 0, transform.localPosition.z),0.5f);
            return;
        }
        SetMovePlayerMap4.IsMove = false;
        MoveTru(new Vector3(0, 0, 0),1);

    }
    public void MoveTru(Vector3 a,float time)
    {
        transform.DOLocalMove((a), time);

    }
    public void MoveDown()
    {
        SetMovePlayerMap4.IsMove = true;
        AudioManagers.Ins.SetSfx(AudioManagers.Ins.NangTru, 1);
        
        MoveTru(new Vector3(0.08f, 0, 0),0.5f);

    }
}
