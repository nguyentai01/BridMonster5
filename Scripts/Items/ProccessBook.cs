using HighlightPlus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProccessBook : MonoBehaviour
{
    public bool isTrue = true;
    public InterGlow InterGlow;
    public HighlightEffect HighlightEffect;
    public ViTri viTri;
    public void SetTrue()
    {
        isTrue = true;
        InterGlow.enabled = false;
        HighlightEffect.enabled = false;
        gameObject.tag = ConstName.None;
    }
}
