using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && UiManager.ins.GetTutorial())
        {
            gameObject.SetActive(false);
        }
    }
}
