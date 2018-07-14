using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSettingManager : MonoBehaviour {

    public static VolumeSettingManager instance;

    public bool setting = false;

    public Slider BGMSlider;
    public Slider SFXSlider;
    public AudioSource bgm;

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
    }

    public void SettingSwitch()
    {
        if(setting)
        {
           canvas.enabled = false;
           setting = false;
        }
        else
        {
            canvas.enabled = true;
            setting = true;
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
}
