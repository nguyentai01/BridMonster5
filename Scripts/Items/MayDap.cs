using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MayDap : MonoBehaviour
{
    public GameObject WaterHealRua;
    public Animator Animator;
    public MeshRenderer MeshRenderer;
    public Material BangMau;
    public Material[] Mats;
    public GameObject canGat;
    public CanGat canGatScript;
    private int id;
    public int IdMayDap =0;
   
    public void SetDap(int id)
    {
        this.id = id;
        Material[] mats = new Material[MeshRenderer.materials.Length];

        mats[0] = BangMau;
        mats[1] = Mats[0];
        MeshRenderer.materials = mats;

        canGat.tag = ConstName.ItemTag;
        ProccessMap3.Instance.GetManhRua(this.id,transform);
       
    }
    public void SetFinish()
    {
        MapController.Ins.Map3.WaterHeal.SetScale();
        WaterHealRua.gameObject.SetActive(false);
    }
    public void SetAni(int status)
    {
        Animator.SetInteger(ConstName.Status, status);
    }
    public void Ep()
    {
        Material[] mats = new Material[MeshRenderer.materials.Length];

        mats[0] = BangMau;
        mats[1] = Mats[1];
        MeshRenderer.materials = mats;

        WaterHealRua.SetActive(true);
        SetAni(1);
        StartCoroutine(ProccessMap3.Instance.SetOff(id));
        AudioManagers.Ins.SetSfx(AudioManagers.Ins.LayMau, 1);
        Pref.SetDataRua(id + "");
        
        Pref.SetMayDap(IdMayDap + "");
    }
    public void FinishDap()
    {
        gameObject.tag = ConstName.None;
        Material[] mats = new Material[MeshRenderer.materials.Length];
        mats[0] = BangMau;
        mats[1] = Mats[1];
        MeshRenderer.materials = mats;
        SetAni(1);
        canGatScript.SetAni();
    }
}
