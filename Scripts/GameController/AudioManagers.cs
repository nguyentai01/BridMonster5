using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagers : MonoBehaviour
{
    public static AudioManagers Ins;
    public AudioSource SFX;
    public AudioSource Music;

    public AudioClip story1;
    public AudioClip story2;
    public AudioClip story3;
    public AudioClip story4;
    public AudioClip story5;
    public AudioClip story6;
    public AudioClip story7;
    public AudioClip story8;
    public AudioClip story9;
    public AudioClip story10;
    public AudioClip Jump;
    public AudioClip FootStep;
    public AudioClip PickItem;
    public AudioClip OpenDoor;
    public AudioClip ButtonClick;
    public AudioClip BtnOpen;
    public AudioClip Door_error;
    public AudioClip ChimKeu;
    public AudioClip ChimAttack;
    public AudioClip ChimWin;
    public AudioClip Shotting;
    public AudioClip BanTrung;
    public AudioClip LosedGame;
    public AudioClip ThangMay;
    public AudioClip WinMap;
    public AudioClip LosedMap;
    public AudioClip[] LamNhams;
    public AudioClip XanhRun;
    public AudioClip XanhAttack;
    public AudioClip CurrentBook;
    public AudioClip WrongBook;
    public AudioClip OpenTu;
    public AudioClip TrauHucCua;
    public AudioClip NangTru;
    public AudioClip TrauRong;
    public AudioClip HucTru;
    public AudioClip LayMau;
    public AudioClip BomNo;
    public AudioClip Kinhvo;
    public AudioClip Warring;
    public AudioClip TiengTho;
    public AudioClip TiengRong;
    public AudioClip QuaiTanCong;
    public AudioClip QuaiRun;
    public AudioClip GapLen;
    public AudioClip[] Controls;

    public AudioClip BackGroudMenu;
    public AudioClip BackGroudGame;


    private void Awake()
    {
        Ins = this;
        DontDestroyOnLoad(this);
    }
    private void Start()
    {
        SetMusicSfx(Pref.GetMussic(), Pref.GetSfx());
        
       
    }
    public IEnumerator SetSfx(AudioClip ac, float volue, float time)
    {
        yield return new WaitForSeconds(time);
        SFX.PlayOneShot(ac, volue);
    }
    public void SetSfx(AudioClip ac, float volue)
    {
        SFX.PlayOneShot(ac, volue);
    }
    public void SetSfx2(AudioClip ac, float volue)
    {
        
        SFX.clip = ac;
        SFX.volume = volue;
        SFX.Play();
    }
    public void SetMusic(AudioClip ac, float volue)
    {
        Music.clip = ac;
        Music.volume = volue;
        Music.Play();
    }
    public void SetRandomAudioSheriff()
    {
        int x = Random.Range(0, 2);
        AudioClip ac = x == 0 ? story6 : story7;
        SetSfx(ac, 1);
    }
    public void SetMusicSfx(bool music, bool sfx)
    {
        SFX.mute = !sfx;
        Music.mute = !music;

    }
    public AudioClip LamNhamAudio()
    {
        return LamNhams[Random.Range(0, 2)];
    }
    public AudioClip Controller()
    {
        return Controls[Random.Range(0, 2)];
    }
}
