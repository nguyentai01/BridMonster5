using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class PopUpmanager : MonoBehaviour
{
    public GameObject Container;
    public BulletFire BulletFires;
    public string WatchAdsPopUp;
    public GameObject[] PopUps;
    public bool pin = false;
    public int idPopup;
    private Dictionary<string, System.Action> actions;
    public static PopUpmanager Instance;
   /* public Text Test;*/
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {

        actions = new Dictionary<string, System.Action>();
        actions[ConstName.CheckWatchAdsPopUp[0]] = X2Speed;
        actions[ConstName.CheckWatchAdsPopUp[1]] = GetAllPin;
        actions[ConstName.CheckWatchAdsPopUp[2]] = GetAllCong;

        actions[ConstName.CheckWatchAdsPopUp[3]] = GetX2Dan;
        actions[ConstName.CheckWatchAdsPopUp[4]] = GetX2Dan;
        actions[ConstName.CheckWatchAdsPopUp[5]] = GetHoSoNow;
        actions[ConstName.CheckWatchAdsPopUp[6]] = GetX1HandCuffNow;
        actions[ConstName.CheckWatchAdsPopUp[7]] = ShootX2;
        actions[ConstName.CheckWatchAdsPopUp[8]] = Get1Bullet;
        actions[ConstName.CheckWatchAdsPopUp[9]] = Get1Book;

        actions[ConstName.CheckWatchAdsPopUp[10]] = GetX2Book;
        actions[ConstName.CheckWatchAdsPopUp[11]] = AutoSortX2;
        actions[ConstName.CheckWatchAdsPopUp[12]] = RaisePillarX2;
        actions[ConstName.CheckWatchAdsPopUp[13]] = X2SpeedELevator;
    }
    public void WatchAds()
    {
        ClosedPopup();
        if (actions.ContainsKey(WatchAdsPopUp))
        {
            actions[WatchAdsPopUp].Invoke();
            FireBaseSet.SetPopUpReward(WatchAdsPopUp);
            WatchAdsPopUp = null;
            UiManager.ins.SetTest("POPUP2");
        }
    }
    public void SetPopUp(int id, string popup)
    {
        /*Test.text = "" + popup;*/
        idPopup = id;
        WatchAdsPopUp = null;
        WatchAdsPopUp = popup;
        Container.SetActive(true);
        PopUps[id].gameObject.SetActive(true);
        Pref.SetCountPopup();
        FireBaseSet.SetPopUpShow(id);
    }
    public void SetPopUp()
    {
        WatchAdsPopUp = null;
    }
    private void X2Speed()
    {
        EventUi.instance.Get_X2_Speed();
    }
    public void ClosedPopup()
    {
        PopUps[idPopup].gameObject.SetActive(false);
        Container.SetActive(false);
    }
    private void GetAllPin()
    {
        GameController.ins.SetPinAll();
    }
    private void GetAllCong()
    {
        GameController.ins.SetAll(0, 3);
        MapController.Ins.Map1.GetAllCong8();
    }
    public void GetX2Dan()
    {
        Pref.SetData(2, 1);
        GameController.ins.SetX2Dan();
        FireBaseSet.SetItem(Pref.GetData(2));
    }
    private void GetHoSoNow()
    {
        EventUi.instance.SetKey(5);
        GameController.ins.SetData(4);
        MapController.Ins.Map1.GetHoSo();
    }
    private void GetX1HandCuffNow()
    {
        EventUi.instance.SetKey(1);
        GameController.ins.SetData(0);
        MapController.Ins.Map1.GetX1Cong8();
        Pref.SetData(0, 1);
    }
    private void ShootX2()
    {
        BulletFire b = Instantiate(BulletFires,GameController.ins.ProccessPlayer.transform.position,Quaternion.Euler(0,-90,0));
        BiaDan bd = MapController.Ins.Map1.GetBiaDan();
        if(bd != null)
        {
            bd.IsFire = true;
            b.FireBullet(bd.transform.position);
            GameController.ins.SetFire();
        }
        EventUi.instance.SetX2Shoot();

    }
    private void Get1Bullet()
    {
        GameController.ins.data[2]++;
        UiManager.ins.SetDataBullet(GameController.ins.data[2] - EventUi.instance.IdBullet);
    }
    private void Get1Book()
    {
        MapController.Ins.Map2.Get1Book(GameController.ins.Sach);
    }
    private void GetX2Book()
    {
        MapController.Ins.Map2.GetX2Book();
    }
    private void AutoSortX2()
    {
        MapController.Ins.Map2.AutoSortX2();
    }
    private void RaisePillarX2()
    {
        MapController.Ins.Map2.MovePillars();
    }
    private void X2SpeedELevator()
    {
        StartCoroutine(MapController.Ins.Map3.Elevator.SetSpeed());
    }
}
