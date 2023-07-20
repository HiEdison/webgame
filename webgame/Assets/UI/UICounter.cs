using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UICounter : MonoBehaviour
{
  
    public VisualElement root;

    private void Awake()
    {
        root  = GetComponent<UIDocument>().rootVisualElement;
    }
    
    protected void OnCloseClick(ClickEvent evt)
    {
        root.style.display = DisplayStyle.None;
    }
}
