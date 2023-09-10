using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLeft : MonoBehaviour
{
    public static TimeLeft ins;
    private int time;
    private void Awake()
    {
        ins = this;
    }
    private IEnumerator SetTimeLeft()
    {
        yield return new WaitForSeconds(5);
         time = 15;
        UiManager.ins.SetActiveTime(true);
        while (time >= 0)
        {
            UiManager.ins.SetTimeRun(time);
            yield return new WaitForSeconds(1);
            time--;
            if (time < 0)
            {
                StartCoroutine(EventUi.instance.SetWinMap());

            }
        }

    }
    public void StartTimeFire()
    {
        StartCoroutine(SetTimeLeft());
    }
    public void StopTimeFire()
    {
        UiManager.ins.SetActiveTime(false);
        StopAllCoroutines();
    }
}
