using SWS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerElevator : MonoBehaviour
{
    public splineMove pm;
    public PathManager pathManager1;
    public PathManager pathManager2;

    [Header("SetMaterial")]
    public Material[] MaterialsEnable;
    public Material[] MaterialsDisable;
    public MeshRenderer[] mesh1;
    public MeshRenderer[] mesh2;

    [Header("Proccess")]
    public int[] SetPointPauses;

    public int SetPointPause = -1;
    private int OldPoint = -1;
    public bool IsPlayr = false;
    private bool SetMoveEle = false;
    public AudioSource aus;
    private void Start()
    {
        SetAudio(false);
    }
    public void SetGame()
    {
        if (SetPointPause == -1 || SetPointPause == SetPointPauses[0])
        {
            return;
        }
        if (pm.currentPoint == SetPointPause)
        {

            pm.Pause();
            SetMoveElevator(false);
        }
        if (pm.pathContainer.ToString().Equals(pathManager2.ToString()) && pm.currentPoint == 0)
        {
            pm.reverse = false;
            pm.pathContainer = pathManager1;
            pm.loopType = LoopType.loop;
            pm.StartMove();
            pm.Pause();
        }
    }
    public void SetFinish()
    {
        if (pm.pathContainer.ToString().Equals(pathManager2.ToString()))
        {
            pm.loopType = LoopType.none;
            pm.Pause();
            SetMoveElevator(false);
            return;
        }
        if (SetPointPause == SetPointPauses[0])
        {
            pm.reverse= false;
           pm.pathContainer = pathManager2;
            pm.StartMove();
        }
   
    }
    public bool SetMove(int index)
    {
        if (pm.pathContainer.ToString().Equals(pathManager1.ToString()))
        {
            if (pm.currentPoint != SetPointPause && SetPointPause != -1)
            {
                return false;
            }

        }
        else
        {
            if (index == 0)
            {
                return false;
            }
        }
        
        SetMoveElevator(true);
        if (SetPointPause == -1)
        {
            SetPointPause = SetPointPauses[0];

        }
        if (OldPoint != -1)
        {
            mesh1[OldPoint].material = MaterialsDisable[OldPoint];
            mesh2[OldPoint].material = MaterialsDisable[OldPoint];
        }
        else
        {
            mesh1[0].material = MaterialsDisable[0];
            mesh2[0].material = MaterialsDisable[0];
        }
        mesh1[index].material = MaterialsEnable[index];
        mesh2[index].material = MaterialsEnable[index];
       

        if (SetPointPause == SetPointPauses[0])
        {
            pm.startPoint = 4;
            pm.reverse = true;
            pm.StartMove();
        }
       
        pm.Resume();
        SetPointPause = SetPointPauses[index];
        if (index == OldPoint - 1)
        {
            pm.startPoint = SetPointPauses[OldPoint];
            pm.reverse = true;
            pm.StartMove();

        }
        else if (OldPoint >= 1)
        {
            pm.startPoint = SetPointPauses[OldPoint];
            pm.reverse = false;
            pm.StartMove();
        }
        OldPoint = index;
        UiManager.ins.SetPopUp(24, ConstName.CheckWatchAdsPopUp[13]);
        return true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ConstName.Player_Tag)) 
        {

            IsPlayr = true;


        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(ConstName.Player_Tag))
        {

            IsPlayr = false;


        }
    }
    private void SetMoveElevator(bool status)
    {
        SetAudio(status);
        SetMoveEle = status;
       
        if (status && IsPlayr)
        {
            GameController.ins.ProccessPlayer.SetMoveElevator(transform, 0, false);
        }
        else if (!status)
        {
            GameController.ins.ProccessPlayer.SetMoveElevator(null, 0, true);
        }
    }
    private void SetAudio(bool status)
    {
        if (!status)
        {
            aus.Stop();
            return;
        }
        aus.Play();
    }
    public IEnumerator SetSpeed()
    {
        pm.ChangeSpeed(20);
        yield return new WaitForSeconds(15);
        pm.ChangeSpeed(10);
    }
}
