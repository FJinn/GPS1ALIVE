using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Sound_Detection : MonoBehaviour {

    [Header("Enemy remain how long until start check")]
    public float shockTimer;

    [Header("Investigate sound source duration")]
    public float investigateTimer;
    
    [Header("Exclamation mark spawn object")]
    public GameObject DM_Object;

    [Header("Detection meter object")]
    public GameObject EM_Object;

    [Header("Don't touch anything below")]
    [HideInInspector] public bool soundHeard;
    [HideInInspector] public Vector2 soundSource;

    private bool startMoving;
    private Vector2 tempPos;

    [HideInInspector] public GameObject EM_DetectionMeter;
    [HideInInspector] public float EM_triggerAmounts;
    [HideInInspector] public bool EM_isSpawned;
    [HideInInspector] public bool EM_instantSound;
    private bool EM_alerted;
    
    private GameObject DM_Spawner;
    private bool DM_spawnOnce;
    [HideInInspector]  public bool DM_triggerOnce;
    [HideInInspector] public Animator anim;
    [SerializeField] bool isNurse;

    IEnumerator Stunned()
    {
        yield return new WaitForSeconds(shockTimer);
        startMoving = true;
        StopAllCoroutines();
    }

    IEnumerator Investigating()
    {
        yield return new WaitForSeconds(investigateTimer);
        startMoving = false;
        GetComponent<E_Movement>().enabled = true;
        StopAllCoroutines();
    }

    IEnumerator PathBlocked()
    {
        // THIS IS FOR WHEN THE ENEMY IS BLOCKED BY SOMETHING AND COULDN'T GO FORWARD, UNTIL A SPECIFIC TIME PERIOD THEN IT TURNS BACK AGAIN
        yield return new WaitForSeconds(10f);
        startMoving = false;
        GetComponent<E_Movement>().enabled = true;
        StopAllCoroutines();
    }



	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        DM_triggerOnce = false;
	}


    void EM_Fillbar()
    {
        EM_DetectionMeter.GetComponent<E_Detection_Meter>().fb_tempDelay = 0;
        if (EM_instantSound)
        {
            EM_DetectionMeter.GetComponent<E_Detection_Meter>().fb_value += EM_triggerAmounts;
        }
        else
        {
            EM_DetectionMeter.GetComponent<E_Detection_Meter>().fb_value += EM_triggerAmounts * Time.deltaTime;
        }
    }
	
    public void Detection_Manager()
    {
        if (!EM_isSpawned)
        {
            EM_DetectionMeter = (GameObject)Instantiate(EM_Object, transform.position + new Vector3(0, 14f, 0), transform.rotation);
            EM_Fillbar();
            EM_DetectionMeter.GetComponent<E_Detection_Meter>().fb_Enemy = gameObject;
        }
        else
        {
            if(EM_DetectionMeter != null)
            {
                EM_Fillbar();
            //    FindObjectOfType<AudioManager>().Play("EnemyDetect");
            }
            
        }
        
    }


	// Update is called once per frame
	void Update () {
        
        if(soundHeard)
        {
            soundHeard = false;
            startMoving = false;
            GetComponent<E_Movement>().enabled = false;

            if (isNurse)
            {
                anim.Play("N_IdleAnim");
            }
            else
            {
                anim.Play("D_IdleAnim");
            }

            StopAllCoroutines();
            StartCoroutine(Stunned());
        }

        if(startMoving)
        {
            if(Vector2.Distance(transform.position, soundSource) < 1f)
            {
                StartCoroutine(Investigating());

                if (isNurse)
                {
                    anim.Play("N_IdleAnim");
                }
                else
                {
                    anim.Play("D_IdleAnim");
                }
            }
            else
            {
                StartCoroutine(PathBlocked());

                if (isNurse)
                {
                    anim.Play("N_PatrolAnim");
                }
                else
                {
                    anim.Play("D_PatrolAnim");
                }

                transform.position = Vector2.MoveTowards(transform.position, soundSource, GetComponent<E_Movement>().e_patrolSpeed * Time.deltaTime);
                if(transform.position.x < soundSource.x)
                {
                    transform.localScale = new Vector3(1,1,1);
                }else
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
            }
        }

        if(!DM_spawnOnce && DM_triggerOnce)
        {
            DM_spawnOnce = true;                                             // change the exclamation spawn position here
            DM_Spawner = (GameObject)Instantiate(DM_Object, transform.position + new Vector3(0,15f,0), transform.rotation);
            DM_Spawner.transform.parent = transform;
            DM_Spawner.GetComponent<E_AM_Destroy>().AM_enemy = gameObject;
        }

    }
    
}
