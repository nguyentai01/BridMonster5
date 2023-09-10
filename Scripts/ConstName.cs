using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ConstName
{
    //Tag

    public const string ItemTag = "item";
    public const string DroneMove = "DroneMove";
    public const string DroneMove2 = "DroneOpen2";

    public const string SetCamera = "SetCamera";
    public const string Sheriff = "Sheriff";
    public const string Door = "Door";
    public const string Player_Tag = "Player_Tag";
    public const string SetToadSter = "SetToadSter";
    public const string DungCu = "DungCu";
    public const string BoxDrone = "BoxDrone";
    public const string PointSave = "PointSave";
    public const string BiaDan = "BiaDan";
    public const string GameOver = "GameOver";
    public const string RaoChan = "RaoChan";
    public const string SetPlayer = "SetPlayer";
    public const string TruMove = "TruMove";
    public const string SetBuff = "SetBuff";
    public const string Buff_Low = "Buff_low";
    public const string GameWin = "GameWin";
    public const string DroneOpen2 = "DroneOpen2";
    public const string Drone = "Drone";
    public const string Ballon = "Ballon";
    public const string Rac = "Rac";

    public const string None = "none";

    //Layer
    public const string NullBox = "NullBox";

    public const string Player = "Player";

    //EvenetUi

    public const string Controller = "Controller";

    //PopUp
    public static string[] CheckWatchAdsPopUp = { "X2Speed","GetAllPin", "GetAllCong", "GetX2Dan", "GetX1BulletNow", "GetHoSoNow", "GetX1HandCuffs","ShootX2","Get1Bullet", "Get1Book", "GetX2Book","SortAutoX2Book","RaisePillar",/*13*/ "X2SpeedEle" };
    //pref
    public const string Sensitivity = "Sensitivity";
    public const string Music = "Music";
    public const string Sound = "Sound";
    public const string PointLoadChapter = "PointLoadChapter";
    public const string CheckSave = "CheckSave";

    public const string KeyDoor = "KeyDoor";
    public const string KeyTu = "KeyTu";
    public const string HuyHieu = "HuyHieu";
    public const string HoSo = "HoSo";
    public const string Sung = "Sung";
    public const string Dan = "Dan";
    public const string Congtay = "Congtay";

    public const string CheckLanguage = "CheckLanguage";
    public const string Language = "Language";
    public const string CheckFirstLan = "CheckFirstLan";
    public const string CheckFirstGet = "CheckFirstGet";
    public const string StartGameCheck = "StartGameCheck_";
    public const string CountPopup = "CountPopup";
    public const string DataBook = "DataBook";
    public const string CheckShowRate = "CheckShowRate";
    public const string CheckShowRateFirst = "CheckShowRateFirst";
    public const string DataManhRua = "DataManhRua";
    public const string DataMayDap = "DataMayDap";
    public const string DataDoorMap3 = "DataDoorMap3";
    public const string DataTronVuong = "DataTronVuong";
    public const string DataKeyCardMap3 = "DataKeyCardMap3";

    //Ani
    public const string Status = "status";


    //NamePopUp

    public static string[] Name_Popups = { "GET_PIN1", "GET_PIN2", "GET_DIEUKHIEN", "GET_1HANDCUFF", "GET_1BULLET", "GET_1KEYCARD", "THE_GUN", "THE_BULLET", "THE_HUNDCUFFS", "THE_CRIMINAL_RECORD", "THE_POLICE_BADGE"
    ,"FULL_EQUIPMENT","LACKE_OF_EQUIPMENT","WHAT_IS_YOURKEYCARD","BRID_MONSTER_ASSISTANT","DOUBLE_SHOOT","NO_MORE_BULLET","GET1BOOK","PUTALL","ONECOLOR","DONT_LET","EXIST","DONT_MAKE","JUMP_JUMP"};
 /*   public static string[] Name_CLICK_REWARD = { "X2_SPEED","GET_ALL_PIN","GET_ALL_HANDCUFFS","GET_X2BULLET", "GET_X2BULLET","GET_HOSO_NOW","GET_X1HANDCUFF_NOW","SHOOTX2","GET_1BULLET","GET_1BOOK","GET_X2BOOK","AUTO_SORTX2","RAISE_PILLARX2" };*/
    //FireBase

    public const string OpenDoor = "OpenDoor_";
    public const string StartGame = "StartGame_";
    public const string PlayGame = "PlayGame_";
    public const string ShowPopUp = "CountPopUpShow_";
    public const string CheckInterNet = "CheckInterNet_";
    public const string CheckRivive = "CheckRivive_";
    public const string pickup = "pickup_";
    public const string PICKUP_BULLET_ = "PICKUP_BULLET_";
    public const string click_button_door = "click_button_Door_Drone_";
    public const string click_button_keycard = "click_button_Door_keycard";
    public const string WinGame = "WinGame_";
    public const string LosedGame = "LosedGame_";
    public const string Check_SpeedUp_= "Check_SpeedUp_";
    public const string RATE_US = "RATE_US_";
    public const string SHOWPOPUPREWARD = "SHOW_POPUP_REWARD_";
    public const string CLICK_BUTTON_REWARD_POPUP = "CLICK_BUTTON_REWARD_POPUP_";


    //items

    public const string ControllerItem = "Controller";
    public const string Pinitems = "Pin";
    public const string GunItem = "Gun";
    public const string CongItem = "Cong8";
    public const string HoSoItem = "HoSo";
    public const string HuyHieuItem = "HuyHieu";
    public const string DanItem = "Bullet";
    public const string CardItem = "Card";

    //LanguageText

    public const string English = "English";
    public const string Vietnamese = "Vietnamese";
    public const string Spanish = "Spanish";
    public const string Portuguese = "Portuguese";
    public const string Indonesian = "Indonesian";
    public const string Russian = "Russian";

    //Language

    private static readonly Dictionary<SystemLanguage, string> COUTRY_CODES = new Dictionary<SystemLanguage, string>()
        {
            { SystemLanguage.Afrikaans, "Afrikaans" },
            { SystemLanguage.Arabic    , "Arabic" },
            { SystemLanguage.Basque    , "English" },
            { SystemLanguage.Belarusian    , "Belarusian" },
            { SystemLanguage.Bulgarian    , "Bulgarian" },
            { SystemLanguage.Catalan    , "Catalan" },
            { SystemLanguage.Chinese    , "Chinese" },
            { SystemLanguage.Czech    , "Czech" },
            { SystemLanguage.Danish    , "Danish" },
            { SystemLanguage.Dutch    , "Dutch" },
            { SystemLanguage.English    , "English" },
            { SystemLanguage.Estonian    , "Estonian" },
            { SystemLanguage.Faroese    , "Faroese" },
            { SystemLanguage.Finnish    , "Finnish" },
            { SystemLanguage.French    , "French" },
            { SystemLanguage.German    , "German" },
            { SystemLanguage.Greek    , "Greek" },
            { SystemLanguage.Hebrew    , "Hebrew" },
            { SystemLanguage.Icelandic    , "Icelandic" },
            { SystemLanguage.Indonesian    , "Indonesian" },
            { SystemLanguage.Italian    , "Italian" },
            { SystemLanguage.Japanese    , "Japanese" },
            { SystemLanguage.Korean    , "Korean" },
            { SystemLanguage.Latvian    , "Latvian" },
            { SystemLanguage.Lithuanian    , "Lithuanian" },
            { SystemLanguage.Norwegian    , "Norwegian" },
            { SystemLanguage.Polish    , "Polish" },
            { SystemLanguage.Portuguese    , "Portuguese" },
            { SystemLanguage.Romanian    , "Romanian" },
            { SystemLanguage.Russian    , "Russian" },
            { SystemLanguage.SerboCroatian    , "SerboCroatian" },
            { SystemLanguage.Slovak    , "Slovak" },
            { SystemLanguage.Slovenian    , "Slovenian" },
            { SystemLanguage.Spanish    , "Spanish" },
            { SystemLanguage.Swedish    , "Swedish" },
            { SystemLanguage.Thai    , "Thai" },
            { SystemLanguage.Turkish    , "Turkish" },
            { SystemLanguage.Ukrainian    , "Ukrainian" },
            { SystemLanguage.Vietnamese    , "Vietnamese" },
            { SystemLanguage.ChineseSimplified    , "Chinese" },
            { SystemLanguage.ChineseTraditional    , "Chinese" },
            { SystemLanguage.Unknown    , "English" },
            { SystemLanguage.Hungarian    , "Hungarian" },
        };

    /// <summary>
    /// Returns approximate country code of the language.
    /// </summary>
    /// <returns>Approximated country code.</returns>
    /// <param name="language">Language which should be converted to country code.</param>
    public static string ToCountryCode(this SystemLanguage language)
    {
        string result;
        if (COUTRY_CODES.TryGetValue(language, out result))
        {
            return result;
        }
        else
        {
            return COUTRY_CODES[SystemLanguage.Unknown];
        }
    }
}
