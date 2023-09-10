using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadBullet : MonoBehaviour
{
    public Image ReLoadUi;
    private void OnEnable()
    {
        ReLoadUi.fillAmount = 1;
        ReLoadUi.DOFillAmount(0, 3.5f).SetEase(Ease.Linear).OnComplete(() =>
        {
           
            gameObject.SetActive(false);
        });
    }
}
