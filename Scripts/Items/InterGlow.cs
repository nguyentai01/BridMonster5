using DG.Tweening;
using HighlightPlus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterGlow : MonoBehaviour
{
    public HighlightEffect highlightEffect;
    private void Start()
    {
        StartCoroutine(IeHighlight());
      /*  RunGlow();*/
    }
    private void RunGlow()
    {
        float y = 0;
        DOTween.To(x => y = x, 0, 1, 1.5f).SetLoops(-1, LoopType.Yoyo).OnUpdate(() =>
        {
            highlightEffect.innerGlow =y;
        });
    }
    IEnumerator IeHighlight()
    {
        yield return new WaitForSeconds(Random.Range(0f, 0.5f));
        if (highlightEffect != null)
        {
            while (highlightEffect.innerGlow <= 1)
            {
                highlightEffect.innerGlow += Time.deltaTime;
                yield return null;
            }
            while (highlightEffect.innerGlow > 0)
            {
                highlightEffect.innerGlow -= Time.deltaTime;
                yield return null;
            }
            StartCoroutine(IeHighlight());
        }
    }
}
