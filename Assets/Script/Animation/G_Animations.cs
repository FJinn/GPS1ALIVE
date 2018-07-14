using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_Animations : MonoBehaviour
{   
    public Animator animator;
   
    void Update()
    {
        Walking();
        Idle();
        Crawling();
        Jumping();
        PullPush();
    }

    void Walking()
    {
        if (GetComponent<P_controls>().Walking == true)
        {
            animator.SetBool("Walking", true);
            animator.SetBool("Idle", false);
        }
    }

    void Idle()
    {
        if (GetComponent<P_controls>().Idle == true)
        {
            animator.SetBool("Walking", false);
            animator.SetBool("Idle", true);
        }
    }

    void Crawling()
    {
        if (GetComponent<P_controls>().CrawlingIdle == true)
        {
            animator.SetBool("Idle", false);
            animator.SetBool("Walking", false);
            animator.SetBool("Crawling", false);
            animator.SetBool("CrawlingIdle", true);
        }
        else if (GetComponent<P_controls>().Crawling == true)
        {
            animator.SetBool("Idle", false);
            animator.SetBool("Walking", false);
            animator.SetBool("Crawling", true);
            animator.SetBool("CrawlingIdle", false);
        }

        else
        {
            animator.SetBool("Crawling", false);
            animator.SetBool("CrawlingIdle", false);
        }
    }

    void Jumping()
    {
        if (GetComponent<P_controls>().Jumping == true)
        {
            animator.SetBool("Jumping", true);
        }
        else if (GetComponent<P_controls>().Jumping == false)
        {
            animator.SetBool("Jumping", false);
        }
    }

    void PullPush()
    {
        if (GetComponent<P_pushPull>().OnBox == true)
        {
            if (GetComponent<Rigidbody2D>().velocity.x > 0)
            {
                if (GetComponent<P_controls>().faceRight == true)
                {
                    animator.SetBool("Pushing", true);
                    animator.SetBool("Pulling", false);
                    animator.SetBool("Idle", false);
                }
                else if (GetComponent<P_controls>().faceLeft == true)
                {
                    animator.SetBool("Pushing", false);
                    animator.SetBool("Pulling", true);
                    animator.SetBool("Idle", false);
                }

            }
            else if (GetComponent<Rigidbody2D>().velocity.x < 0)
            {
                if (GetComponent<P_controls>().faceRight == true)
                {
                    animator.SetBool("Pushing", false);
                    animator.SetBool("Pulling", true);
                    animator.SetBool("Idle", false);
                }
                else if (GetComponent<P_controls>().faceLeft == true)
                {
                    animator.SetBool("Pushing", true);
                    animator.SetBool("Pulling", false);
                    animator.SetBool("Idle", false);
                }
            }
        }
        else
        {
            animator.SetBool("Pushing", false);
            animator.SetBool("Pulling", false);
        }
    }
}
