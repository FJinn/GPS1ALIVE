﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VolumeSettingManager : MonoBehaviour {

    public static VolumeSettingManager instance;

    public bool setting = false;
    
    public Slider BGMSlider;
    public Slider SFXSlider;
    public AudioSource bgm;
    public Canvas MainMenu;
    public Canvas canvas;

    private void Awake()
    {
        canvas.enabled = false;
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    } 

    void Start () {

        bgm = GetComponent<AudioSource>();
        bgm.ignoreListenerVolume = true;
        BGMSlider.onValueChanged.AddListener(delegate { BGMValueChanged(); });        
        SFXSlider.onValueChanged.AddListener(delegate { SFXValueChanged(); });
        setCamera();
    }    

    public void SettingSwitch()
    {
        if(setting)
        {
            canvas.enabled = false;          
            setting = false;           
            if (MainMenu != null)
            {
                MainMenu.enabled = true;
            }         
        }
        else
        {
            setCamera();
            canvas.enabled = true;
            setting = true;
            if (MainMenu != null)
            {
                MainMenu.enabled = false;
            }
        }
    }

    void BGMValueChanged()
    {
        bgm.volume = BGMSlider.value;
    }

    void SFXValueChanged()
    {
        // set audio volume to userSetVolume
        AudioListener.volume = SFXSlider.value;
    }

    public void setCamera()
    {
        canvas.worldCamera = Camera.main;
    }

    public void defaultSetting()
    {
        BGMSlider.value = 0.5f;
        SFXSlider.value = 0.5f;
        BrightnessManager.instance.slider.value = 0f;
    }
}
