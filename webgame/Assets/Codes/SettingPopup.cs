using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SettingPopup : UICounter
{
    // Start is called before the first frame update
    private Label fogDensityValue;
    private Slider fogDensity;
    protected override void Awake()
    {
        base.Awake();
        root.style.display = DisplayStyle.None;
    }

    private void Start()
    {
        root.Q<Button>("btnSave").RegisterCallback<ClickEvent>(OnCloseClick);
        Toggle fog = root.Q<Toggle>("fog");
        fog.value = RenderSettings.fog;
        fog.RegisterValueChangedCallback(OnFogChanged);
        fogDensity = root.Q<Slider>("fogDensity");
        fogDensityValue = root.Q<Label>("densityValue");
    
        fogDensity.RegisterValueChangedCallback(OnfogDensityChanged);
    }

    private void OnfogDensityChanged(ChangeEvent<float> evt)
    {
        fogDensityValue.text = evt.newValue.ToString();
        RenderSettings.fogDensity = evt.newValue;
    }

    private void OnFogChanged(ChangeEvent<bool> evt)
    {
        RenderSettings.fog = evt.newValue;
    }

    public void Open()
    {
        root.style.display = DisplayStyle.Flex;
        fogDensityValue.text = RenderSettings.fogDensity.ToString();
        fogDensity.value = RenderSettings.fogDensity;
    }
}