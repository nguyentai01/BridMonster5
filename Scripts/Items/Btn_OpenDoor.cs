using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Btn_OpenDoor : MonoBehaviour
{
    /* public EnumDoorS type;*/
    public MeshRenderer MeshRenderer;
    public Material[] materials;
    public Material nut1;
    public MeshRenderer nutOpen;
    public OpenDoor OpenDoor;
    public bool Open = false;
    public int IdButton = 0;
    public int IdDoor = -1;
    public IdCard IdCard;
    public int idTru = -1;
    public Btn_OpenDoor btnDoor;
    public bool SetButtonWin = false;
    public int IdPopUp = -1;
    public Transform target;
    public Transform targetlook;
    public Material[] materialsTu;
    public MeshRenderer MeshRendererTu;
    public TruController[] truControllers;
    private void Start()
    {
        if (IdButton == 1)
        {
            gameObject.tag = ConstName.None;
        }
        if (MeshRendererTu != null)
        {
            Material[] mats = new Material[MeshRendererTu.materials.Length];

            mats[0] = null;
            mats[1] = materialsTu[(int)IdCard];
            MeshRendererTu.materials = mats;


        }
    }
    public void SetUnLock()
    {
        if (MeshRenderer != null)
        {

            Material[] mats = new Material[MeshRenderer.materials.Length];
            mats[0] = nut1;
            mats[1] = materials[1];
            MeshRenderer.materials = mats;
        }
        else if (nutOpen != null)
        {
            Material[] mats = new Material[nutOpen.materials.Length];
            mats[0] = materials[0];
            nutOpen.materials = mats;
        }
    }
    public void SetLock()
    {
        if (MeshRenderer != null)
        {
            Material[] mats = new Material[MeshRenderer.materials.Length];
            mats[0] = nut1;
            mats[1] = materials[0];
            MeshRenderer.materials = mats;
        }
        if (nutOpen != null)
        {
            Material[] mats = new Material[nutOpen.materials.Length];
            mats[0] = materials[1];
            nutOpen.materials = mats;
        }
    }
    public void SetOpen()
    {
        if (OpenDoor == null)
        {
            return;
        }
        if (IdButton == 2)
        {
            SetNutWin();
        }
        Open = true;
        SetUnLock();
        OpenDoor.Fn_OpenDoor();

    }
    public void OpenTru()
    {
        for (int i = 0; i < truControllers.Length; i++)
        {
            truControllers[i].Move();
        }
        if (OpenDoor != null)
        {
            OpenDoor.Fn_OpenDoor();
        }
        gameObject.tag = ConstName.None;
        Open = true;
        SetUnLock();
    }
   
    public void SetNutWin()
    {
        btnDoor.SetLock();
        /* btnDoor.gameObject.tag = ConstTag.DungCu;*/
    }
   
    public void SetUnLockButton()
    {
        Debug.Log("" + gameObject.name);
        Material[] mats = new Material[MeshRenderer.materials.Length];
        mats[0] = nut1;
        mats[1] = materials[0];
        MeshRenderer.materials = mats;
        gameObject.tag = ConstName.DroneMove;
    }
    public void SetOpenLoadGame()
    {


        Open = true;
        SetUnLock();
        OpenDoor.SetAniDoor(1);

    }
}

public enum IdCard
{
    Card1 = 0,
    Card2 = 1,
    Card3 = 2,
    Card4 = 3,
    Card5 = 4,
}

