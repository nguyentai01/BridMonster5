using UnityEngine;
/*using SWS;*/
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public CapsuleCollider capsuleCollider;
  /*  public SetCamControl setCamControl;*/
   /* public PathManager pathManager;
    public splineMove splineMove;*/
    public float speed = 15;
    public Vector3 move;

    public float gravity = 0f;
    public float jumpHeight = 2;
    public Vector3 velocity;
    public GameObject camTarget;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public bool isGrounded;
    public float distance;
    public bool editor;
    public bool canMove;
    public bool checkMoveFan;
    public bool CheckMovePlayer =true;
    private bool IsJump;
    public bool CheckMovePlayer2 = false;

    public SetMovePlayer SetMovePlayer;
    public Animator ani;
    public Animator aniDk;

    public Animator[] SachsController;

    public FloatingJoystick joystick;
    private bool checkMove = false;
    private void OnEnable()
    {
        canMove = true;
   /*     joystick = UIManager.ins.gamePlay.floatingJoystick;*/
    }
    void Start()
    {
        IsJump = false;
        
    }

    void Update()
    {
        if (!CheckMovePlayer)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            editor = true;
        }
        if (canMove)
        {
            if (editor)
            {
                float x = Input.GetAxis("Horizontal");
                float z = Input.GetAxis("Vertical");
                move = camTarget.transform.right * x + camTarget.transform.forward * z;
            
                move.y = 0.1f;
                if ((x != 0 || z != 0) && !checkMove)
                {
                    SetAni(1);
                    MapController.Ins.Map2.BotMap.checkPlayerMove = true;
                    SetMovePlayer.SetPlayerTrue(true);
                    checkMove = true;
                }
                else if ((x == 0 && z == 0) && checkMove)
                {
                    SetAni(0);
                    MapController.Ins.Map2.BotMap.checkPlayerMove = false;
                    SetMovePlayer.SetPlayerTrue(false);
                    checkMove = false;
                }
            }
            else
            {
                float x = joystick.Horizontal;
                float z = joystick.Vertical;
                move = camTarget.transform.right * x + camTarget.transform.forward * z;
                move.y = 0.1f;
                if ((x != 0 || z != 0)&& !checkMove)
                {
                    SetAni(1);
                    MapController.Ins.Map2.BotMap.checkPlayerMove = true;
                    SetMovePlayer.SetPlayerTrue(true);
                    checkMove = true;
                }
                else if ((x == 0 && z == 0) && checkMove)
                {
                    SetAni(0);
                    MapController.Ins.Map2.BotMap.checkPlayerMove = false;
                    SetMovePlayer.SetPlayerTrue(false);

                    checkMove = false;
                }
            }
        
            controller.Move(move * speed * Time.deltaTime);
            isGrounded = Physics.CheckSphere(groundCheck.position, distance, groundLayer);
        }
        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }
        else
        {

            velocity.y += gravity * Time.deltaTime;
           
        }
        controller.Move(velocity * Time.deltaTime);
    }
     
    public void Jump()
    {
        
        if (isGrounded)
        {
            
            StartCoroutine(CheckJumpFinish());
        }
        /* if (!IsJump && !CheckJump)
         {
            *//* SetAni(0);
             IsJump = true;
             Invoke("SetJump", 1f);

             velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);*//*

         }*/
    }
    private IEnumerator CheckJumpFinish()
    {
        SetAni(0);
        AudioManagers.Ins.SetSfx(AudioManagers.Ins.Jump, 1);
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        MapController.Ins.Map2.BotMap.checkPlayerMove = true;
        SetMovePlayer.SetPlayerTrue(true);
        yield return new WaitForSeconds(1f);
        if (!checkMove)
        {
            MapController.Ins.Map2.BotMap.checkPlayerMove = false;
            SetMovePlayer.SetPlayerTrue(false);
        }
        else
        {
            SetAni(1);
        }
    }
    private void SetJump()
    {
        
        checkMove = false;
        IsJump = false;
    }
    public void SetAni(int status)
    {
        ani.SetInteger("status", status);
        aniDk.SetInteger("status", status);
        SetAniSachs(status);
    }
    public void setMove()
    {
        joystick.input =  Vector2.zero;
        joystick.background.gameObject.SetActive(false);
        SetAni(0);
        
    }
    public IEnumerator SetPausePlayer(bool pause,float time)
    {
        yield return new WaitForSeconds(time);
        CheckMovePlayer = pause;
        SetAni(0);
    }
    public void SetAniSachs(int status)
    {
        if (EventUi.instance.idSach != -1)
        {
            SachsController[EventUi.instance.idSach].SetInteger(ConstName.Status, status);

        }
    }
    public IEnumerator SetCoillisionPlayer(float time,float vex)
    {
        yield return new WaitForSeconds(time);
        velocity.x = vex;
        velocity.z = vex;

    }
    public void SetVelocity()
    {
         StartCoroutine(SetCoillisionPlayer(0, 2));
        StartCoroutine(SetCoillisionPlayer(2, 0));
    }
}
