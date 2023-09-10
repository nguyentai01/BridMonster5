using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class TuLanh : MonoBehaviour
{
    public MeshRenderer[] meshTronVuong;
    int index = 0;
    public Animator animator;
    public GameObject SetGame;
    public void SetTrue(int id)
    {
        index++;
        meshTronVuong[id].enabled = true;
        if (index == 5)
        {
            animator.SetInteger(ConstName.Status, 1);
            SetGame.tag = ConstName.ItemTag ;   
        }
        
    }
}
