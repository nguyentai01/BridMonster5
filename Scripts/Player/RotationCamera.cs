using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotationCamera : MonoBehaviour,IDragHandler, IEndDragHandler
{
    [SerializeField] private Transform playerTransform;
    private float delta;
    private float delta2;

    private void Start()
    {
        delta = 0;
        Debug.Log("Vector :" + transform.TransformDirection(Vector3.forward) + " " + Vector3.forward);
    }
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("data :" + eventData.delta.x);
        Debug.Log("data2 :" + eventData.delta.y);

        float y = playerTransform.rotation.y;
        float x = playerTransform.rotation.x;

        float difference = eventData.pressPosition.x - eventData.position.x;
        delta += eventData.delta.x/5f;
        delta2 += eventData.delta.y / 5f;

        /*Debug.Log(" dif " + difference);*/
        playerTransform.rotation = Quaternion.Euler(x-delta2, y - delta, playerTransform.rotation.z);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        /*throw new System.NotImplementedException();*/
    }
}
