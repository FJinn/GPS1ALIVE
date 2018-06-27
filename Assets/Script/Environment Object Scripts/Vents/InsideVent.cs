using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideVent : MonoBehaviour {

    public GameObject[] p;
    private bool insideVent = false;

	// Use this for initialization
	void Start () {
        p = new GameObject[2];
        p[0] = GameObject.FindGameObjectWithTag("Player");
        p[1] = GameObject.FindGameObjectWithTag("Player2");
	}
	
	// Update is called once per frame
	void Update () {
        
		for(int i=0; i<p.Length; i++){
         //   if (insideVent)
         //   {
          //      p[i].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
          //  }

            if (GetComponent<BoxCollider2D>().IsTouching(p[i].GetComponent<BoxCollider2D>()) )// && !insideVent)
            {
                p[i].GetComponent<P_Vent>().onVent = true;
                p[i].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                //insideVent = true;
            }
            /*
            else if (!GetComponent<BoxCollider2D>().IsTouching(p[i].GetComponent<BoxCollider2D>()) && p[i].GetComponent<P_avoidEnemyVent>().firstTap && !p[i].GetComponent<P_Vent>().preventColliderErase)//&& insideVent)
            {
                p[i].GetComponent<P_Vent>().onVent = false;
                p[i].GetComponent<P_avoidEnemyVent>().firstTap = false;
                //insideVent = false;
            } 
            */
        }
	}

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.GetComponent<P_Vent>().onVent = false;
        collision.GetComponent<P_avoidEnemyVent>().firstTap = false;

        collision.GetComponent<P_Vent>().StartCoroutine("ResetCollider");
    }
}
