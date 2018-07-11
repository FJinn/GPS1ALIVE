using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.PostProcessing;

public class BrightnessManager : MonoBehaviour {

    public float GammaCorrection;

    public Slider slider;

    public static bool oneBright;
   
    private void Awake()
    {
        if (oneBright == false)
        {
            oneBright = true;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {

        slider.onValueChanged.AddListener(delegate { valueChanged(); });

        DontDestroyOnLoad(this.gameObject);
    }
    
    public void valueChanged()
    {
        // set audio volume to userSetVolume
        GammaCorrection = slider.value;
    }

    private void Update()
    {
        RenderSettings.ambientLight = new Color(GammaCorrection, GammaCorrection, GammaCorrection, 1.0f);
    }
}
