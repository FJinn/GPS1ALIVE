using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Manager : MonoBehaviour {
    
	private GameObject[] p;
    private GameObject[] e;

	void Awake(){
        p = new GameObject[2];
        e = GameObject.FindGameObjectsWithTag("Enemy");
	}

	// Use this for initialization
	void Start () {
		p[0] = GameObject.FindGameObjectWithTag ("Player");
        p[1] = GameObject.FindGameObjectWithTag ("Player2");
        for(int i =0; i < p.Length; i++)
        {
            if (p[i].GetComponent<P_throw>().onThrow)
            {
                GetComponent<S_Movement>().enabled = true;
               
            }
        }
		
		Invoke ("onTrigger", 0.3f); // to prevent stone collide with player //!!!!!!!!! if throw at bottom platform, it will go through since 0.3s trigger
	}

	void onTrigger(){
		GetComponent<BoxCollider2D> ().isTrigger = false;
	}

	// Update is called once per frame
	void Update () {
        for(int i =0;i < p.Length;i ++)
        {
            if (GetComponent<BoxCollider2D>().IsTouching(p[i].GetComponent<BoxCollider2D>()) && Input.GetKeyDown(p[i].GetComponent<P_controls>().KeyUse) && p[i].GetComponent<P_throw>().spawnStone == 0)
            {
                p[i].GetComponent<P_throw>().spawnStone = 1;
                GetComponent<BoxCollider2D>().isTrigger = true;
                Destroy(gameObject);
            }
        }

        // ignore collision with enemy
        for(int i=0; i<e.Length; i++)
        {
            Physics2D.IgnoreCollision(this.GetComponent<BoxCollider2D>(), e[i].GetComponent<BoxCollider2D>());
        }
	}

}
