using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetCamControl : MonoBehaviour
{
    public Transform body;
    public Vector2 tempCamTarget;
    public float minCamAngle = -15.0f;
    public float maxCamAngle ;
    public float sensitivityCam = 30f;
    public FixedTouchField touchField;

    public Transform camTarget;
    public bool moveCamPath;
    public bool CheckDrone = false;
    private void OnEnable()
    {
        /*touchField = UIManager.ins.gamePlay.fixedTouchField;*/
    }
    private void Start()
    {
        sensitivityCam = Pref.GetSensi();
    }
    private void Update()
    {
        if (!moveCamPath)
        {
            PlayerRotation();
        }
    }
    void PlayerRotation()
    {
        
        tempCamTarget += new Vector2(-touchField.TouchDist.y, touchField.TouchDist.x) * Time.deltaTime * sensitivityCam;
        tempCamTarget.x = Mathf.Clamp(tempCamTarget.x, minCamAngle, maxCamAngle);
        camTarget.rotation = Quaternion.Euler(tempCamTarget.x, tempCamTarget.y, 0);
    }
    // public void SetCamTargetTrue()
    // {
    //     UIManager.ins.gamePlay.floatingJoystick.gameObject.SetActive(true);
    //     UIManager.ins.gamePlay.btnChangeCamDrone.GetComponent<Button>().enabled = true;
    //     camTarget.gameObject.SetActive(true);
    // }
    // public void SetCamTargetFalse()
    // {
    //     var uiGame = UIManager.ins.gamePlay;
    //     camTarget.gameObject.SetActive(false);
    //     AudioManager.ins.StopOtherJumpAndMove(PreConsts.AUDIO_STEP);
    //     uiGame.floatingJoystick.input = Vector2.zero;
    //     uiGame.floatingJoystick.background.gameObject.SetActive(false);
    //     uiGame.floatingJoystick.gameObject.SetActive(false);
    //     uiGame.btnChangeCamDrone.GetComponent<Button>().enabled = false;
    // }
}
