using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public static MapController Ins { get; private set; }

    public Map1 Map1;
    public Map2 Map2;
    public Map3 Map3;

    private void Awake()
    {
        Ins = this;

    }




    public Transform ProccessPlayer3()
    {
        Map1.BiaDan.SetActive(true);
        StartCoroutine(GameController.ins.SetShowCam3(50, 20, true));
        UiManager.ins.SetUiMission(4);
        StartCoroutine(GameController.ins.SetMusic(AudioManagers.Ins.story10, 1, 0));
        return Map1.BiaDan.transform;
    }



    public void ProccessFinishMap1()
    {
        GameController.ins.ProccessSheriff.SetAni(0);
        Map1.MoveElevetor.MoveEle(0);
        /*Map1.MoveElevetor.SetAni(1);*/
        Map1.LanCanWin.DOLocalMove(new Vector3(1.70850027f, -0.192000002f, 5.8350606f), 2);
        GameController.ins.ProccessPlayer.SetMovePlayer(true);
    }
}
