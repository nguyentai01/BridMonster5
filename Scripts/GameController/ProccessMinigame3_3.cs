using DG.Tweening;
using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class ProccessMinigame3_3 : MonoBehaviour
{
    public Joystick joystick;
    public GameObject CongTac;
    public GameObject TayCam;
    public float speed = 10f;
    public float minCamAngleX;
    public float maxCamAngleX;
    public float minCamAngleZ;
    public float maxCamAngleZ;
    public bool IsMove = false;
    public Animator aniHandler;
    public GameObject Tutorial;
    public AudioSource aus;
    private bool IsRun = false;
    private void Start()
    {
        
    }
    private void Update()
    {
        if (IsMove)
        {
            CongTac.transform.rotation = Quaternion.Euler(new Vector3(-joystick.Horizontal * 40, 0, -(joystick.Vertical * 40)+14));
            TayCam.transform.position += new Vector3(joystick.Vertical * Time.deltaTime, 0, -joystick.Horizontal * Time.deltaTime) * speed;
            TayCam.transform.position = new Vector3(Mathf.Clamp(TayCam.transform.position.x, minCamAngleX, maxCamAngleX), TayCam.transform.position.y, Mathf.Clamp(TayCam.transform.position.z, minCamAngleZ, maxCamAngleZ));
            
            if (!IsRun)
            {
                IsRun = true;
                RunAudio(true);
            }
            if (joystick.Horizontal != 0 || joystick.Vertical != 0)
            {
                return;
            }
        }
        if (IsRun && joystick.Horizontal==0 && joystick.Vertical==0)
        {
            IsRun = false;
            RunAudio(false);

        }
    }

    public void RunAudio(bool status)
    {
        if (!Pref.GetSfx())
        {
            return;
        }
        if (status)
        {
            aus.clip = AudioManagers.Ins.Controller();
            aus.Play();
        }
        else
        {
            aus.Pause();

        }

    }
    public void SetMove(bool status)
    {
        if (status)
        {
            /*Tutorial.SetActive(true);*/
            transform.position = new Vector3(61.58f, 5.44f, -598.57f);
            transform.rotation = Quaternion.Euler(0, 0, 14);
        }
        else
        {
            transform.position = new Vector3(64.35f, 1.6f, -598.72f);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        IsMove = status;
        joystick.background.GetComponent<Image>().enabled = !status;
        joystick.handle.GetComponent<Image>().enabled = !status;
    }
    public void GetJoy()
    {
        if(IsMove)
        {
           

            IsMove = false;
            TayCam.transform.DOMove(new Vector3(TayCam.transform.position.x, 10, TayCam.transform.position.z), 1).OnComplete(() =>
            {
            aniHandler.SetInteger(ConstName.Status, 1);
            StartCoroutine(MoveDown());
            });
        }
      
    }
    private IEnumerator MoveDown()
    {
        yield return new WaitForSeconds(0.5f);
        aniHandler.SetInteger(ConstName.Status, 2);

        TayCam.transform.DOMove(new Vector3(TayCam.transform.position.x, 15, TayCam.transform.position.z), 1).OnComplete(() =>
        {
            IsMove = true;
        });
    }
}
