using System.Collections;
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
    public int enterCounter;
    public bool ConnectedDoor;
    public bool frontSwitch;
    public bool sideSwitch;
    
    private Animator M_animator;
    private int animCounter;

    [Header("Does this thing trigger only once when touch?")]
    public bool TriggerOnlyOnce = false;
    public bool TriggerSpecificBool = false;
    [Header("Is this a switch?")]
    public bool switchAnim = false;
    
	// Use this for initialization
	void Start () 
	{
        M_animator= GetComponent<Animator>();

        if (Pressure_Pad)
        {
            originalScaleY = transform.localScale.y;
            enterCounter = 0;
        }
	}
    
    public void UnitTrigger()
    {
        
        for (int i = 0; i < ObjectList.Length ; i++)
        {
            if(ObjectList[i] != null)
            {
                ObjectList[i].GetComponent<M_Trigger>().Trigger();
                if (TriggerSpecificBool)
                {
                    ObjectList[i].GetComponent<M_Trigger>().EnterToFall = true;
                    ObjectList[i].GetComponent<M_Trigger>().MoveHorizontal = false;
                    ObjectList[i].GetComponent<M_Trigger>().MoveVertical = false;
                }
            }
        }

        if(switchAnim)
        {
            animCounter++;
            if (animCounter == 1)
            {
                if(sideSwitch)
                {
                    M_animator.Play("AIO_AnimLeverDown");
                }
                else if(frontSwitch)
                {
                    M_animator.Play("AIO_FrontLeverDown");
                }               
                GameObject.FindGameObjectWithTag("Player").GetComponent<P_mechanismTrigger>().leverDown = true;
                GameObject.FindGameObjectWithTag("Player2").GetComponent<P_mechanismTrigger>().leverDown = true;                
                GameObject.FindGameObjectWithTag("Player").GetComponent<P_mechanismTrigger>().leverUp = false;                
                GameObject.FindGameObjectWithTag("Player2").GetComponent<P_mechanismTrigger>().leverUp = false;                
            }
            else
            if (animCounter >= 2)
            {
                if (sideSwitch)
                {
                    M_animator.Play("AIO_AnimLeverUp");
                }
                else if (frontSwitch)
                {
                    M_animator.Play("AIO_FrontLeverUp");
                }
                GameObject.FindGameObjectWithTag("Player").GetComponent<P_mechanismTrigger>().leverUp = true;
                GameObject.FindGameObjectWithTag("Player2").GetComponent<P_mechanismTrigger>().leverUp = true;
                GameObject.FindGameObjectWithTag("Player").GetComponent<P_mechanismTrigger>().leverDown = false;
                GameObject.FindGameObjectWithTag("Player2").GetComponent<P_mechanismTrigger>().leverDown = false;

                animCounter = 0;
            }
        }
        
    }

    

    // Update is called once per frame
    void Update () 
	{
		if(isStepped)
        {
            if(transform.localScale.y > 0.3f)
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
            if(ObjectList[i] != null)
            {
                Gizmos.DrawLine(transform.position, ObjectList[i].transform.position);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(Pressure_Pad && (other.CompareTag("PushPull") || other.CompareTag("Player") || other.CompareTag("Player2") || other.CompareTag("Enemy")))
        {
            if (enterCounter <= 0)
            {
                 UnitTrigger();
            }
            enterCounter++;
        }

        if(TriggerOnlyOnce && (other.CompareTag("PushPull") || other.CompareTag("Player") || other.CompareTag("Player2") || other.CompareTag("Enemy")))
        {
            TriggerOnlyOnce = false;
            UnitTrigger();
        }

        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(Pressure_Pad && (other.CompareTag("PushPull") || other.CompareTag("Player") || other.CompareTag("Player2") || other.CompareTag("Enemy")))
        {
            isStepped = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(Pressure_Pad && (other.CompareTag("PushPull") || other.CompareTag("Player") || other.CompareTag("Player2") || other.CompareTag("Enemy")))
        {
            enterCounter--;
            if (enterCounter <= 0)
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
