using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PopupUI_two : UICounter
{
    public Button btn_green;
    public Button btn_voice;
    public Button btn_exit;

    public Label txtTitle;
    public Label txtContent;


    void Start()
    {
        btn_green = root.Q<Button>("btn_green");
        btn_voice = root.Q<Button>("btn_voice");
        btn_exit = root.Q<Button>("btn_exit");
        
        txtTitle= root.Q<Label>("txtTitle");
        txtContent= root.Q<Label>("txtContent");

        btn_green.RegisterCallback<ClickEvent>(OnGreenClick);

        btn_exit.RegisterCallback<ClickEvent>(OnCloseClick);
    }

    private void OnGreenClick(ClickEvent evt)
    {
        Debug.Log("start click");
    }
}