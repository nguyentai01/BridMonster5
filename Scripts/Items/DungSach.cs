using HighlightPlus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungSach : MonoBehaviour
{
    public HighlightEffect HighlightEffect;
    public InterGlow InterGlow;
    public GameObject Sach;
    public int id=0;
    public bool IsSet = false;
    public GameObject tutorial;
    public void SetWin()
    {
        HighlightEffect.enabled = false;
        InterGlow.enabled = false;
        Sach.SetActive(true);
        IsSet = true;
        if (tutorial != null)
        {
            tutorial.SetActive(false);
        }
    }

}
