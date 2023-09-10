using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCam : MonoBehaviour
{
    public Rigidbody Rigidbody;
    private void OnEnable()
    {
        Vector3 a = MapController.Ins.Map2.buff_Low.transform.position- transform.position;
        a.Normalize();
        Rigidbody.AddForce(a * 50);
    }
}
