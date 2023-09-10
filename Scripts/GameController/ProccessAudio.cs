using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProccessAudio : MonoBehaviour
{
    public void OpenDoor()
    {
        AudioManagers.Ins.SetSfx(AudioManagers.Ins.OpenDoor, 1);
    }
    public void PlayerMove()
    {
        AudioManagers.Ins.SetSfx(AudioManagers.Ins.FootStep, 1);

    }
    public void Shotting()
    {
        AudioManagers.Ins.SetSfx(AudioManagers.Ins.Shotting, 1);

    }
    public void ThangMaydi()
    {
        AudioManagers.Ins.SetSfx(AudioManagers.Ins.ThangMay, 1);

    }
    public void OpenTu()
    {
        AudioManagers.Ins.SetSfx(AudioManagers.Ins.OpenTu, 1);
    }
    public void Run()
    {
        AudioManagers.Ins.SetSfx(AudioManagers.Ins.QuaiRun, 1);
    }
}
