using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckInternet : MonoBehaviour
{
    public static CheckInternet instance;
    public bool checkInternet = false;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            checkInternet = true;
            /*FireBaseSet.SetStatusInternet("true");*/
            return;
        }
       /* FireBaseSet.SetStatusInternet("false");*/
    }

    
}
