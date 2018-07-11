using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_Animations : MonoBehaviour {
    public Animator anim;
    P_controls playerControl;
    P_pushPull playerPushPull;
    Rigidbody2D playerVelocity;

    private void Start()
    {
        playerControl = GetComponent<P_controls>();
        playerPushPull = GetComponent<P_pushPull>();
        playerVelocity = GetComponent<Rigidbody2D>();
    }

    void Update () {
        Walking();
        Idle();
        Crawling();
        Jumping();
        PullPush();
	}

    void Walking()
    {
        if(playerControl.Walking == true)
        {
            anim.SetBool("Walking", true);
            anim.SetBool("Idle", false);
        }
    }

    void Idle()
    {
        if(playerControl.Idle == true)
        {
            anim.SetBool("Walking", false);
            anim.SetBool("Idle", true);
        }
    }

    void Crawling()
    {
        if (playerControl.CrawlingIdle == true)
        {
            anim.SetBool("Idle", false);
            anim.SetBool("Walking", false);
            anim.SetBool("Crawling", false);
            anim.SetBool("CrawlingIdle", true);
        }
        else if (playerControl.Crawling == true)
        {
            anim.SetBool("Idle", false);
            anim.SetBool("Walking", false);
            anim.SetBool("Crawling", true);
            anim.SetBool("CrawlingIdle", false);
        }    
        else
        {
            anim.SetBool("Crawling", false);
            anim.SetBool("CrawlingIdle", false);
        }
    }

    void Jumping()
    {
        if(playerControl.Jumping == true)
        {
            anim.SetBool("Jumping", true);
        }
        else if(playerControl.Jumping == false)
        {
            anim.SetBool("Jumping", false); 
        }
    }

    void PullPush()
    {
        if(playerPushPull.OnBox == true)
        {
            if(playerVelocity.velocity.x > 0.1f)
            {
                if(playerControl.faceRight == true)
                {
                    anim.SetBool("Pushing", true);
                    anim.SetBool("Pulling", false);
                    anim.SetBool("Idle", false);
                }
                else if(playerControl.faceLeft == true)
                {
                    anim.SetBool("Pushing", false);
                    anim.SetBool("Pulling", true);
                    anim.SetBool("Idle", false);
                }
                
            }
            else if(playerVelocity.velocity.x < -0.1f)
            {
                if (playerControl.faceRight == true)
                {
                    anim.SetBool("Pushing", false);
                    anim.SetBool("Pulling", true);
                    anim.SetBool("Idle", false);
                }
                else if (playerControl.faceLeft == true)
                {
                    anim.SetBool("Pushing", true);
                    anim.SetBool("Pulling", false);
                    anim.SetBool("Idle", false);
                }
            }
        }
        else
        {
            anim.SetBool("Pushing", false);
            anim.SetBool("Pulling", false);
        }
    }

    void PressButton()
    {

    }
}
