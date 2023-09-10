using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyUi : MonoBehaviour
{
    public Transform target;
    private void OnEnable()
    {
        DisPlayKey();
    }
    public void DisPlayKey()
    {
        transform.localPosition = new Vector3(0, 0, 0);
        transform.DOScale(new Vector3(0.5f, 0.5f, 0.5f), 0);
        transform.gameObject.SetActive(true);
        transform.DOScale(new Vector3(1.7f, 1.7f, 1.7f), 0.7f).OnComplete(() =>
        {
            transform.DOScale(new Vector3(1f, 1f, 1f), 0.8f);
            transform.DOMove(target.position, 0.35f).OnComplete(() =>
            {
                transform.gameObject.SetActive(false);
            });
        });
  
    }
}
