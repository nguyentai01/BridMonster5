using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public Transform Gun;
    public GameObject Bullet;
    private BulletFire BulletFire;
    private bool checkFire = false;
    public RayCastGun RayCastGun;
    public Gun gunSet;
    public Transform TargetFire;
    private GameObject g;
    private void Start()
    {
        g = Instantiate(Bullet, Gun);
        BulletFire = g.GetComponent<SetBullet>().BulletFire;
    }
    public void Btn_Fire()
    {
        if (checkFire)
        {
            return;
        }

        Vector3 target = RayCastGun.SetRayCast2();

        gunSet.SetAni(2);
        target = target == Vector3.zero ? TargetFire.transform.position : target;
        BulletFire.FireBullet2(target);
        checkFire = true;

        StartCoroutine(SetFireBullet());
    }
    private IEnumerator SetFireBullet()
    {
        UiManager.ins.SetFireBtn(true, 1);
        yield return new WaitForSeconds(0.5f);
        gunSet.SetAni(3);
        StartCoroutine(gunSet.SetStay(2));
        yield return new WaitForSeconds(1f);
        BulletFire = Instantiate(Bullet, Gun).GetComponent<SetBullet>().BulletFire;
        yield return new WaitForSeconds(2f);

        checkFire = false;
    }
}
