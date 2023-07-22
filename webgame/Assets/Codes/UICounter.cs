using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Object = System.Object;

public class UICounter : MonoBehaviour
{
    public VisualElement root;

    protected virtual void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
    }

    protected void OnCloseClick(ClickEvent evt)
    {
        root.style.display = DisplayStyle.None;
    }

    private string log;
    public bool isLog = true;

    public void Log(params Object[] objs)
    {
        if (!isLog) return;
        log = String.Empty;
        if (objs.Length > 0)
        {
            foreach (var item in objs)
            {
                log += $"{item} ";
            }
        }

        Debug.Log(log);
    }
}