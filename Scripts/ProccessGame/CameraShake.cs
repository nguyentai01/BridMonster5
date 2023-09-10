using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Vector3 vec1;
    public Vector3 vec2;

    public static CameraShake instance { get; private set; }
    private void Awake()
    {
        instance = this;
    }
    
    public void ShakeCamera()
    {
        transform.DOShakePosition(2,2).SetEase(Ease.InOutSine);
  /*      transform.DOShakeRotation(4f, vec2);*/
    }    
}
