using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class RunInter : MonoBehaviour
{
    public static RunInter ins { get; private set; }
    public bool CheckRunInter = false;
    public bool CheckFirst = false;
    public bool checkRun = true;
    public float timeRun;

    private bool CheckShowAds = false;
    private bool CheckShowPopUp = true;
    public float timeRemote = 10;
    public GameObject ShowInter;
    public Text TimRunTxt;
    private Dictionary<string, Action<float>> actions;
    private void Awake()
    {
        ins = this;
    }
    private void Start()
    {
        checkRun = true;
        actions = new Dictionary<string, Action<float>>();
        actions["English"] = FncEnglish;
        actions["Vietnamese"] = FncVietnamese;
        actions["Spanish"] = FncSpanish;
        actions["Portuguese"] = FncPortuguese;
        actions["Indonesian"] = FncIndonesian;
        actions["Russian"] = FncRussian;
        
        timeRemote = ManagerAds.ins.timeShowAds;
        CheckShowPopUp = /*ManagerAds.ins.IsCheckShowPopUp*/true;
        if (CheckShowPopUp)
        {
            timeRun = 0;
            return;
        }
        timeRun = timeRemote;
    }
    private void Update()
    {
        if (CheckFirst && CheckShowPopUp)
        {
            if (timeRun > 0)
            {
                timeRun -= Time.deltaTime;
            }
        }
        if (!CheckShowPopUp && !CheckShowAds && checkRun)
        {
            if (timeRun > 0)
            {
                timeRun -= Time.deltaTime;
            }
            else
            {
                StartCoroutine(ShowInterAds(3));
                CheckShowAds = true;
                ShowInter.SetActive(true);
            }
        }
    }


    public void RunInterIn()
    {
        CheckFirst = true;
        if (CheckFirst && timeRun <= 0)
        {
            ManagerAds.ins.ShowInterstitial();
            timeRun = ManagerAds.ins.timeShowAds;
        }
    }

    private IEnumerator ShowInterAds(int time)
    {
        string lang = Pref.GetLanguage();
        if (actions.ContainsKey(lang))
        {
            actions[lang].Invoke(time);
        }
        yield return new WaitForSeconds(1);
        if (time <= 0)
        {
            
            ManagerAds.ins.ShowInterstitial();
            ShowInter.SetActive(false);
            CheckShowAds = false;
            timeRun = timeRemote;
            yield break;
        }
        if (!checkRun)
        {
            ShowInter.SetActive(false);
            CheckShowAds = false;
            checkRun = true;
            timeRun = timeRemote;
            yield break;
        }
        time--;
        StartCoroutine(ShowInterAds(time));


    }
    private void FncEnglish(float time)
    {
        TimRunTxt.text = "ADS IN " + time;
    }
    private void FncVietnamese(float time)
    {
        TimRunTxt.text = "QUẢNG CÁO TRONG " + time;
    }
    private void FncSpanish(float time)
    {
        TimRunTxt.text = "ANUNCIOS EN " + time;
    }
    private void FncPortuguese(float time)
    {
        TimRunTxt.text = "ANÚNCIOS EM " + time;
    }
    private void FncIndonesian(float time)
    {
        TimRunTxt.text = "IKLAN DI " + time;
    }
    private void FncRussian(float time)
    {
        TimRunTxt.text = "РЕКЛАМА В " + time;

    }
}
