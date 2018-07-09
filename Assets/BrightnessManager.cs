using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrightnessManager : MonoBehaviour {

    public float GammaCorrection;

    public GameObject slider;

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

    void Update()
    {
        GammaCorrection = slider.GetComponent<Slider>().value;
        RenderSettings.ambientLight = new Color(GammaCorrection, GammaCorrection, GammaCorrection, 1.0f);
        DontDestroyOnLoad(this.gameObject);
    }
}
