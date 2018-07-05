using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlIntoCrack : MonoBehaviour {

    // player
    public GameObject[] p;


    public bool teleport = false;
    public GameObject teleportLocation;

    // RESETING COLLISION BOX BOX COLLIDER 2D
    
    
    private void Awake() {
        p = new GameObject[2];
        p[0] = GameObject.FindGameObjectWithTag("Player");
        p[1] = GameObject.FindGameObjectWithTag("Player2");
    }

    void Update () {
        
		for(int i=0; i<p.Length; i++){
            if (Input.GetKeyDown(p[i].GetComponent<P_controls>().KeyUse) && !p[i].GetComponent<P_avoidEnemyVent>().firstTap && GetComponent<BoxCollider2D>().IsTouching(p[i].GetComponent<BoxCollider2D>())) {
                p[i].GetComponent<P_Vent>().onVent = true;
                p[i].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                p[i].GetComponent<P_avoidEnemyVent>().firstTap = true;

                if(teleport)
                {
                    p[i].transform.position = teleportLocation.transform.position;
                }
                
                p[i].GetComponent<P_Vent>().StartCoroutine("ResetCollider");
                /*
                var foundEnemies = Physics2D.OverlapCircleAll(p[i].transform.position, 150f, bypassEnemyMask);
                for(int k =0; k < foundEnemies.Length; k++)
                {
                    Physics2D.IgnoreCollision(p[i].GetComponent<P_Vent>().BoxColliderOrigin, foundEnemies[k]);
                }
                */
            }
            else if (Input.GetKeyDown(p[i].GetComponent<P_controls>().KeyUse) && p[i].GetComponent<P_avoidEnemyVent>().firstTap && GetComponent<BoxCollider2D>().IsTouching(p[i].GetComponent<BoxCollider2D>()))
            {
                p[i].GetComponent<P_Vent>().onVent = false;
                p[i].GetComponent<P_avoidEnemyVent>().firstTap = false;

                p[i].GetComponent<P_Vent>().StartCoroutine("ResetCollider");
                
                /*
                var foundEnemies = Physics2D.OverlapCircleAll(p[i].transform.position, 150f, bypassEnemyMask);
                for (int k = 0; k < foundEnemies.Length; k++)
                {
                    Physics2D.IgnoreCollision(p[i].GetComponent<P_Vent>().BoxColliderOrigin, foundEnemies[k],false);
                }
                */
            }
        }
	}
}
