using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Interaction : MonoBehaviour {

	public GameObject[] ObjectList;

    [Header("Is this pressure pad?")]
    public bool Pressure_Pad;
    private bool isStepped;
    private float originalScaleY;
    private float scaleY;
    private int enterCounter;
    
    private Animator M_animator;
    private Animation M_animation;


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
        if(Pressure_Pad && (other.CompareTag("PushPull") || other.CompareTag("Player") || other.CompareTag("Player2")))
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
        if(Pressure_Pad && (other.CompareTag("PushPull") || other.CompareTag("Player") || other.CompareTag("Player2")))
        {
            isStepped = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(Pressure_Pad && (other.CompareTag("PushPull") || other.CompareTag("Player") || other.CompareTag("Player2")))
        {
            enterCounter--;
            if (isStepped && enterCounter == 0)
            {
                UnitTrigger();
            }
            isStepped = false;
        }
    }


}
