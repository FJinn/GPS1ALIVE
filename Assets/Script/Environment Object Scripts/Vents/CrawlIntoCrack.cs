using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlIntoCrack : MonoBehaviour {

    // player
    public GameObject[] p;
    public LayerMask bypassEnemyMask;

    // private BoxCollider2D boxColliderFix;

    /* RESETING COLLISION BOX BOX COLLIDER 2D
    IEnumerator ResetCollider()
    {
        Destroy(boxColliderFix);
        yield return 0;
        boxColliderFix = gameObject.AddComponent<BoxCollider2D>() as BoxCollider2D;
    }
    */

    private void Awake() {
        p = new GameObject[2];
        p[0] = GameObject.FindGameObjectWithTag("Player");
        p[1] = GameObject.FindGameObjectWithTag("Player2");
    //    boxColliderFix = GetComponent<BoxCollider2D>();
    }

    void Update () {
        
		for(int i=0; i<p.Length; i++){
            if (Input.GetKeyDown(p[i].GetComponent<P_controls>().KeyUse) && !p[i].GetComponent<P_avoidEnemyVent>().firstTap && GetComponent<BoxCollider2D>().IsTouching(p[i].GetComponent<BoxCollider2D>())) {
                p[i].GetComponent<P_Vent>().onVent = true;
                p[i].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                p[i].GetComponent<P_avoidEnemyVent>().firstTap = true;



                var foundEnemies = Physics2D.OverlapCircleAll(p[i].transform.position, 150f, bypassEnemyMask);
                for(int k =0; k < foundEnemies.Length; k++)
                {
                    Physics2D.IgnoreCollision(p[i].GetComponent<BoxCollider2D>(), foundEnemies[k]);
                }

                //StartCoroutine("ResetCollider");
            }
            else if (Input.GetKeyDown(p[i].GetComponent<P_controls>().KeyUse) && p[i].GetComponent<P_avoidEnemyVent>().firstTap && GetComponent<BoxCollider2D>().IsTouching(p[i].GetComponent<BoxCollider2D>()))
            {
                p[i].GetComponent<P_Vent>().onVent = false;
                p[i].GetComponent<P_avoidEnemyVent>().firstTap = false;
                var foundEnemies = Physics2D.OverlapCircleAll(p[i].transform.position, 150f, bypassEnemyMask);
                for (int k = 0; k < foundEnemies.Length; k++)
                {
                    Physics2D.IgnoreCollision(p[i].GetComponent<BoxCollider2D>(), foundEnemies[k],false);
                }

                //StartCoroutine("ResetCollider");
            }
        }
	}
}
