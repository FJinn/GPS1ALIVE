using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.PostProcessing;

public class BrightnessManager : MonoBehaviour {

    public float BrightnessCorrection = 0f;

    public Slider slider;

    public static BrightnessManager instance;

    public ColorGradingComponent mycolor;

    public PostProcessingProfile myProfile;

    public PostProcessingProfile otherProfile;

    public PostProcessingProfile tutorialProfile;

    ColorGradingModel.Settings tempmodel;
    ColorGradingModel.Settings tutorialmodel;

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
        tutorialmodel = tutorialProfile.colorGrading.settings;

        tempmodel.colorWheels.log.power.a = 0;
        tutorialmodel.colorWheels.log.power.a = 0;

        myProfile.colorGrading.settings = tempmodel;
        otherProfile.colorGrading.settings = tempmodel;
        tutorialProfile.colorGrading.settings = tutorialmodel;



        slider.onValueChanged.AddListener(delegate { valueChanged(); });

        DontDestroyOnLoad(this.gameObject);
    }
    
    public void valueChanged()
    {
        // set audio volume to userSetVolume
        BrightnessCorrection = slider.value;

        tempmodel.colorWheels.log.power.a = BrightnessCorrection;
        tutorialmodel.colorWheels.log.power.a = BrightnessCorrection;

        myProfile.colorGrading.settings = tempmodel;
        otherProfile.colorGrading.settings = tempmodel;
        tutorialProfile.colorGrading.settings = tutorialmodel;

    }
    
}
