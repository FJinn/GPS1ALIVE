using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.PostProcessing;

public class BrightnessManager : MonoBehaviour {

    public float GammaCorrection = 0f;

    public Slider slider;

    public static BrightnessManager instance;

    public ColorGradingComponent mycolor;

    public PostProcessingProfile myProfile;

    public PostProcessingProfile otherProfile;

    ColorGradingModel.Settings tempmodel;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        tempmodel = myProfile.colorGrading.settings;

        tempmodel.colorWheels.linear.gamma.a = 0;

        myProfile.colorGrading.settings = tempmodel;
        otherProfile.colorGrading.settings = tempmodel;



        slider.onValueChanged.AddListener(delegate { valueChanged(); });

        DontDestroyOnLoad(this.gameObject);
    }
    
    public void valueChanged()
    {
        // set audio volume to userSetVolume
        GammaCorrection = slider.value;

        tempmodel.colorWheels.linear.gamma.a = GammaCorrection;

        myProfile.colorGrading.settings = tempmodel;
        otherProfile.colorGrading.settings = tempmodel;

    }
    
}
