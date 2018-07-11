using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_AM_Destroy : MonoBehaviour {

    private Color AM_myColor;
    private float AM_alpha = 1f;
    public GameObject AM_enemy;
    public GameObject[] p;

	// Use this for initialization
	void Start () {
        p = new GameObject[2];
        p[0] = GameObject.FindGameObjectWithTag("Player");
        p[1] = GameObject.FindGameObjectWithTag("Player2");

        p[0].GetComponent<P_Death>().StartCoroutine("Dead");
        
        for(int i =0;i < p.Length;i++)
        {
            p[i].GetComponent<P_controls>().StopGameControl = true;
        }

        if(AM_enemy != null)
        {
            AM_enemy.GetComponent<E_Sound_Detection>().EM_isSpawned = true;
        }
        
        AM_myColor = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, AM_alpha);

        if(AM_enemy.GetComponent<E_Sound_Detection>().EM_isSpawned)
        {
            Destroy(AM_enemy.GetComponent<E_Sound_Detection>().EM_DetectionMeter);
        }
    }
	
	// Update is called once per frame
	void Update () {


        if (AM_alpha > 0.1f)
        {
            AM_alpha -= 0.25f * Time.deltaTime;
        }else
        {
            if(AM_enemy != null)
            {
                AM_enemy.GetComponent<E_Sound_Detection>().EM_isSpawned = false;
            }
            Destroy(gameObject);
        }

        AM_myColor = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, AM_alpha);
        GetComponent<SpriteRenderer>().color = AM_myColor;

	}
}
