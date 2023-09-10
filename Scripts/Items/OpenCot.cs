using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCot : MonoBehaviour
{
    public TruController TruController;

    public bool IsOpen = false;
    public Material[] materials;
    public Material nut1;

    public MeshRenderer MeshRenderer;
    public Transform target;
    private bool IsCheck = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ConstName.Drone) && !IsCheck)
        {
            Open();
            StartCoroutine(SetCheck());
        }
    }
    private IEnumerator SetCheck()
    {
        IsCheck = true;
        yield return new WaitForSeconds(1);
        IsCheck = false;
    }
    public void Open()
    {
        IsOpen = !IsOpen;
        if (!IsOpen)
        {
            SetOpen(1);
            TruController.MoveDown();
        }
        else
        {
            
            TruController.Move();

            SetOpen(2);
            GameController.ins.SetCamMinigame1();
        }
    }
    private void SetOpen(int id)
    {
        Material[] mats = new Material[MeshRenderer.materials.Length];
        mats[0] = nut1;
        mats[1] = materials[id];
        MeshRenderer.materials = mats;
    }
}
