﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Interaction : MonoBehaviour {

	public GameObject[] ObjectList;

    [Header("Is this pressure pad?")]
    public bool Pressure_Pad;
    public bool PPad_Delay;
    public float PPad_Delay_Time;
    private bool isStepped;
    private float originalScaleY;
    private float scaleY;
    private int enterCounter;
    
    private Animator M_animator;
    private int animCounter;

    [Header("Is this a switch?")]
    public bool switchAnim = false;

    public bool leverUp = false;
    public bool leverDown = false;

	// Use this for initialization
	void Start () 
	{
        M_animator= GetComponent<Animator>();

        if (Pressure_Pad)
        {
            originalScaleY = transform.localScale.y;
        }
	}
    
    public void UnitTrigger()
    {
        
        for (int i = 0; i < ObjectList.Length; i++)
        {
            ObjectList[i].GetComponent<M_Trigger>().Trigger();
        }
        if(switchAnim)
        {
            animCounter++;
            if (animCounter == 1)
            {
                M_animator.Play("AIO_AnimLeverDown");
                leverDown = true;
                leverUp = false;
                Debug.Log(leverDown);
            }
            else
            if (animCounter >= 2)
            {
                M_animator.Play("AIO_AnimLeverUp");
                leverDown = false;
                leverUp = true;
                Debug.Log(leverUp);
                animCounter = 0;
            }
        }
        
    }

    // Update is called once per frame
    void Update () 
	{
		if(isStepped)
        {
            if(transform.localScale.y > 0.1f)
            {
                scaleY = transform.localScale.y;
                scaleY -= 0.05f;

                transform.localScale = new Vector3(transform.localScale.x, scaleY, transform.localScale.z);
            }
        }else
        {
            if(transform.localScale.y < originalScaleY)
            {
                scaleY = transform.localScale.y;
                scaleY += 0.05f;

                transform.localScale = new Vector3(transform.localScale.x, scaleY, transform.localScale.z);
            }
        }
	}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        for (int i = 0;i < ObjectList.Length; i++)
        {
            Gizmos.DrawLine(transform.position, ObjectList[i].transform.position);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(Pressure_Pad && (other.CompareTag("PushPull") || other.CompareTag("Player") || other.CompareTag("Player2")) || other.CompareTag("Enemy"))
        {
            if (enterCounter <= 0)
            {
                UnitTrigger();
            }
            enterCounter++;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(Pressure_Pad && (other.CompareTag("PushPull") || other.CompareTag("Player") || other.CompareTag("Player2")) || other.CompareTag("Enemy"))
        {
            isStepped = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(Pressure_Pad && (other.CompareTag("PushPull") || other.CompareTag("Player") || other.CompareTag("Player2")) || other.CompareTag("Enemy"))
        {
            enterCounter--;
            if (isStepped && enterCounter <= 0)
            {
                if(PPad_Delay)
                {
                    Invoke("UnitTrigger", PPad_Delay_Time);
                }else
                {
                    UnitTrigger();
                    enterCounter = 0;
                }

            }
            isStepped = false;
        }
    }


}
