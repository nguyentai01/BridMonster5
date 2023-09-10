using DG.Tweening;
using GoogleMobileAds.Api;
using SWS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class ProccessMiniGame4 : MonoBehaviour
{
    public MoveItemGame4[] moveItemGame4s;
    int id = 0;
    private bool IsMove = false;
    public IEnumerator SetStart()
    {
        IsMove = true;
        for (int i = 0; i < moveItemGame4s.Length; i++)
        {
            moveItemGame4s[i].SetStart();
        }
        while (IsMove)
        {

            yield return new WaitForSeconds(5);
            if (!IsMove)
            {
                yield break;
            }
            id = Random.Range(0, 3);
            for(int i=0; i<moveItemGame4s.Length; i++)
            {
                moveItemGame4s[i].SetReve(id);
            }
        }
    }
    public void StopMiniGame4()
    {
        IsMove = false;
        StopAllCoroutines();
        for (int i = 0; i < moveItemGame4s.Length; i++)
        {
            moveItemGame4s[i].SetPause();
        }
        
    }
}
