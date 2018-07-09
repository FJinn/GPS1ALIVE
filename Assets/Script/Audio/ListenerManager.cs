using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListenerManager : MonoBehaviour {

    // this will let user set BGM volume
    private float userSetVolume;
    // to get the slider object
    public GameObject slider;

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

    void Update()
    {
        // so volume = user-set-slider value
        userSetVolume = slider.GetComponent<Slider>().value;

        // set audio volume to userSetVolume
        AudioListener.volume = userSetVolume;

        DontDestroyOnLoad(this.gameObject);
    }
}
