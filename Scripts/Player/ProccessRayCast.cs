using System;
using System.Collections.Generic;

/*using System.Diagnostics;*/
using UnityEngine;
using UnityEngine.EventSystems;

public class ProccessRayCast : MonoBehaviour
{
    private bool CheckInteract;
    private bool checkRayCast;
    public bool IsRun = true;
    public Transform cameraPl;
    public float distance = 6f;
    private Dictionary<string, Action<Transform>> actions;
    public int checkFirstGet = 0;
    private int layerMask;
    private int layerMask2;
    private string tag;
    private bool checkWall = false;

    RaycastHit hit;
    Transform selection;
    Ray ray;
    private void Start()
    {
         
        layerMask = LayerMask.GetMask("Default"/*, "wall", "line"*/);
        layerMask2 = LayerMask.GetMask("Controller");

        actions = new Dictionary<string, Action<Transform>>();
        actions[ConstName.ItemTag] = ProccessItems;
        actions[ConstName.DroneMove] = DroneMove;
        actions[ConstName.DroneMove2] = DroneMove;
        CheckInteract = false;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 screenPosition = Input.mousePosition;
            Vector3 viewportPosition = Camera.main.ScreenToViewportPoint(screenPosition);
            RayCast2(viewportPosition);
        }
    }
    private void RayCast2(Vector3 view)
    {
        ray = Camera.main.ViewportPointToRay(view);
        checkRayCast = Physics.Raycast(ray, out hit, 6  , layerMask2);
        if (checkRayCast)
        {
            MapController.Ins.Map3.MiniGame3.GetJoy();
        }
    }

    private void FixedUpdate()
    {

        if (IsRun)
        {
            SetRayCast();

        }
    }
   
    private void SetRayCast()
    {

        ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        checkRayCast = Physics.Raycast(ray, out hit, distance, layerMask);
        if (checkRayCast && !actions.ContainsKey(hit.collider.tag))
        {
            return;
        }
        if (!checkRayCast)
        {
            if (CheckInteract)
            {
                EventUi.instance.ResetEventUi();
                UiManager.ins.Hide_BtnInteract();
                UiManager.ins.DisPlayBtn_DroneMove(false);
                CheckInteract = false;
            }
            return;
        }
        
        if (!CheckInteract )
        {

            selection = hit.collider.transform;
            tag = hit.collider.tag;
            

            CheckInteract = true;

            string name = hit.collider.name;
            if (actions.ContainsKey(tag))
            {
                actions[tag].Invoke(selection);
                checkWall = true;
            }
        }
         
    }
    private void ProccessItems(Transform select)
    {
        EventUi.instance.SetItem(select);

    }
    private void DroneMove(Transform select)
    {
        if (GameController.ins.CheckDrone)
        {
            Items item = select.GetComponent<Items>();
            EventUi.instance.item = item;
            UiManager.ins.DisPlayBtn_DroneMove(true);
        }

    }

}
