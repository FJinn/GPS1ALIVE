using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_DetectionIntensity : MonoBehaviour {

    private Transform fill_Bar;
    public GameObject fb_Owner;
    private float fb_scaleX;
    public float fb_value;
    public float fb_MaxSize;
    public float fb_tempDelay;

    

    // Use this for initialization
    void Start () {
        fb_value = 4;
        
	}

    // Update is called once per frame
    void Update () {

        // if bar is maxed
        if(fb_value > fb_MaxSize)
        {
            fb_Owner.GetComponent<E_Sound_Detection>().DM_triggerOnce = true;
        }

        // decreasing bar
        if (fb_value > 4)
        {
            if(fb_tempDelay >= 1f)
            {
                fb_value -= 1f * Time.deltaTime;
            }
        }else 
        {
            fb_value = 4;
        }

        // temporary delay when heard sound
        if (fb_tempDelay < 1f)
        {
            fb_tempDelay += 1f * Time.deltaTime;
        }

      //  fill_Bar.localScale = new Vector3(5, fb_value, transform.localScale.z);
        GetComponent<Light>().intensity = fb_value;
    }
}
