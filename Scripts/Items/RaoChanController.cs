using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaoChanController : MonoBehaviour
{
    public Rigidbody rbs;
    public SkinnedMeshRenderer SkinnedMeshRenderer;
    
    public void SetAttackCua()
    {
        float value = 0;
        DOTween.To(() => value, x => value = x, 100, 1).OnUpdate(() =>
        {
            SkinnedMeshRenderer.SetBlendShapeWeight(0, value);

        });
        rbs.AddForce(new Vector3(-1000,300,0));
    }
}
