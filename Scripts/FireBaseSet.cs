using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FireBaseSet
{
    public static void SetFireBaseOpenDoor(int id)
    {
        Firebase.Analytics.FirebaseAnalytics.LogEvent(ConstName.OpenDoor + id);
    }
    public static void SetStartGame(int id)
    {
        if (Pref.GetStartGame(id) == 0)
        {
            Firebase.Analytics.FirebaseAnalytics.LogEvent(ConstName.StartGame + id);
            Pref.SetStartGame(id);
        }
    }
    public static void SetPlayGame(string id)
    {
        Firebase.Analytics.FirebaseAnalytics.LogEvent(ConstName.PlayGame + id);

    }
    public static void SetPopup(int id)
    {
        Firebase.Analytics.FirebaseAnalytics.LogEvent(ConstName.ShowPopUp + id);
    }
    public static void SetStatusInternet(string status)
    {
        Firebase.Analytics.FirebaseAnalytics.LogEvent(ConstName.CheckInterNet + status);
    }
    public static void SetRivive(string s)
    {
        switch (Pref.GetPointSave())
        {
            case 0:
            case 1:
            case 2:
                Firebase.Analytics.FirebaseAnalytics.LogEvent(s + "1");
                break;
        }

    }
    public static void SetItem(string item)
    {

        Firebase.Analytics.FirebaseAnalytics.LogEvent(ConstName.pickup + item);

    }
    public static void SetItem(int count)
    {

        Firebase.Analytics.FirebaseAnalytics.LogEvent(ConstName.PICKUP_BULLET_ + count);


    }
    public static void SetDroneOpen(int id)
    {

        Firebase.Analytics.FirebaseAnalytics.LogEvent(ConstName.click_button_door + id + "");
    }

    public static void SetFirebase(string s)
    {

        Firebase.Analytics.FirebaseAnalytics.LogEvent(s);
    }
    public static void SetRateLog(int rate)
    {

        Firebase.Analytics.FirebaseAnalytics.LogEvent(ConstName.RATE_US + rate + "_STAR");
    }
    public static void SetPopUpShow(int id)
    {
        if (id < ConstName.Name_Popups.Length)
        {
            Firebase.Analytics.FirebaseAnalytics.LogEvent(ConstName.SHOWPOPUPREWARD + "" + ConstName.Name_Popups[id]);

        }
    }
    public static void SetPopUpReward(string s)
    {
        Firebase.Analytics.FirebaseAnalytics.LogEvent(ConstName.CLICK_BUTTON_REWARD_POPUP + "" +s);


    }
}
