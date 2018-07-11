using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGM_Manager : MonoBehaviour {
    
    // to get the slider object
    public Slider slider;

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

    private void Start()
    {
        GetComponent<AudioSource>().ignoreListenerVolume = true;
        
        slider.onValueChanged.AddListener(delegate { valueChanged(); });

        DontDestroyOnLoad(this.gameObject);
    }

    public void valueChanged()
    {
        // set audio volume to userSetVolume
        GetComponent<AudioSource>().volume = slider.value;
    }
    
}
