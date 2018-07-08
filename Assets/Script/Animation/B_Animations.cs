using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_Animations : MonoBehaviour {
    public Animator anim;

	void Update () {
        Walking();
        Idle();
        Crawling();
        Jumping();
        PullPush();
	}

    void Walking()
    {
        if(GetComponent<P_controls>().Walking == true)
        {
            anim.SetBool("Walking", true);
            anim.SetBool("Idle", false);
        }
    }

    void Idle()
    {
        if(GetComponent<P_controls>().Idle == true)
        {
            anim.SetBool("Walking", false);
            anim.SetBool("Idle", true);
        }
    }

    void Crawling()
    {
        if(GetComponent<P_controls>().CrawlingIdle == true)
        {
            anim.SetBool("Idle", false);
            anim.SetBool("Walking", false);
            anim.SetBool("Crawling", false);
            anim.SetBool("CrawlingIdle", true);
        }
        else if(GetComponent<P_controls>().Crawling == true)
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
        if(GetComponent<P_controls>().Jumping == true)
        {
            anim.SetBool("Jumping", true);
        }
        else if(GetComponent<P_controls>().Jumping == false)
        {
            anim.SetBool("Jumping", false); 
        }
    }

    void PullPush()
    {
        if(GetComponent<P_pushPull>().OnBox == true)
        {
            if(GetComponent<Rigidbody2D>().velocity.x > 0.1f)
            {
                if(GetComponent<P_controls>().faceRight == true)
                {
                    anim.SetBool("Pushing", true);
                    anim.SetBool("Pulling", false);
                    anim.SetBool("Idle", false);
                }
                else if(GetComponent<P_controls>().faceLeft == true)
                {
                    anim.SetBool("Pushing", false);
                    anim.SetBool("Pulling", true);
                    anim.SetBool("Idle", false);
                }
                
            }
            else if(GetComponent<Rigidbody2D>().velocity.x < -0.1f)
            {
                if (GetComponent<P_controls>().faceRight == true)
                {
                    anim.SetBool("Pushing", false);
                    anim.SetBool("Pulling", true);
                    anim.SetBool("Idle", false);
                }
                else if (GetComponent<P_controls>().faceLeft == true)
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
