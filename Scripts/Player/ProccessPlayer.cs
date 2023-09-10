using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProccessPlayer : MonoBehaviour
{
    public GameObject Camera;
    private Dictionary<string, Action<GameObject>> actions;
    public PlayerMovement pm;
    public SetCamControl sc;
    public Transform target2;
    private bool CheckDelay = false;
    private bool CheckCam4 = false;
    public Transform target;
    private bool IsCheckLookat = false;
    public GameObject light;
    private void Start()
    {
        actions = new Dictionary<string, Action<GameObject>>();
        actions[ConstName.SetCamera] = SetCamera;
        actions[ConstName.BoxDrone] = BoxDrone;
        actions[ConstName.PointSave] = PointSave;
        actions[ConstName.GameOver] = SetGameOver;
        actions[ConstName.SetPlayer] = SetPlayer;
        actions[ConstName.Buff_Low] = SetBuff_Low;
        actions[ConstName.GameWin] = SetGameWin;

        CheckDelay = false;
        StartCoroutine(SetDelay());
    }
    private void Update()
    {
        if (IsCheckLookat)
        {
            Camera.transform.LookAt(target);
        }
    }
    private IEnumerator SetDelay()
    {
        yield return new WaitForSeconds(1);
        CheckDelay = true;
    }
    private void BoxDrone(GameObject other)
    {
        string s = other.name.Substring(other.name.IndexOf("P") + 1, 2);
        EventUi.instance.AddressPlayer = Int32.Parse(s);
    }
    private void PointSave(GameObject other)
    {
        int IdSavePoint = Int32.Parse(other.name.Substring(other.name.Length - 2, 2).ToString());

        Pref.SetPointSave(IdSavePoint);
        Pref.SetCheckSave(1);
        

        if (other.name[0].ToString().Equals("L"))
        {

            Pref.SetPointLoadChapter(IdSavePoint);

        }
        SetPlay(IdSavePoint);
    }
    public void SetGameWin(GameObject other)
    {
        other.gameObject.tag = ConstName.None;
        UiManager.ins.SetRatePopUpStart();
        Pref.SetPointLoadChapter(6);
        Pref.SetPointSave(6);
        if (Int32.Parse(other.name[other.name.Length - 1].ToString()) == 2)
        {
            MapController.Ins.Map3.rua.StopRun();
            StartCoroutine(UiManager.ins.StartConverMap(2, 1,2));
        }
        else
        {
            StartCoroutine(UiManager.ins.StartConverMap(2, 1, 1));
        }
        ProccessMap3.Instance.SetAudio();


    }
    private void SetPlay(int point)
    {
        switch (point)
        {
            case 1: FireBaseSet.SetPlayGame("1_1"); break;
            case 2: FireBaseSet.SetPlayGame("1_2"); break;
           /* case 3: FireBaseSet.SetPlayGame("2_1"); break;*/
            /*case 4: FireBaseSet.SetPlayGame("2_1"); break;*/
            case 5: FireBaseSet.SetPlayGame("2_2"); break;
            case 7: FireBaseSet.SetPlayGame("3_2"); break;
            case 8: FireBaseSet.SetPlayGame("3_5"); break;
            case 9: FireBaseSet.SetPlayGame("3_4"); break;
            case 10: FireBaseSet.SetPlayGame("3_3"); break;
            case 11: FireBaseSet.SetPlayGame("3_1"); break;

        }
    }
    private void SetBuff_Low(GameObject other)
    {
        StartCoroutine(UiManager.ins.SetOver(2));
        FireBaseSet.SetFirebase(ConstName.LosedGame + "2_2");
    }
    private void SetCamera(GameObject other)
    {
        if (!GameController.ins.CheckDrone /*|| CheckCam4*/)
        {
            return;
        }
        if (!GameController.ins.CheckPlayer)
            EventUi.instance.SetPlayer();
        other.tag = ConstName.None;
        StartCoroutine(ContinueMovePlayer(false, 0));

        pm.setMove();
        switch (Int32.Parse(other.name[other.name.Length - 1].ToString()))
        {
            case 1:
                GameController.ins.ProccessPlayer1();
                StartCoroutine(SetCam(false, 6));
                StartCoroutine(GameController.ins.ProcceessGame1());
                break;
            case 2:
                GameController.ins.ProccessPlayer2();
                UiManager.ins.DisPlayTutorial(true);
                break;
            case 3:
                StartCoroutine(RotationCamera(MapController.Ins.ProccessPlayer3(), 0, true));
                break;
            case 4:
                StartCoroutine(ProccessCam4());
                FireBaseSet.SetPlayGame("2_3");
                /* StartCoroutine(OpenDoor6(5));*/
                break;
            case 5:
                StartCoroutine(RotationCamera(MapController.Ins.Map2.GiaSach.transform, 0, true));
                StartCoroutine(ContinueMovePlayer(true, 2));
                break;
            case 6:
                GameController.ins.SetCamMinigame3();

                StartCoroutine(ContinueMovePlayer(true, 4.5f));
                break;
        }
    }
   
    private IEnumerator ProccessCam4()
    {
        yield return new WaitForSeconds(1);
        if (!GameController.ins.CheckPlayer)
        {
            EventUi.instance.SetPlayer();
        }
        AudioManagers.Ins.SetSfx(AudioManagers.Ins.TrauRong, 1);
        /* CheckCam4 = true;*/
        StartCoroutine(MapController.Ins.Map2.ProccessDomDom());
        StartCoroutine(ContinueMovePlayer(false, 0));
        StartCoroutine(ContinueMovePlayer(true, 6));
        StartCoroutine(UiManager.ins.SetDrone(6.1f, false));

        GameController.ins.ProccessMap2_1();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (CheckDelay)
        {
            if (actions.ContainsKey(other.tag))
            {
                actions[other.tag].Invoke(other.gameObject);
            }
        }


    }


    public IEnumerator RotationCamera(Transform target, float Time, bool callBack)
    {

        StartCoroutine(ContinueMovePlayer(false, 0));
        yield return new WaitForSeconds(Time);
        Vector3 dir = (Vector3)transform.position - target.position;
        float radi = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        Vector3 x = new Vector3(0, /*-47.081f*/ 180 + radi, 0);
        sc.tempCamTarget = x;
        Camera.transform.DORotate(x, 1).OnComplete(() =>
        {
            if (callBack)
            {
                sc.moveCamPath = false;
            }

        });

    }
    public IEnumerator OpenDoor6(float time)
    {
        Vector3 a = Camera.transform.rotation.eulerAngles;
        StartCoroutine(RotationCamera(MapController.Ins.Map2.buff_Low.transform, 0, false));
        yield return new WaitForSeconds(time);
        a = new Vector3(0, a.y, 0);
        sc.tempCamTarget = a;
        Camera.transform.DORotate(a, 1).OnComplete(() =>
        {
            StartCoroutine(ContinueMovePlayer(true, 0));
        });
    }
    public IEnumerator ContinueMovePlayer(bool status, float time)
    {
        yield return new WaitForSeconds(time);

        
        UiManager.ins.SetUiPause(status);
        SetMovePlayer(status);
        sc.moveCamPath = !status;
        pm.setMove();
    }
    public IEnumerator SetCam(bool status, float time)
    {
        yield return new WaitForSeconds(time);

        sc.moveCamPath = status;
    }
    public IEnumerator ContinueMove(float time, bool status)
    {
        yield return new WaitForSeconds(time);
        SetMovePlayer(status);
    }
    public void SetMovePlayer(bool status)
    {
        pm.canMove = status;
    }
    public void SetGameOver(GameObject other)
    {
        StartCoroutine(UiManager.ins.SetOver(0));
    }
    public void SetPlayer(GameObject other)
    {
        GameController.ins.SetUpPlayer(false, -1);
    }
    public IEnumerator SetParentPlayer(Transform parent, float time)
    {
        yield return new WaitForSeconds(time);
        transform.parent = parent;
    }
    public void ProccessSavePoint3(Transform parent)
    {
        StartCoroutine(SetParentPlayer(parent, 0));
        StartCoroutine(SetParentPlayer(null, 4.5f));

        StartCoroutine(pm.SetPausePlayer(false, 0f));
        StartCoroutine(pm.SetPausePlayer(true, 4.5f));
    }
  
    public IEnumerator SetGameOverMap2()
    {
        target = MapController.Ins.Map2.DauBot2;
        StartCoroutine(RotationCamera(target, 0, false));

        yield return new WaitForSeconds(1);
        IsCheckLookat = true;
        
    }
    public void SetMoveElevator(Transform parent,float time,bool Move)
    {
        if (Move)
        {
            gameObject.layer = LayerMask.NameToLayer(ConstName.Player);
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer(ConstName.NullBox);
        }
        StartCoroutine(pm.SetPausePlayer(Move, time));
        StartCoroutine(SetParentPlayer(parent, time));
       
    }
}
