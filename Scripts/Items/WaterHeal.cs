using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterHeal : MonoBehaviour
{
    float i = 0;
    public int index = 0;
    public void SetScale()
    {
        index++;
        i += 0.1f;
        transform.DOScale(new Vector3(1, i, 1), 1); 
        if (index==6)
        {
            UiManager.ins.SetUiMission(11);
            AudioManagers.Ins.SetSfx(AudioManagers.Ins.WinMap, 1);
            GameController.ins.SetCamMap3();
            MapController.Ins.Map3.BtnWin.SetUnLockButton();
        }
    }
    public void SetScaleFinish()
    {
        AudioManagers.Ins.SetSfx(AudioManagers.Ins.LayMau, 1);
        transform.DOScale(new Vector3(1, 1, 1), 2).OnComplete(() =>
        {

            ProccessMap3.Instance.SetStartRua();
            
        });
       
    }
    
}
