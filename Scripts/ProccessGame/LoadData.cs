using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadData : MonoBehaviour
{
    public GameObject[] Maps;
    public static LoadData ins;
    private void Awake()
    {
        ins = this;
        DontDestroyOnLoad(gameObject);
    }
}
