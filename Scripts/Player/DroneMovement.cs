using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneMovement : MonoBehaviour
{
    public CharacterController controller;
    public SetCamControl setCamControl;
    public float speed = 15;
    public Vector3 move;
    public GameObject camTarget;
    public bool editor;
    public bool canMove;


    public FloatingJoystick joystick;
    private void OnEnable()
    {
        /*joystick = UIManager.ins.gamePlay.floatingJoystick;*/
    }
  
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
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
            }
            else
            {
                float x = joystick.Horizontal;
                float z = joystick.Vertical;
                move = camTarget.transform.right * x + camTarget.transform.forward * z;
            }

            controller.Move(move * speed * Time.deltaTime);
        }
    }
}
