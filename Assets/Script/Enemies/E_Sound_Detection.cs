using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Sound_Detection : MonoBehaviour {

    [Header("Enemy remain how long until start check")]
    public float shockTimer;

    [Header("Investigate sound source duration")]
    public float investigateTimer;

    [Header("Enemy alert amounts for eye light")]
    public float EM_triggerAmounts;

    [Header("Exclamation mark spawn object")]
    public GameObject DM_Object;

    [Header("Don't touch anything below")]
    public bool soundHeard;
    public Vector2 soundSource;

    private bool startMoving;
    private Vector2 tempPos;

    public GameObject EM_EyeLight;
    public bool EM_isSpawned;
    public bool EM_instantSound;
    private bool EM_alerted;
    
    private GameObject DM_Spawner;
    private bool DM_spawnOnce;
    public bool DM_triggerOnce;

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
        yield return new WaitForSeconds(6f);
        startMoving = false;
        GetComponent<E_Movement>().enabled = true;
        StopAllCoroutines();
    }



	// Use this for initialization
	void Start () {
        DM_triggerOnce = false;
	}


    void EM_Fillbar()
    {
        EM_EyeLight.GetComponent<E_DetectionIntensity>().fb_tempDelay = 0;
        if (EM_instantSound)
        {
            EM_EyeLight.GetComponent<E_DetectionIntensity>().fb_value += EM_triggerAmounts;
        }
        else
        {
            EM_EyeLight.GetComponent<E_DetectionIntensity>().fb_value += EM_triggerAmounts * Time.deltaTime;
        }
    }
	
    public void Detection_Manager()
    {
        /*
        if (!EM_isSpawned)
        {
            EM_EyeLight = (GameObject)Instantiate(EM_Object, transform.position + new Vector3(0, 14f, 0), transform.rotation);
            EM_Fillbar();
            EM_EyeLight.GetComponent<E_DetectionIntensity>().fb_Enemy = gameObject;
            EM_EyeLight.transform.parent = transform;
        }
        else
        {
            if(EM_EyeLight != null)
            {
                EM_Fillbar();
            }
            
        }*/

        foreach (Transform eyelight in transform)
        {
            if(eyelight.CompareTag("T_EnemyEyeLight"))
            {
                EM_EyeLight = eyelight.gameObject;
                EM_EyeLight.GetComponent<E_DetectionIntensity>().fb_Owner = gameObject;
                EM_Fillbar();
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
            StartCoroutine(Stunned());
        }

        if(startMoving)
        {
            if(Vector2.Distance(transform.position, soundSource) < 1f)
            {
                StartCoroutine(Investigating());
            }else
            {
                StartCoroutine(PathBlocked());
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
