using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProccessRaycastPlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(ConstName.Player_Tag))
        {
            gameObject.SetActive(false);
        }
    }
}
