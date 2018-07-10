using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListenerManager : MonoBehaviour {
    
    // to get the slider object
    public Slider slider;

    public static bool oneLis;

    private void Awake()
    {
        if (oneLis == false)
        {
            oneLis = true;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        slider.onValueChanged.AddListener(delegate { valueChanged(); });

        DontDestroyOnLoad(this.gameObject);
    }
    

    public void valueChanged()
    {
        // set audio volume to userSetVolume
        AudioListener.volume = slider.value;
    }
}
