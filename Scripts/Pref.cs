
using System.Collections.Generic;
using UnityEngine;

public static class Pref
{
    public static void SetSensi(float sen)
    {
        PlayerPrefs.SetFloat(ConstName.Sensitivity, sen);

    }
    public static float GetSensi()
    {
#if UNITY_EDITOR
        return PlayerPrefs.GetFloat(ConstName.Sensitivity, 30);

#endif
        return PlayerPrefs.GetFloat(ConstName.Sensitivity, 7);

    }
    public static void SetMussic(bool Music)
    {
        PlayerPrefs.SetInt(ConstName.Music, Music ? 1 : 0);
    }
    public static bool GetMussic()
    {
        return PlayerPrefs.GetInt(ConstName.Music, 1) == 1;
    }
    public static void SetSfx(bool Sfx)
    {
        PlayerPrefs.SetInt(ConstName.Sound, Sfx ? 1 : 0);
    }
    public static bool GetSfx()
    {
        return PlayerPrefs.GetInt(ConstName.Sound, 1) == 1;
    }
    public static void SetPointSave(int point)
    {
        PlayerPrefs.SetInt(ConstName.PointSave, point);
    }
    public static int GetPointSave()
    {
        return PlayerPrefs.GetInt(ConstName.PointSave, 0);
    }
    public static void SetPointLoadChapter(int point)
    {

        PlayerPrefs.SetInt(ConstName.PointLoadChapter, point);
    }
    public static int GetPointLoadChapter()
    {
        return PlayerPrefs.GetInt(ConstName.PointLoadChapter, 0);
    }
    public static void SetCheckSave(int point)
    {
        PlayerPrefs.SetInt(ConstName.CheckSave, point);
    }
    public static int GetCheckSave()
    {
        return PlayerPrefs.GetInt(ConstName.CheckSave, 0);
    }
    public static void SetData(int id, int index)
    {
        switch (id)
        {
            case 0:
                PlayerPrefs.SetInt(ConstName.Congtay, PlayerPrefs.GetInt(ConstName.Congtay, 0) + index);
                break;
            case 1:
                PlayerPrefs.SetInt(ConstName.Sung, PlayerPrefs.GetInt(ConstName.Sung, 0) + index); break;
            case 2:
                PlayerPrefs.SetInt(ConstName.Dan, PlayerPrefs.GetInt(ConstName.Dan, 0) + index); break;
            case 3:
                PlayerPrefs.SetInt(ConstName.HuyHieu, PlayerPrefs.GetInt(ConstName.HuyHieu, 0) + index); break;
            case 4:
                PlayerPrefs.SetInt(ConstName.HoSo, PlayerPrefs.GetInt(ConstName.HoSo, 0) + index); break;
            case 5:
                PlayerPrefs.SetInt(ConstName.KeyTu, PlayerPrefs.GetInt(ConstName.KeyTu, 0) + index); break;
            case 6:
                PlayerPrefs.SetInt(ConstName.KeyDoor, PlayerPrefs.GetInt(ConstName.KeyDoor, 0) + index); break;
            default: break;
        }
    }
    public static bool GetData(out int[] data)
    {
        data = new int[5] { GetData(0), GetData(1), GetData(2), GetData(3), GetData(4) };
        return true;
    }
    public static int GetData(int id)
    {
        switch (id)
        {
            case 0:
                return PlayerPrefs.GetInt(ConstName.Congtay, 0);
            case 1:
                return PlayerPrefs.GetInt(ConstName.Sung, 0);
            case 2:
                return PlayerPrefs.GetInt(ConstName.Dan, 0);
            case 3:
                return PlayerPrefs.GetInt(ConstName.HuyHieu, 0);
            case 4:
                return PlayerPrefs.GetInt(ConstName.HoSo, 0);
            case 5:
                return PlayerPrefs.GetInt(ConstName.KeyTu, 0);
            case 6:
                return PlayerPrefs.GetInt(ConstName.KeyDoor, 0);
            default: return 0;
        }
    }
    public static void ResetData()
    {
        PlayerPrefs.SetInt(ConstName.Congtay, 0);
        PlayerPrefs.SetInt(ConstName.HuyHieu, 0);
        PlayerPrefs.SetInt(ConstName.Sung, 0);
        PlayerPrefs.SetInt(ConstName.HoSo, 0);
        PlayerPrefs.SetInt(ConstName.Dan, 0);
        PlayerPrefs.SetInt(ConstName.KeyTu, 0);
        PlayerPrefs.SetInt(ConstName.KeyDoor, 0);
        SetCheckSave(0);
        ResetDataBook();
        ResetDataRua();
    }
    public static void SetLanguage()
    {
        PlayerPrefs.GetInt(ConstName.CheckLanguage, 0);
    }
    public static int SAVE_Languages()
    {
        return PlayerPrefs.GetInt(ConstName.CheckLanguage, 0);
    }
    public static void SetLanguage(string lan)
    {
        PlayerPrefs.SetString(ConstName.Language, lan);
    }
    public static string GetLanguage()
    {
        string s = Application.systemLanguage.ToCountryCode();
        HashSet<string> set = new HashSet<string>() { "English", "Vietnamese", "Russian", "Spanish", "Portuguese", "Indonesian" };
        if (!set.Contains(s))
        {
            s = "English";
        }
        return PlayerPrefs.GetString(ConstName.Language, s);
    }
    public static void SetFirstGet(int index)
    {
        PlayerPrefs.SetInt(ConstName.CheckFirstGet, index);
    }
    public static int GetFirstGet()
    {
        return PlayerPrefs.GetInt(ConstName.CheckFirstGet, 0);
    }
    public static void SetStartGame(int id)
    {
        PlayerPrefs.SetInt(ConstName.StartGameCheck + id, 1);
    }
    public static int GetStartGame(int id)
    {
        return PlayerPrefs.GetInt(ConstName.StartGameCheck + id, 0);
    }
    public static void SetCountPopup()
    {
        int Count = GetCountPopup();

        PlayerPrefs.SetInt(ConstName.CountPopup, Count + 1);
        PlayerPrefs.Save();
        if (Count + 1 <= 10)
        {
            FireBaseSet.SetPopup(Count + 1);

        }

    }
    public static int GetCountPopup()
    {
        return PlayerPrefs.GetInt(ConstName.CountPopup, 0);
    }
    public static void SetCountPopup(int index)
    {
        PlayerPrefs.SetInt(ConstName.CountPopup, index);
        PlayerPrefs.Save();

    }
    public static void ResetDataBook()
    {
        PlayerPrefs.SetString(ConstName.DataBook, "");
    }
    public static void SetDataBook(string s)
    {

        PlayerPrefs.SetString(ConstName.DataBook, GetDataBook(0) + s + ",");
    }
    public static string[] GetDataBook()
    {
        if (GetDataBook(0).Equals(""))
        {

            return null;
        }

        return GetDataBook(0).Split(',');
    }
    public static string GetDataBook(int i)
    {
        return PlayerPrefs.GetString(ConstName.DataBook, null);
    }
    public static void SetRate(int i)
    {
        PlayerPrefs.SetInt(ConstName.CheckShowRate, i);
    }
    public static int GetRate()
    {
        return PlayerPrefs.GetInt(ConstName.CheckShowRate, 0);
    }
    public static void SetShowRate(int i)
    {
        PlayerPrefs.SetInt(ConstName.CheckShowRateFirst, i);
    }
    public static int GetShowRate()
    {
        return PlayerPrefs.GetInt(ConstName.CheckShowRateFirst, 0);
    }

