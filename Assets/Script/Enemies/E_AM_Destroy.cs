using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_AM_Destroy : MonoBehaviour {

    private Color AM_myColor;
    private float AM_alpha = 1f;
    private Animator AM_animator;
    public GameObject AM_enemy;
    public GameObject[] p;

    private SpriteRenderer AM_SR;
    private E_Sound_Detection AM_ESD;

	// Use this for initialization
	void Start () {
        p = new GameObject[2];
        p[0] = GameObject.Find("Player1");
        p[1] = GameObject.Find("Player2");

        AM_SR = GetComponent<SpriteRenderer>();
        FindObjectOfType<AudioManager>().Play("Detected");

        for (int i = 0; i < p.Length; i++)
        {
            p[i].GetComponent<P_controls>().spotted = true;
            p[i].GetComponent<P_controls>().StopGameControl = true;
            p[i].GetComponent<P_Death>().StartCoroutine("Dead");
        }
        
        

        if(AM_enemy != null)
        {
            AM_ESD = AM_enemy.GetComponent<E_Sound_Detection>();

            AM_ESD.EM_isSpawned = true;
            AM_animator = AM_enemy.GetComponent<Animator>();

            if(AM_enemy.GetComponent<E_Movement>().isNurse)
            {
                AM_animator.Play("N_AlertAnim");
            }else
            {
                AM_animator.Play("D_AlertAnim");
            }
        }
        
        AM_myColor = new Color(AM_SR.color.r, AM_SR.color.g, AM_SR.color.b, AM_alpha);

        if(AM_enemy.GetComponent<E_Sound_Detection>().EM_isSpawned)
        {
            Destroy(AM_ESD.EM_DetectionMeter);
            AM_ESD.enabled = false;

            AM_enemy.GetComponent<E_Movement>().enabled = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < p.Length; i++)
        {
            p[i].GetComponent<P_controls>().StopGameControl = true;
        }

        if (AM_alpha > 0.1f)
        {
            AM_alpha -= 0.25f * Time.deltaTime;
        }else
        {
            if(AM_enemy != null)
            {
                AM_ESD.EM_isSpawned = false;
            }
            Destroy(gameObject);
        }

        AM_myColor = new Color(AM_SR.color.r, AM_SR.color.g, AM_SR.color.b, AM_alpha);
        AM_SR.color = AM_myColor;

	}
}
