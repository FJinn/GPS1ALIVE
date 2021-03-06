﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_SoundRadius : MonoBehaviour {

    public float s_soundRadius;
    [Header("Detect what kind of layer?")]
    public LayerMask enemyMask;

    [Header("Does it move to sound source?")]
    public bool s_moveTarget;

    [Header("Make sound on collide?")]
    public bool s_onCollides;

    [Header("Moving makes sound?")]
    public bool s_moveSound;

    [Header("Can trigger multiple times?")]
    public bool triggerMultiple;

    public float s_triggerAmounts;

    public bool onVent = false;

    private bool triggerOnce;

	// Use this for initialization
	void Start () {
        triggerOnce = false;
       
	}
	
	// Update is called once per frame
	void Update () {
        if(gameObject.name == "Player1" || gameObject.name == "Player2")
        {
            onVent = GetComponent<P_Vent>().onVent;
        }
        if(s_moveSound && !onVent)
        {
            if (GetComponent<Rigidbody2D>().velocity.x > 1f || GetComponent<Rigidbody2D>().velocity.x < -1f)
            {
                if(CompareTag("Player")|| CompareTag("Player2"))
                {
                    if(!GetComponent<P_controls>().inTheAir)
                    {
                        soundTrigger(s_triggerAmounts);
                    }
                }else
                {
                    soundTrigger(s_triggerAmounts);
                }
            }
            if (CompareTag("Player") || CompareTag("Player2"))
            {
                if ((GetComponent<Rigidbody2D>().velocity.y > 0.2f || GetComponent<Rigidbody2D>().velocity.y < -0.2f) & GetComponent<P_controls>().OnLadder)
                {
                    soundTrigger(s_triggerAmounts);
                }
            }
        }
	}

    private void soundTrigger(float triggerAmounts)
    {
            Collider2D[] targetsInSoundRadius = Physics2D.OverlapCircleAll(transform.position, s_soundRadius, enemyMask);
            for (int i = 0; i < targetsInSoundRadius.Length; i++)
              {
                targetsInSoundRadius[i].GetComponent<E_Sound_Detection>().EM_triggerAmounts = triggerAmounts;
                if (s_moveTarget)
                    {
                        targetsInSoundRadius[i].GetComponent<E_Sound_Detection>().soundSource = new Vector2(transform.position.x, targetsInSoundRadius[i].transform.position.y);
                        targetsInSoundRadius[i].GetComponent<E_Sound_Detection>().soundHeard = true;
                        targetsInSoundRadius[i].GetComponent<E_Sound_Detection>().Detection_Manager();
                }
                    if(s_moveSound)
                    {
                        targetsInSoundRadius[i].GetComponent<E_Sound_Detection>().Detection_Manager();
                    }
                }
        }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag != "Enemy" && s_onCollides && other.gameObject.tag != "Player" && other.gameObject.tag != "Player2")
        {
            if(!triggerOnce)
            {
                triggerOnce = true;
                soundTrigger(s_triggerAmounts);
            }

            if(triggerMultiple)
            {
                soundTrigger(s_triggerAmounts);
            }
        }
    }

}