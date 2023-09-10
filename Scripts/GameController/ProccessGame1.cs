using SWS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProccessGame1 : MonoBehaviour
{
    public ProccessSheriff sheriff;
    public ChimProccess[] chimProccesses;
    public AudioSource Warrning;
  

   
    public IEnumerator ProccessGame(float time)
    {
        yield return new WaitForSeconds(time-3);
        MapController.Ins.Map1.openDoors[1].Fn_OpenAni();
        yield return new WaitForSeconds(3);
        chimProccesses[0].gameObject.SetActive(true);
        chimProccesses[0].StartMove();
       
        AudioManagers.Ins.SetSfx(AudioManagers.Ins.ChimAttack, 1);
        StartCoroutine(GameController.ins.SetMusic(AudioManagers.Ins.story4, 1, 4));
        yield return new WaitForSeconds(5);

        chimProccesses[1].StartMove();
        StartCoroutine(sheriff.Proccess1());
        yield return new WaitForSeconds(3);

        MapController.Ins.Map1.openDoors[0].Fn_CloseDoor();
        yield return new WaitForSeconds(3);
        
        UiManager.ins.SetUiMission(1);
    } 
    public void SetAudioWarring(bool status)
    {
        Warrning.mute = status;
        Warrning.Play();
    }
}