    public static void ResetDataRua()
    {
        PlayerPrefs.SetString(ConstName.DataManhRua, "");
        PlayerPrefs.SetString(ConstName.DataMayDap, "");
        PlayerPrefs.SetString(ConstName.DataDoorMap3, "");
        PlayerPrefs.SetString(ConstName.DataTronVuong, "");
        PlayerPrefs.SetString(ConstName.DataKeyCardMap3, "");

    }
    public static void SetDataRua(string s)
    {

        PlayerPrefs.SetString(ConstName.DataManhRua, GetDataRua(0) + s + ",");
    }
    public static string[] GetDataRua()
    {
        if (GetDataRua(0).Equals(""))
        {

            return null;
        }

        return GetDataRua(0).Split(',');
    }
    public static string GetDataRua(int i)
    {
        return PlayerPrefs.GetString(ConstName.DataManhRua, null);
    }


    public static void SetMayDap(string s)
    {

        PlayerPrefs.SetString(ConstName.DataMayDap, GetDataMayDap(0) + s + ",");
    }
    public static string[] GetDataMayDap()
    {
        if (GetDataMayDap(0).Equals(""))
        {

            return null;
        }

        return GetDataMayDap(0).Split(',');
    }
    public static string GetDataMayDap(int i)
    {
        return PlayerPrefs.GetString(ConstName.DataMayDap, null);
    }


    public static void SetDataDoorMap3(string s)
    {

        PlayerPrefs.SetString(ConstName.DataDoorMap3, GetDataDoorMap3(0) + s + ",");
    }
    public static string[] GetDataDoorMap3()
    {
        if (GetDataDoorMap3(0).Equals(""))
        {

            return null;
        }

        return GetDataDoorMap3(0).Split(',');
    }
    public static string GetDataDoorMap3(int i)
    {
        return PlayerPrefs.GetString(ConstName.DataDoorMap3, null);
    }

    public static void SetDataTronVuong(string s)
    {

        PlayerPrefs.SetString(ConstName.DataTronVuong, GetDataTronVuong(0) + s + ",");
    }
    public static string[] GetDataTronVuong()
    {
        if (GetDataTronVuong(0).Equals(""))
        {

            return null;
        }

        return GetDataTronVuong(0).Split(',');
    }
    public static string GetDataTronVuong(int i)
    {
        return PlayerPrefs.GetString(ConstName.DataTronVuong, null);
    }

    public static void SetDataKeyCard(int[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == 1)
            {
                PlayerPrefs.SetString(ConstName.DataKeyCardMap3, GetDataKeyCard(0) + i + ",");
            }
        }
        
    }
    public static string[] GetDataKeyCard()
    {
        if (GetDataKeyCard(0).Equals(""))
        {
            return null;
        }
        
        return GetDataKeyCard(0).Split(',');
    }
    public static string GetDataKeyCard(int i)
    {
        return PlayerPrefs.GetString(ConstName.DataKeyCardMap3, null);
    }
}
