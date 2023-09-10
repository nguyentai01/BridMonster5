using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    public static EffectsManager instance;
    public GameObject EffectNo;

    private void Awake()
    {
         instance = this;
    }

    public void SetEffectNo(Transform pos)
    {
        EffectNo.transform.position = pos.position;
        EffectNo.gameObject.SetActive(true);   
    }
}
