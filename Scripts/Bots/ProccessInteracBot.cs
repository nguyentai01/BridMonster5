using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProccessInteracBot : MonoBehaviour
{
    public ProccessSheriff ps;
    public ComeHereAu ComeHereAu;
    public bool In = false;
    private bool checkProccess = true;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ConstName.Player_Tag)&& In)
        {
            if (ComeHereAu != null)
            {
                ComeHereAu.enabled = false;
            }
            if (!ps.checkMove)
            {
                ps.checkMove = true;
                if (ps.CheckStart && checkProccess)
                {

                    /*ps. SetPauseAudio(false);*/
                    StopAllCoroutines();
                    StartCoroutine(GameController.ins.SetPause(false, 0f));
                    checkProccess = false;
                    ps.SetResume();
                    Invoke("TimeDelay", 3);
                }
            }
        }
       
    }
    private void OnTriggerExit(Collider other)
    {
        if (ps.checkMove && other.CompareTag(ConstName.Player_Tag) && !In)
        {
            ps.checkMove = false;
            if (ps.CheckStart && checkProccess)
            {
                if(!ps.checkOpenDoor1)
                {
                    StartCoroutine(GameController.ins.SetPause(true,0));
                    AudioManagers.Ins.SetRandomAudioSheriff();
                    StartCoroutine(GameController.ins.SetPause(false, 0.6f));
                }
                /*ps.SetPauseAudio(true);*/

                checkProccess = false;
                ps.Target = GameController.ins.ProccessPlayer.transform;
                ps.PauseMove(1);
                Invoke("TimeDelay", 3);

            }
        }
    }
    private void TimeDelay()
    {
        checkProccess = true;
    }
}
