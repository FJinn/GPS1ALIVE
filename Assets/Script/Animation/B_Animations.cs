using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_Animations : MonoBehaviour {
    public Animator anim;
    P_Vent playerVent;
    P_controls playerControl;
    P_pushPull playerPushPull;
    P_throw playerThrow;
    P_mechanismTrigger leverInteract;
    Rigidbody2D playerVelocity;

    bool enterVent = false;
    bool exitVent = false;
    public bool pullLeft = false;
    public bool pullRight = false;
    public bool pushLeft = false;
    public bool pushRight = false;
    public bool player1 = false;
    public bool player2 = false;
    public bool p1FallDeath = false;
    public bool p2FallDeath = false;

    private void Start()
    {
        playerVent = GetComponent<P_Vent>();
        playerControl = GetComponent<P_controls>();
        playerPushPull = GetComponent<P_pushPull>();
        playerThrow = GetComponent<P_throw>();
        leverInteract = GetComponent<P_mechanismTrigger>();
        playerVelocity = GetComponent<Rigidbody2D>();
    }

    void Update () {
        Movement();
        VentMovement();  
        PullPush();
        OnLadder();
        Interaction();
        Spotted();
        Throwing();
	}

    void Movement()
    {
        if(playerVelocity.velocity.x > 0.1f || playerVelocity.velocity.x < -0.1f)
        {
            anim.SetBool("Walking", true);
            anim.SetBool("Idle", false);
        }
        else
        {
            anim.SetBool("Walking", false);
            anim.SetBool("Idle", true);
        }

        if(playerVelocity.velocity.y > 10f)
        {
            anim.SetBool("Jumping", true);
        }
        else
        {
            anim.SetBool("Jumping", false);
        }

        if(playerControl.fallen)
        {
            anim.SetBool("Fallen", true);
            anim.SetBool("Idle", false);
            anim.SetBool("Walking", false);
            anim.SetBool("Jumping", false);           
        } 
        if(p1FallDeath && player2)
        {
            anim.SetBool("Scared", true);
            anim.SetBool("Jumping", false);
            anim.SetBool("Idle", false);
            anim.SetBool("Walking", false);
        }
        else if(p2FallDeath && player1)
        {
            anim.SetBool("Scared", true);
            anim.SetBool("Jumping", false);
            anim.SetBool("Idle", false);
            anim.SetBool("Walking", false);
        }
    }   

    void VentMovement()
    {
        if (playerControl.onVent)
        {
            anim.SetBool("Idle", false);
            anim.SetBool("Walking", false);
            if (!enterVent)
            {
                playerControl.StopGameControl = true;
                anim.SetBool("Walking", false);
                anim.SetBool("EnterVent", true);
                anim.SetBool("ExitVent", false);
               // Invoke("EnterVentTransition", 1f);
            }
            else if(enterVent)
            {
                //playerControl.StopGameControl = false;
                if (playerVelocity.velocity.x > 0.1f || playerVelocity.velocity.x < -0.1f)
                {
                    anim.SetBool("CrawlingIdle", false);
                    anim.SetBool("Crawling", true);
                }
                else
                {
                    anim.SetBool("CrawlingIdle", true);
                    anim.SetBool("Crawling", false);
                }
            }
        }       
        else if (!playerControl.onVent && playerVent.exitVent == true)
        {                      
            if(!exitVent)
            {
                playerControl.StopGameControl = true;
                anim.SetBool("CrawlingIdle", false);
                anim.SetBool("Crawling", false);
                anim.SetBool("ExitVent", true);
                anim.SetBool("EnterVent", false);
                //Invoke("ExitVentTransition", 0.8f);
            }
        }
        else if (playerControl.fallFromVent)
        {
            anim.SetBool("CrawlingIdle", false);
            anim.SetBool("Crawling", false);
            anim.SetBool("Idle", true);
            anim.SetBool("Walking", true);
            playerControl.fallFromVent = false;
            exitVent = false;
            enterVent = false;
            playerVent.exitVent = false;
        }
    }

    void EnterVentTransition()
    {
        enterVent = true;
        playerControl.StopGameControl = false;
        anim.SetBool("EnterVent", false);
    }

    void ExitVentTransition()
    {
        playerControl.StopGameControl = false;
        exitVent = false;
        enterVent = false;
        playerVent.exitVent = false;
        anim.SetBool("ExitVent", false);
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
                    anim.SetBool("PushIdle", false);
                    anim.SetBool("PullIdle", false);
                    pushRight = true;
                    pushLeft = false;
                    pullLeft = false;
                    pullRight = false;
                }
                else if(playerControl.faceLeft == true)
                {
                    anim.SetBool("Pushing", false);
                    anim.SetBool("Pulling", true);
                    anim.SetBool("Idle", false);
                    anim.SetBool("PushIdle", false);
                    anim.SetBool("PullIdle", false);
                    pullRight = true;
                    pullLeft = false;
                    pushRight = false;
                    pushLeft = false;                
                }                
            }
            else if(playerVelocity.velocity.x < -0.1f)
            {
                if (playerControl.faceRight == true)
                {
                    anim.SetBool("Pushing", false);
                    anim.SetBool("Pulling", true);
                    anim.SetBool("Idle", false);
                    anim.SetBool("PushIdle", false);
                    anim.SetBool("PullIdle", false);
                    pullLeft = true;
                    pullRight = false;
                    pushRight = false;
                    pushLeft = false;                  
                }
                else if (playerControl.faceLeft == true)
                {
                    anim.SetBool("Pushing", true);
                    anim.SetBool("Pulling", false);
                    anim.SetBool("Idle", false);
                    anim.SetBool("PushIdle", false);
                    anim.SetBool("PullIdle", false);
                    pushLeft = true;
                    pushRight = false;
                    pullLeft = false;
                    pullRight = false;
                }
            }
            else
            {
                if(pushLeft)
                {
                    anim.SetBool("PushIdle", true);              
                    anim.SetBool("Pushing", false);
                    anim.SetBool("Idle", false);
                }
                else if(pushRight)
                {
                    anim.SetBool("PushIdle", true);
                    anim.SetBool("Pushing", false);
                    anim.SetBool("Pulling", false);
                    anim.SetBool("Idle", false);
                }
                else if(pullLeft)
                {
                    anim.SetBool("Pushing", false);
                    anim.SetBool("PullIdle", true);
                    anim.SetBool("Pulling", false);
                    anim.SetBool("Idle", false);
                }
                else if(pullRight)
                {
                    anim.SetBool("Pushing", false);
                    anim.SetBool("PullIdle", true);
                    anim.SetBool("Pulling", false);
                    anim.SetBool("Idle", false);
                }        
            }
        }
        else
        {
            anim.SetBool("Pushing", false);
            anim.SetBool("Pulling", false);
            anim.SetBool("PushIdle", false);
            anim.SetBool("PullIdle", false);
        }
    }
    
    void Interaction()
    {
        if (leverInteract.triggerLever && leverInteract.leverDown)
        {
            playerControl.StopGameControl = true;
            anim.SetBool("PullLever", true);
            anim.SetBool("PushLever", false);
            anim.SetBool("Idle", false);
        }       
        else if(leverInteract.triggerLever && leverInteract.leverUp)
        {
            playerControl.StopGameControl = true;
            anim.SetBool("PullLever", false);
            anim.SetBool("PushLever", true);
            anim.SetBool("Idle", false);
        }

        if(playerControl.openDoor)
        {
            playerControl.StopGameControl = true;
            anim.SetBool("Pressing", true);
            anim.SetBool("Idle", false);
        }
    }

    void ResetDoorInteraction()
    {
        playerControl.StopGameControl = false;
        playerControl.openDoor = false;
        anim.SetBool("Pressing", false);
        anim.SetBool("Idle", true);
    }

    void ResetLeverInteraction()
    {
        leverInteract.triggerLever = false;
        playerControl.StopGameControl = false;
        anim.SetBool("PullLever", false);
        anim.SetBool("PushLever", false);
        anim.SetBool("Idle", true);
    }

    void OnLadder()
    {
        if(playerControl.OnLadder)
        {
            anim.SetBool("Idle", false);
            anim.SetBool("Walking", false);
            if (playerVelocity.velocity.y == 0)
            {               
                anim.SetBool("ClimbingIdle", true);
                anim.SetBool("Climbing", false);
            }
            else
            {
                anim.SetBool("ClimbingIdle", false);
                anim.SetBool("Climbing", true);
            }       
        }
        else
        {
            anim.SetBool("Climbing", false);
            anim.SetBool("ClimbingIdle", false);
        }
    }

    void Spotted()
    {
        if(playerControl.spotted)
        {
            anim.SetBool("Scared", true);
            anim.SetBool("Jumping", false);
            anim.SetBool("Idle", false);
            anim.SetBool("Walking", false);
        }
    }

    void Throwing()
    {
        if(playerThrow.pickedUp)
        {
            anim.SetBool("PickUp", true);
            anim.SetBool("Idle", false);
            anim.SetBool("Walking", false);           
        }
        
        if(playerThrow.throwStance)
        {
            if (playerThrow.throwed)
            {
                anim.SetBool("PickUp", false);
                anim.SetBool("Aim", false);
                anim.SetBool("Throw", true);                
            }
        }     
    }

    public void ResetThrowInteraction()
    {
        anim.SetBool("Aim", false);
        anim.SetBool("Throw", false);
        anim.SetBool("PickUp", false);
        anim.SetBool("Idle", true);
        playerControl.StopGameControl = false;
        playerThrow.pickedUp = false;
        playerThrow.throwed = false;
        playerThrow.canThrow = false;
        playerThrow.canUseStonePile = false;
        playerThrow.droppedStone = false;
    }  

    void SetCanThrow()
    {
        playerThrow.canThrow = true;        
    }

    void DisableCanThrow()
    {
        playerThrow.canThrow = false;
    }

    void SetCanUseStonePile()
    {
        playerThrow.canUseStonePile = true;
    }

    void DisableCanUseStonePile()
    {
        playerThrow.canUseStonePile = false;
    }    
}
