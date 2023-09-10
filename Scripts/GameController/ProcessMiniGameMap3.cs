using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessMiniGameMap3 : MonoBehaviour
{
 

    [Header("GameProccess")]


    public Transform Item;
    public Renderer[] rends;

    public Queue<int> slots = new Queue<int>();
    public int index;
    /*  private void FixedUpdate()
      {
          Item.position = SetGame();
      }*/
    private void Start()
    {
        StartCoroutine(StartMove());
    }
    public IEnumerator StartMove()
    {

        yield return new WaitForSeconds(0);
        Item.DOJump(SetGame(), 1, 1, 5).SetEase(Ease.Linear).OnComplete(() =>
        {
            StartCoroutine(StartMove());
        });
    }
    public Vector3 SetGame()
    {
        Bounds bounds = rends[GetSlot()].bounds;
        Vector3 a = new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
               Random.Range(bounds.min.y, bounds.max.y),
               Random.Range(bounds.min.z, bounds.max.z));
        return a;
    }
    public int GetSlot()
    {
        if (rends.Length == 0)
        {
            return 0;
        }
        if (slots.Count > 0)
        {
            
            return slots.Dequeue();
        }
        else
        {
            for (int i = 0; i < rends.Length; i++)
            {
                slots.Enqueue(i);
                
            }
            return GetSlot();
        }
    }
}
