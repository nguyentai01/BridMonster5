using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiaDan : MonoBehaviour
{
    public Animator animator;
    public bool IsFire = false;
    private bool CheckFire = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ConstName.DanItem) && !CheckFire && IsFire)
        {
            CheckFire = true;
            StartCoroutine(SetActive(other.gameObject));
            AudioManagers.Ins.SetSfx(AudioManagers.Ins.BanTrung, 1);
        }
    }
    private IEnumerator SetActive(GameObject other)
    {
        other.transform.parent = transform;
        animator.SetInteger(ConstName.Status, 2);
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
    }
}
