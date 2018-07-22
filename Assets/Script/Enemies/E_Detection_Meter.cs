using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Detection_Meter : MonoBehaviour {

    private Transform fill_Bar;
    public GameObject fb_Enemy;
    private float fb_scaleX;
    public float fb_value;
    public float fb_MaxSize;
    public float fb_tempDelay;


    public GameObject DM_Object;
    private GameObject DM_Spawner;

    // Use this for initialization
    void Start () {
        fb_Enemy.GetComponent<E_Sound_Detection>().EM_isSpawned = true;

        foreach (Transform child in transform)
        {
            if(child.CompareTag("Fillbar"))
            {
                fill_Bar = child;
            }
        }
        
	}

    private void DestroySelf()
    {
        if(fb_Enemy != null)
        {
            fb_Enemy.GetComponent<E_Sound_Detection>().EM_isSpawned = false;
        }
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update () {

        // always facing right

		// updating fill bar's size;

        // if bar is maxed
        if(fb_value > fb_MaxSize)
        {
            DM_Spawner = (GameObject)Instantiate(DM_Object, transform.position, transform.rotation);
            DM_Spawner.transform.parent = fb_Enemy.transform;
            DM_Spawner.GetComponent<E_AM_Destroy>().AM_enemy = fb_Enemy;
            DestroySelf();
        }

        // decreasing bar
        if (fb_value > 0)
        {
            if(fb_tempDelay >= 1f)
            {
                fb_value -= 0.1f * Time.deltaTime;
            }
            CancelInvoke();
        }else 
        {
            fb_value = 0;
            Invoke("DestroySelf", 2f);
        }

        // temporary delay when heard sound
        if (fb_tempDelay < 1f)
        {
            fb_tempDelay += 1f * Time.deltaTime;
        }

        //  fill_Bar.localScale = new Vector3(5, fb_value, transform.localScale.z);
        fill_Bar.transform.localScale = new Vector3(transform.localScale.x, fb_value, transform.localScale.y);
        transform.position = fb_Enemy.transform.position + new Vector3(0, 14f, 0);

        if(fb_Enemy == null)
        {
            Destroy(gameObject);
        }
    }
}
