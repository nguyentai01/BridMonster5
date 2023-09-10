using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFire : MonoBehaviour
{
    public float speed = 25;
    public bool CheckFire = false;
    public bool CheckMiniGame4 = false;
    private bool isFireTrue = false;
    public void FireBullet(Vector3 target)
    {
        transform.parent = null;
        float dis = Vector3.Distance(transform.position, target);

        if (dis < 100)
        {
            transform.DOJump(target, 0.5f, 0, dis / speed).SetEase(Ease.Linear).OnComplete(() =>
            {
                if (dis > 100)
                {
                    gameObject.SetActive(false);
                }
            });
        }
        else
        {
            transform.DOMove(target, dis / speed).SetEase(Ease.Linear).OnComplete(() =>
            {
                if (dis > 100)
                {
                    gameObject.SetActive(false);
                }
            });
        }

        CheckFire = true;
        transform.DOScale(new Vector3(20, 20, 20), dis / (speed * 2)).SetEase(Ease.Linear);
    }
    public void FireBullet2(Vector3 target)
    {

        transform.parent = null;
        float dis = Vector3.Distance(transform.position, target);



        transform.DOMove(target, dis / speed).SetEase(Ease.Linear).OnComplete(() =>
        {
            gameObject.SetActive(false);

        });


        CheckFire = true;
        transform.DOScale(new Vector3(20, 20, 20), dis / (speed * 2)).SetEase(Ease.Linear);
    }
    private void PauseMove()
    {
        transform.DOPause();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!CheckMiniGame4 && isFireTrue)
        {
            return;
        }
        if (other.CompareTag(ConstName.Ballon) && !isFireTrue)
        {
            Debug.Log("name " + other.gameObject.name);
            gameObject.SetActive(false);
            other.gameObject.GetComponent<MoveItemGame4>().SetBreak();
            PauseMove();
            isFireTrue = true;
            transform.parent = other.transform;
            return;
        }
        if (other.CompareTag(ConstName.None) && !isFireTrue)
        {
            gameObject.SetActive(false);
            PauseMove();
            isFireTrue = true;
            transform.parent = other.transform;
        }
    }
}
