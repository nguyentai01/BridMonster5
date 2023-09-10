using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComeHereAu : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(AudioComeHere());
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
    private IEnumerator AudioComeHere()
    {
        while (true)
        {
            yield return new WaitForSeconds(6);
            AudioManagers.Ins.SetRandomAudioSheriff();
        }
    }
}
