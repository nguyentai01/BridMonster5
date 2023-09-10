using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;

public class ToolController : MonoBehaviour
{
    public GameObject tool;
    public void OnOff()
    {
        tool.SetActive(!tool.activeSelf);
    }
    public void btn_Chapter(int chapter)
    {
        Pref.SetPointLoadChapter(chapter);
        Pref.SetPointSave(chapter);
    }
}
