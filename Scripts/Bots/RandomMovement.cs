using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI; //important

//if you use this code you are contractually obligated to like the YT video
public class RandomMovement : MonoBehaviour //don't forget to change the script name if you haven't
{
    public NavMeshAgent agent;
    public float range; //radius of sphere
    public bool isRun = false;
    public Transform[] centrePoints;
    private Transform centrePoint; //centre of the area the agent wants to move around in
    //instead of centrePoint you can set it as the transform of the agent if you don't care about a specific area
    private bool checkRun = true;
    private bool checkDistance = true;
    Vector3 point;
    private List<Transform> points = new List<Transform>();
    private Transform delta;
    private Transform target;
    private bool CheckAttackPlayer = false;
    private bool IsMove1 = false;
    private bool IsMove2 = false;
    public ProccessBotMap2 Bot2;
    public BotChilldren BotChilldren;
    public AudioSource aus;
    private IEnumerator RunAudio;
    private bool IsRunAttack = false;
    

    private void OnEnable()
    {
        RunAudio = StartAudio();
    }
    private void Start()
    {
        centrePoint = RanDom();
    }

    void FixedUpdate()
    {
        if (!agent.enabled)
        {
            return;
        }
        if (isRun)
        {

            if (agent.remainingDistance <= agent.stoppingDistance && checkDistance) //done with path
            {
                PauseNav();
                Bot2.SetAni(0);
                StopAudio();
                checkDistance = false;
                StartCoroutine(RunContinue());
            }

            if (checkRun)
            {

                if (RandomPoint(centrePoint.position, range, out point))
                {
                    ResumeNav();
                    SetSpeed(3f);
                    Bot2.SetAni(1);
                    centrePoint = RanDom();
                    checkRun = false;
                    /* Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);*/
                    agent.SetDestination(point);
                    checkDistance = true;
                }
            }

        }
        else if (IsMove1 && !BotChilldren.isFinish )
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                SetContinue();
            }


        }


    }
    public void PauseNav()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
    }
    public void ResumeNav()
    {
        agent.isStopped = false;
    }
    private IEnumerator StartAudio()
    {
        while (isRun)
        {
            Debug.Log("RUn");
            yield return new WaitForSeconds(2);
            if(!Pref.GetSfx())
            {
                yield break;
            }
            aus.clip = AudioManagers.Ins.LamNhamAudio();

            aus.volume = 1.0f;
            aus.Play();
        }
    }
    public void StartRun()
    {
        StartCoroutine(RunAudio);
    }
    public void StopRun()
    {
        StopCoroutine(RunAudio);

    }
    public void SetRun(bool run)
    {
        isRun = run;
        if (isRun)
        {
            StartRun();
            return;
        }
        StopRun();
    }
    private IEnumerator RunContinue()
    {
        yield return new WaitForSeconds(Random.Range(2f, 3f));
        checkRun = true;
    }
    private bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint + Random.insideUnitSphere * range, out hit, 1.0f, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    public void StopAgent()
    {
        SetRun(false);

        agent.enabled = false;
        Bot2.SetAni(0);
        Bot2.isWin = true;
        BotChilldren.isFinish = true;
        /*Bot2.enabled = false;
        BotChilldren.enabled = false;*/
    }
    /*  public void Continute()
      {

          agent.enabled = true;
          SetRun(true);
      }*/
    private Transform RanDom()
    {
        if (points.Count > 0)
        {
            int i = Random.Range(0, points.Count);
            delta = points[i];
            points.RemoveAt(i);
            return delta;
        }
        SetPoints();

        return RanDom();
    }
    private void SetPoints()
    {
        for (int i = 0; i < centrePoints.Length; i++)
        {
            points.Add(centrePoints[i]);
        }
    }
    public void Move1(Vector3 Point)
    {
        ResumeNav();
        SetSpeed(7f);
        Bot2.SetAni(2);
        if (!IsRunAttack)
        {
            IsRunAttack = true;
            AudioRun(AudioManagers.Ins.XanhRun, true);

        }
        IsMove1 = true;
        IsMove2 = false;
        SetRun(false);
        checkRun = false;
        checkDistance = true;
        agent.SetDestination(Point);
    }
    private void AudioRun(AudioClip ac, bool loop)
    {
        aus.clip = ac;
        aus.volume = 1;
        aus.loop = loop;
        if (!Pref.GetSfx())
        {
            return;
        }
        aus.Play();
    }
    public void StopAudio()
    {
        IsRunAttack = false;
        aus.loop = false;
        aus.Stop();
    }
    public void SetSpeed(float speed)
    {
        agent.speed = speed;
    }
    public void SetContinue()
    {
        Bot2.SetAni(4); 
        IsMove1 = false;
        IsMove2 = false;
        checkRun = false;
        StartCoroutine(RunContinue());
        SetRun(true);
    }
    public void SetAttackPlayer(Transform target)
    {
        StartCoroutine(RotationCamera(target, 0));
        IsMove1 = false;
        SetRun(false);
        /*  this.target = target;
          CheckAttackPlayer = true;*/
    }
    public IEnumerator RotationCamera(Transform target, float Time)
    {
        /*SetRun(false);*/
        yield return new WaitForSeconds(Time);
        Vector3 dir = transform.position - target.position;
        float radi = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        Vector3 x = new Vector3(0, 180 + radi, 0);
        transform.DORotate(x, 1).OnComplete(() =>
        {

            AudioRun(AudioManagers.Ins.XanhAttack, false);
            StartCoroutine(UiManager.ins.SetOver(2));
            CameraShake.instance.ShakeCamera();
            Bot2.SetAni(3);
            FireBaseSet.SetFirebase(ConstName.LosedGame + "2_1");
        });

    }
    
}
