using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyItem : MonoBehaviour
{
    public Renderer Renderer;
    public Rigidbody Rigidbody;
    public bool IsBom = true;
    public void Fly()
    {
        Rigidbody.isKinematic = false;
        if (IsBom)
        { StartCoroutine(UiManager.ins.SetOver(1.5f)); }
          
        transform.DOJump(SetGame(), 1, 1, 1).OnComplete(() =>
        {
            if (IsBom)
            {
                EffectsManager.instance.SetEffectNo(transform);
                
                
                /*gameObject.SetActive(false);*/
                FireBaseSet.SetFirebase(ConstName.LosedGame + "3_4");
            }
        });
    }
    public Vector3 SetGame()
    {
        Bounds bounds = Renderer.bounds;
        return new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
               Random.Range(bounds.min.y, bounds.max.y),
               Random.Range(bounds.min.z, bounds.max.z)); ;
    }
}
