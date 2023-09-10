using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguagesPopup : MonoBehaviour
{
    public Image[] imageLanguage;
    private int idLanguage;
    public TranslateLanguage translate;
    private void OnEnable()
    {
        idLanguage = PlayerPrefs.GetInt(ConstName.CheckLanguage, 0);
        if (PlayerPrefs.GetInt(ConstName.CheckFirstLan, 0) == 0)
        {
            PlayerPrefs.SetInt(ConstName.CheckFirstLan, 1);
            var lan = Application.systemLanguage.ToCountryCode();

            if (lan.Equals("English"))
            {
                idLanguage = 0;
            }
            else if (lan.Equals("Vietnamese"))
            {
                idLanguage = 1;
            }
            else if (lan.Equals("Russian"))
            {
                idLanguage = 2;
            }
            else if (lan.Equals("Spanish"))
            {
                idLanguage = 3;
            }
            else if (lan.Equals("Portuguese"))
            {
                idLanguage = 4;
            }
            else if (lan.Equals("Indonesian"))
            {
                idLanguage = 5;
            }
            else
            {
                idLanguage = 0;
            }

        }
        SetSaveLanguages(idLanguage);

        CheckLanguage();
        

       
    }
    public void CheckLanguage()
    {
        for(int i=0;i<imageLanguage.Length;i++)
        {
            imageLanguage[i].gameObject.SetActive(i== idLanguage);
        }
        
    }
    public void ClickLanguage(int index)
    {
        AudioManagers.Ins.SetSfx(AudioManagers.Ins.ButtonClick, 1);
        idLanguage = index;
        PlayerPrefs.SetInt(ConstName.CheckLanguage, idLanguage);
        PlayerPrefs.Save();

        SetSaveLanguages(idLanguage);
        translate.Change();
        
        for (int i = 0; i < imageLanguage.Length; i++)
        {
            
            imageLanguage[i].gameObject.SetActive(i == idLanguage);
        }

    }
    private void SetSaveLanguages(int id)
    {
        switch (id)
        {
            case 0:

                Pref.SetLanguage("English");
                break;
            case 1:

                Pref.SetLanguage("Vietnamese");
                break;
            case 2:

                Pref.SetLanguage("Russian");
                break;
            case 3:

                Pref.SetLanguage("Spanish");
                break;
            case 4:

                Pref.SetLanguage("Portuguese");
                break;
            case 5:

                Pref.SetLanguage("Indonesian");
                break;
        }
        PlayerPrefs.Save();
    }
}
