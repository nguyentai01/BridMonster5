using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadMenu : MonoBehaviour
{
    public GameObject Content;
    public GameObject StartGame;
    public GameObject LoadGame;
    public GameObject Setting;
    public GameObject Language;

    public GameObject Quit;

    public GameObject Setting1;
    public GameObject checkMusic;
    public GameObject checkSfx;
    public GameObject LanguageUi;

    public AudioSource mussic;
    public Slider Slider;
    public bool CheckShowOpenAds_Banner = false;

    public bool CheckMussic;
    AsyncOperation asyncLoad;
  
    private void Start()
    {
        StartCoroutine(ShowOpenAds());
        ManagerAds.ins.ShowBanner();
        
        AudioManagers.Ins.SetMusic(AudioManagers.Ins.BackGroudMenu,1);
        RunGame();
        asyncLoad = SceneManager.LoadSceneAsync(2);
        asyncLoad.allowSceneActivation = false;
        /* CheckMussic = Pref.GetMussic();*/
        if (!CheckMussic)
        {
  
            mussic.mute = true;
        }
        else
        {
            mussic.mute = false;
        }


        FireBaseSet.SetStatusInternet(""+CheckInternet.instance.checkInternet+"");
    }
    private IEnumerator ShowOpenAds()
    {
        yield return new WaitForSeconds(3);
        if (AdmobManager.ins.showAppOpen == 0)
        {
            AdmobManager.ins.ShowAppOpenAds();

            AdmobManager.ins.showAppOpen = 1;
        }
    }

    private void RunGame()
    {
        StartCoroutine(GameHome());
    }
    public void Btn_Start()
    {
        AudioManagers.Ins.SetSfx(AudioManagers.Ins.ButtonClick, 1);
       
        /* AudioManagers.ins.Sfx(AudioManagers.ins.Click, 1);*/
        Pref.SetPointSave(0);
        Pref.SetPointLoadChapter(0);
        Pref.ResetData();
        asyncLoad.allowSceneActivation = true;
        SceneManager.LoadScene(2);
    }
    public IEnumerator GameHome()
    {
        yield return new WaitForSeconds(0.1f);
        Content.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        StartGame.SetActive(true); yield return new WaitForSeconds(0.1f);
        LoadGame.SetActive(true); yield return new WaitForSeconds(0.1f);
        Setting.SetActive(true); yield return new WaitForSeconds(0.1f);
        Language.SetActive(true); yield return new WaitForSeconds(0.1f);
        Quit.SetActive(true);
    }
    private void HideStart()
    {
        Content.SetActive(false);
        StartGame.SetActive(false);
        LoadGame.SetActive(false);
        Setting.SetActive(false);
        Language.SetActive(false) ; 
        Quit.SetActive(false);
    }
    public void LoadChapters()
    {

        
        Pref.ResetData();
        AudioManagers.Ins.SetSfx(AudioManagers.Ins.ButtonClick, 1);
        asyncLoad.allowSceneActivation = true;
        SceneManager.LoadScene(2);
    }
    public void Btn_Setting()
    {
        /*AudioManagers.ins.Sfx(AudioManagers.ins.Click, 1);*/
        AudioManagers.Ins.SetSfx(AudioManagers.Ins.ButtonClick, 1);
#if UNITY_EDITOR
        Slider.value = ((Pref.GetSensi() - 10) / 40f);
        HideStart();
        Setting1.SetActive(true);
        if (Pref.GetMussic())
        {
            checkMusic.SetActive(true);
        }
        else
        {
            checkMusic.SetActive(false);
        }
        if (Pref.GetSfx())
        {
            checkSfx.SetActive(true);
        }
        else
        {
            checkSfx.SetActive(false);
        }
        return;
#endif
        Slider.value = ((Pref.GetSensi() - 5) / 4f);
        HideStart();
        Setting1.SetActive(true);
        if (Pref.GetMussic())
        {
            checkMusic.SetActive(true);
        }
        else
        {
            checkMusic.SetActive(false);
        }
        if (Pref.GetSfx())
        {
            checkSfx.SetActive(true);
        }
        else
        {
            checkSfx.SetActive(false);
        }
    }
    public void ClosedSetting()
    {
        AudioManagers.Ins.SetSfx(AudioManagers.Ins.ButtonClick, 1);
        StartCoroutine(GameHome());
        Setting1.SetActive(false);
#if UNITY_EDITOR
        Pref.SetSensi((float)Slider.value * 40 + 10);
        return;
#endif

        Debug.Log("");
        Pref.SetSensi((float)Slider.value * 4 + 5);
    }
    public void HideChapters()
    {
                    
    }
    public void QuitGame()
    {
        
        AudioManagers.Ins.SetSfx(AudioManagers.Ins.ButtonClick, 1);
        Application.Quit();
    }
    public void Btn_SetMussic()
    {
        AudioManagers.Ins.SetSfx(AudioManagers.Ins.ButtonClick, 1);

        if (Pref.GetMussic())
        {
            //false
            AudioManagers.Ins.SetMusicSfx(false, Pref.GetSfx());
            Pref.SetMussic(false);
            checkMusic.SetActive(false);
            CheckMussic = false;
            mussic.mute = true;
        }
        else
        {
            AudioManagers.Ins.SetMusicSfx(true, Pref.GetSfx());
            Pref.SetMussic(true);
            checkMusic.SetActive(true);
            CheckMussic = true;
            mussic.mute = false;
        }
    }
    public void Btn_SetSound()
    {
        AudioManagers.Ins.SetSfx(AudioManagers.Ins.ButtonClick, 1);

        if (Pref.GetSfx())
        {
            //false
            AudioManagers.Ins.SetMusicSfx(Pref.GetMussic(), false);
            Pref.SetSfx(false);
            checkSfx.SetActive(false);
        }
        else
        {
            AudioManagers.Ins.SetMusicSfx(Pref.GetMussic(), true);
            Pref.SetSfx(true);
            checkSfx.SetActive(true);
        }
    }
    public void langugae(bool status)
    {
        AudioManagers.Ins.SetSfx(AudioManagers.Ins.ButtonClick, 1);
        LanguageUi.SetActive(status);
        if (!status)
        {
            StartCoroutine(GameHome());
            return;
        }
        HideStart();
       
    }
}
