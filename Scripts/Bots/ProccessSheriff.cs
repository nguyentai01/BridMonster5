using DG.Tweening;
using SWS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ProccessSheriff : MonoBehaviour
{
    public splineMove sm;
    private MapController ControllerMap;
    public ProccessInteracBot[] ProccessInteracBotl;
    public Transform Target;
    public bool checkMoveFirst = false;
    private bool CheckLook = false;
    public bool checkMove = true;
    public bool CheckStart = false;
    public float timeRo = 3;
    public bool checkOpenDoor1 = false;
    public ChimProccess[] chimProccesses;
    public Animator anim;
    public Vector3 BotRotaion;
    public AudioSource aus;
    public Transform Bullet;
    public GameObject Gun;
    public ComeHereAu ComeHereAu;
    public GameObject ComeHereAu2;

    private bool CheckPath2 = false;
    private void Start()
    {
        ControllerMap = MapController.Ins;
    }
    private void Update()
    {
        if (CheckLook)
        {
            transform.LookAt(Target);
        }
    }
    public void SetMoveAgain()
    {

        string s = sm.pathContainer.ToString();
        switch (Int32.Parse(s.Substring(s.IndexOf("L") + 1, 2)))
        {
            case 1:
                CheckLook = true;
                Target = GameController.ins.ProccessPlayer.transform;
                StartCoroutine(GameController.ins.ProccessPlayer.ContinueMovePlayer(true, 0));
                ControllerMap.Map1.pathToadSters[0].gameObject.SetActive(false);
                ControllerMap.Map1.pathToadSters[1].gameObject.SetActive(true);
                SetAni(1);
                sm.Stop();
                SetPathContainer(ControllerMap.Map1.pathToadSters[1]);
                break;
            case 5:
                SetAni(0);
                chimProccesses[0].CheckFinish();
                chimProccesses[1].CheckFinish();
                /*sm.StartMove();*/
                sm.Stop();
                SetPathContainer(ControllerMap.Map1.pathToadSters[3]);
                if (GameController.ins.CheckDataFinish())
                {

                    UiManager.ins.SetUiMission(3);
                }
                StartCoroutine(GameController.ins.SetMusic(AudioManagers.Ins.story8, 1, 0));
                break;
            case 2:
                SetAni(0);
                break;
            case 6:
                SetAni(0);
                ProccessInteracBotl[0].gameObject.SetActive(false);
                ProccessInteracBotl[1].gameObject.SetActive(false);

                chimProccesses[0].SetAni(0);
                chimProccesses[1].SetAni(0);
                chimProccesses[0].transform.parent = null;
                chimProccesses[1].transform.parent = null;
                StartCoroutine(SetRotaionSheriff());
                break;
        }

        //Set 2
    }
    public void StartMove()
    {
        SetAni(4);
        sm.StartMove();
        StartCoroutine(GameController.ins.SetMusic(AudioManagers.Ins.story1, 1, 4));
    }
    private void RotationSheriff()
    {
        SetAni(4);
        if (chimProccesses[0].gameObject.activeSelf)
        {
            chimProccesses[0].ProccessBird(1, transform);
            chimProccesses[1].ProccessBird(1, transform);
        }
        Vector3 ro = transform.rotation.eulerAngles;
        ProccessInteracBotl[0].gameObject.SetActive(true);
        sm.StartMove();
        sm.Pause();
        transform.DORotate(ro + new Vector3(0, 180, 0), timeRo).OnComplete(() =>
        {
            ProccessInteracBotl[1].gameObject.SetActive(true);
            CheckStart = true;
            sm.moveToPath = true;
            sm.Resume();
            if (!checkMove)
            {
                Target = GameController.ins.ProccessPlayer.transform;
                PauseMove(1);
            }
        });

    }

    public void PauseMove(int SetAniStay)
    {
        BotRotaion = transform.rotation.eulerAngles;
        sm.Pause();
        Vector3 pos = Target.position;

        Vector3 dir = (Vector3)transform.position - pos;
        float radi = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        Vector3 x = new Vector3(0, /*-47.081f*/ 180 + radi, 0);
        if (chimProccesses[0].gameObject.activeSelf)
        {
            chimProccesses[0].ProccessBird(0, transform.parent);
            chimProccesses[1].ProccessBird(0, transform.parent);
        }
        transform.DORotate(x, timeRo).OnComplete(() =>
        {

            CheckLook = true;

            if (checkMove)
            {
                SetResume();
                return;
            }
            SetAni(SetAniStay);
        });
    }
    public void SetResume()
    {
        SetAni(4);

        CheckLook = false;
        transform.DORotate(BotRotaion, timeRo).OnComplete(() =>
        {
            if (chimProccesses[0].gameObject.activeSelf)
            {
                chimProccesses[0].ProccessBird(1, transform);
                chimProccesses[1].ProccessBird(1, transform);
            }
            CheckLook = false;
            sm.Resume();
            if (!checkMove)
            {
                PauseMove(1);
            }
        });

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ConstName.Player_Tag) && !checkMoveFirst)
        {
            CheckLook = false;
            checkMoveFirst = true;
            RotationSheriff();

            if (CheckPath2)
            {
                StartCoroutine(GameController.ins.SetMusic(AudioManagers.Ins.story9, 1, 1.5f));
                CheckPath2 = false;
            }
        }

    }
    public void SetAni(int status)
    {
        anim.SetInteger("status", status);
    }
    public void SetOpenDoor1()
    {
        ProccessInteracBotl[0].gameObject.SetActive(true);
        ProccessInteracBotl[1].gameObject.SetActive(true);
        Invoke("SetResume", 3);

        checkMove = true;
    }
    public void RotationCameraBot(Transform target)
    {
        checkMove = false;
        ProccessInteracBotl[0].gameObject.SetActive(false);
        ProccessInteracBotl[1].gameObject.SetActive(false);
        Target = target;
        PauseMove(2);
    }
    public IEnumerator Proccess1()
    {

        StartCoroutine(GameController.ins.SetMusic(AudioManagers.Ins.story5, 1, 1));
        SetAni(4);

        CheckLook = false;
        transform.DORotate(BotRotaion, timeRo).OnComplete(() =>
        {
            sm.Resume();
        });
        yield return new WaitForSeconds(2);
        SetAni(3);
        sm.ChangeSpeed(15);
    }
    public void Proccess2()
    {
        gameObject.SetActive(true);
        chimProccesses[0].gameObject.SetActive(true);
        chimProccesses[1].gameObject.SetActive(true);
        chimProccesses[0].SetAni(1);
        chimProccesses[1].SetAni(1);
        sm.ChangeSpeed(5);
        SetPath(MapController.Ins.Map1.pathToadSters[2]);


    }
    public void SetPath(PathManager path)
    {
        SetPathContainer(path);
        sm.StartMove();
    }
    public void SetPathContainer(PathManager path)
    {
        sm.pathContainer = path;
    }
    public IEnumerator SetAudioSFx(AudioClip ac, float time)
    {
        yield return new WaitForSeconds(time);
        aus.PlayOneShot(ac);
    }
    public IEnumerator SetAudioSteriff(AudioClip ac, float time)
    {
        yield return new WaitForSeconds(time);
        aus.clip = ac;
        aus.Play();
    }
    public void SetPauseAudio(bool isPause)
    {
        aus.mute = isPause;
    }
    public void ProccessRevive1()
    {
        checkMoveFirst = true;
        gameObject.transform.position = MapController.Ins.Map1.ReviveSheriff.position;
        chimProccesses[0].gameObject.SetActive(false);
        chimProccesses[1].gameObject.SetActive(false);
        SetAni(4);
    }
    public void Proccess3()
    {
        SetAni(1);
       /* SetPathContainer(MapController.Ins.Map1.pathToadSters[3]);*/
        CheckPath2 = true;
        checkMoveFirst = false;
        ComeHereAu.enabled = true;
        ProccessInteracBotl[0].gameObject.SetActive(true);
        ProccessInteracBotl[1].gameObject.SetActive(true);
    }
    public IEnumerator FirePlayer()
    {

        Gun.SetActive(true);
        chimProccesses[0].transform.parent =null;
        chimProccesses[1].transform.parent = null;
        SetAni(0);
        yield return new WaitForSeconds(3);
        Vector3 dir = (Vector3)transform.position - GameController.ins.ProccessPlayer.transform.position;
        float radi = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        transform.DORotate(new Vector3(0, 180 + radi, 0), timeRo).OnComplete(() =>
        {
            SetAni(5);
            StartCoroutine(ShottingPlayer());
            StartCoroutine(UiManager.ins.SetOver(1));
        });
        FireBaseSet.SetFirebase(ConstName.LosedGame + "1_2");
    }
    private IEnumerator ShottingPlayer()
    {
        yield return new WaitForSeconds(0.5f);
        Bullet.parent = null;
        Bullet.DOMove(GameController.ins.ProccessPlayer.target2.position, 0.2f);
    }
    private IEnumerator SetRotaionSheriff()
    {
        yield return new WaitForSeconds(1);
        Vector3 dir = (Vector3)transform.position - MapController.Ins.Map1.BiaDan.transform.position;
        float radi = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        transform.DORotate(new Vector3(0, 180 + radi, 0), timeRo).OnComplete(() =>
        {
            SetAni(2);
        });
    }
}
