﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_AM_Destroy : MonoBehaviour {

    private Color AM_myColor;
    private float AM_alpha = 1f;
    public GameObject AM_enemy;
    public GameObject p;

	// Use this for initialization
	void Start () {

        p = GameObject.Find("Player1");

        p.GetComponent<P_Death>().StartCoroutine("Dead");
        
        if(AM_enemy != null)
        {
            AM_enemy.GetComponent<E_Sound_Detection>().EM_isSpawned = true;
        }
        
        AM_myColor = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, AM_alpha);
	}
	
	// Update is called once per frame
	void Update () {
        if (AM_alpha > 0.1f)
        {
            AM_alpha -= 0.25f * Time.deltaTime;
        }else
        {
            if(AM_enemy != null)
            {
                AM_enemy.GetComponent<E_Sound_Detection>().EM_isSpawned = false;
            }
            Destroy(gameObject);
        }

        AM_myColor = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, AM_alpha);
        GetComponent<SpriteRenderer>().color = AM_myColor;

	}
}
