﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGM_Manager : MonoBehaviour {
    
    // this will let user set BGM volume
    private float userSetVolume;
    // to get the slider object
    public GameObject slider;

    public static bool oneBGM;

    private void Awake()
    {
        if (oneBGM == false)
        {
            oneBGM = true;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Update () {
        GetComponent<AudioSource>().ignoreListenerVolume = true;

        // so volume = user-set-slider value
        userSetVolume = slider.GetComponent<Slider>().value;

        // set audio volume to userSetVolume
        GetComponent<AudioSource>().volume = userSetVolume;

        DontDestroyOnLoad(this.gameObject);
	}
}
