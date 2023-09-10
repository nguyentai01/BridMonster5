using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowRate : MonoBehaviour
{
    public static ShowRate ins;
    public int showReWard;
    private void Awake()
    {
        ins = this;
    }
    void Start()
    {
        if (Pref.GetShowRate() == 1)
        {
            showReWard = 5;
            StartTimeRate(480, true);
            return;
        }

        showReWard = 4;
        StartTimeRate(300, true);
    }

    public void StartTimeRate(float time,bool checkFirst)
    {
        if (!checkFirst)
        {
            ResetData();

        }
        StartCoroutine(TimeReWard(time));
    }
    public void StopTimeRate()
    {
        StopAllCoroutines();
    }
    private IEnumerator TimeReWard(float time)
    {
        yield return new WaitForSeconds(time);
        UiManager.ins.SetRatePopUpStart();
    }
    public void ResetData()
    {
        StopTimeRate();
        showReWard = 5;

    }
    public void SetReward()
    {
        showReWard--;
        if (showReWard <= 0 && (Pref.GetRate()==0))
        {
            showReWard = 5;
            UiManager.ins.SetRatePopUpStart();
        }
    }
}
