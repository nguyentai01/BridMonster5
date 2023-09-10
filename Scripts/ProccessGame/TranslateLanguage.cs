using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using I2.Loc;

public class TranslateLanguage : MonoBehaviour
{
    
    public string[] language = { "Afrikaans","Arabic","Basque","Belarusian","Bulgarian","Catalan","Chinese","Czech","Danish","Dutch","English",
                                "Estonian","Faroese","Finnish","French","German","Greek","Hebrew","Icelandic","Indonesian","Italian","Japanese",
                                "Korean","Latvian","Lithuanian","Norwegian","Polish","Portuguese","Romanian","Russian","Serbo-Croatian","Slovak","Slovenian",
                                "Spanish","Swedish","Thai","Turkish","Ukrainian","Vietnamese","ChineseSimplified","ChineseTraditional","Hindi","Unknown", "Hungarian" };
    private void OnEnable()
    {
        Change();
    }
    private void Start()
    {
        Debug.Log("Name" +gameObject.name);
    }
    public void Change()
    {
        var countryCode = Pref.GetLanguage();
        bool checkName = false;
        foreach (string cName in language)
        {
            if (countryCode == cName)
            {
                checkName = true;
            }
        }
        if (!checkName)
        {
            countryCode = "English";
        }
        if (LocalizationManager.HasLanguage(countryCode))
        {
            LocalizationManager.CurrentLanguage = countryCode;
        }
    }
    
}
