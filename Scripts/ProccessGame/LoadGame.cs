using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadGame : MonoBehaviour
{
    public Image imgLoading;
   
    private void Start()
    {
        Pref.SetCountPopup(0);
        Pref.SetShowRate(0);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1);
        asyncLoad.allowSceneActivation = false;
        imgLoading.DOFillAmount(1, 4).OnComplete(() =>
        {
            asyncLoad.allowSceneActivation = true;
            
        });
    }
}
