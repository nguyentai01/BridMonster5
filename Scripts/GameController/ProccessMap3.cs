using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Rendering;

public class ProccessMap3 : MonoBehaviour
{
    public static ProccessMap3 Instance;

    private bool IsGet = false;
    public int[] ManhRuas = new int[6];
    public bool IsRua = false;
    public int id = -1;
    public GameObject[] ManhRuasObject;
    public int[] TronVuong = new int[5];
    private string[] s;
    private string[] sMayDap;
    public MayDap[] Maydaps;
    public GameObject[] TronVuongObject;
    public Light[] Lights;
    public GameObject LightMain;
    public Color[] colorsEnv;

    public Transform Tru;
    public AudioSource aus;
    public GameObject Water;
    private void Awake()
    {
        Instance = this;
    }

    public bool SetData(int index)
    {

        if (IsGet)
        {
            return false;
        }
        id = index;
        IsRua = true;
        IsGet = true;
        UiManager.ins.GetManhRua(index, true);
        ManhRuas[index]++;
        return true;
    }
    public bool GetRua()
    {
        if (IsRua)
        {
            IsRua = false;
            IsGet = false;
            return true;
        }
        else
        {
            return false;
        }
    }
    public void GetManhRua(int id, Transform parent)
    {
        ManhRuasObject[id].transform.parent = parent;
        ManhRuasObject[id].transform.localPosition = Vector3.zero;
        ManhRuasObject[id].SetActive(true);
    }
    public IEnumerator SetOff(int id)
    {
        yield return new WaitForSeconds(0.5f);
        ManhRuasObject[id].SetActive(false);
    }
    public void SetTronVuong(int index)
    {

        TronVuong[index]++;
        TronVuongObject[index].SetActive(false);
        UiManager.ins.GetTronVuong();
    }
    public bool GetTronVuong(int index)
    {
        return TronVuong[index] == 1;
    }
    public void GetDataRua()
    {
        s = Pref.GetDataRua();
        if (s != null)
        {
            if (s.Length <= 1)
            {
                return;
            }
            for (int i = 0; i < s.Length - 1; i++)
            {
                ManhRuas[Int32.Parse(s[i])]++;
            }
            MapController.Ins.Map3.SetManhRua(ManhRuas);
        }
        GetDataMayDap();
        GetDataTronVuong();
    }
    public void GetDataMayDap()
    {
        sMayDap = Pref.GetDataMayDap();
        if (sMayDap != null)
        {
            if (sMayDap.Length <= 1)
            {
                return;
            }
            for (int i = 0; i < sMayDap.Length - 1; i++)
            {
                Maydaps[Int32.Parse(sMayDap[i])].FinishDap();
            }
            MapController.Ins.Map3.WaterHeal.index = sMayDap.Length - 1;
        }

    }
    public void GetDataTronVuong()
    {
        String[] DatatronVuong = Pref.GetDataTronVuong();
        if (DatatronVuong != null)
        {
            if (DatatronVuong.Length <= 1)
            {
                return;
            }
            for (int i = 0; i < DatatronVuong.Length - 1; i++)
            {
                SetTronVuong(Int32.Parse(DatatronVuong[i]));

            }
        }
    }
    public void SetStartRua()
    {
        StartCoroutine(SetLightWarring());
    }
    private IEnumerator SetLightWarring()
    {
        yield return new WaitForSeconds(0.8f);
        Rung();
        yield return new WaitForSeconds(0.8f);
        
        Lights[0].enabled = false;
        Lights[1].enabled = false;
        LightMain.SetActive(false);

        RenderSettings.ambientMode = AmbientMode.Trilight;
        RenderSettings.ambientSkyColor = colorsEnv[0];
        RenderSettings.ambientEquatorColor = colorsEnv[0];
        RenderSettings.ambientGroundColor = colorsEnv[0];
        //No
        yield return new WaitForSeconds(0.8f);
        aus.clip = AudioManagers.Ins.Warring;
        aus.Play();
        RenderSettings.ambientMode = AmbientMode.Flat;
        RenderSettings.ambientLight = colorsEnv[1];
        Lights[2].enabled = true;
        Lights[3].enabled = true;
        Lights[2].DOIntensity(0, 0.5f).SetLoops(-1, LoopType.Yoyo);
        Lights[3].DOIntensity(0, 0.5f).SetLoops(-1, LoopType.Yoyo);
        yield return new WaitForSeconds(0.8f);
        Water.SetActive(false);
        EffectsManager.instance.SetEffectNo(Tru);
        MapController.Ins.Map3.ConRua.SetActive(true);
        
    }
    public void Rung()
    {
        Tru.DOShakePosition(1f, 0.02f).SetEase(Ease.InOutSine);
    }
    public void SetAudio()
    {
        aus.Stop();
    }
}
