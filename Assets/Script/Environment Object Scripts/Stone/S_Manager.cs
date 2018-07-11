using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Manager : MonoBehaviour {
    
	private GameObject[] p;
    private GameObject[] e;
    // bool to check distance
    bool onDistance1;
    bool onDistance2;
    // distance that player can pick up stone
    public float distance;

	void Awake(){
        p = new GameObject[2];
        e = GameObject.FindGameObjectsWithTag("Enemy");
        onDistance1 = false;
        onDistance2 = false;
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
/*        for(int i =0;i < p.Length;i ++)
        {
            if (GetComponent<BoxCollider2D>().IsTouching(p[i].GetComponent<BoxCollider2D>()) && Input.GetKeyDown(p[i].GetComponent<P_controls>().KeyUse) && p[i].GetComponent<P_throw>().spawnStone == 0)
            {
                p[i].GetComponent<P_throw>().spawnStone = 1;
                GetComponent<BoxCollider2D>().isTrigger = true;
                Destroy(gameObject);
            }
        }
*/
        // ignore collision with players
        for (int i = 0; i < p.Length; i++)
        {
            Physics2D.IgnoreCollision(this.GetComponent<BoxCollider2D>(), p[i].GetComponent<BoxCollider2D>());
        }

        // ignore collision with enemy
        for (int i=0; i<e.Length; i++)
        {
            Physics2D.IgnoreCollision(this.GetComponent<BoxCollider2D>(), e[i].GetComponent<BoxCollider2D>());
        }

        //! use distance instead collision box
        // check distance
        if(Mathf.Abs(transform.position.x - p[0].transform.position.x) <= distance)
        {
            onDistance1 = true;
        }
        else
        {
            onDistance1 = false;
        }
        if (Mathf.Abs(transform.position.x - p[1].transform.position.x) <= distance)
        {
            onDistance2 = true;
        }
        else
        {
            onDistance2 = false;
        }


        if (onDistance1 && Input.GetKeyDown(p[0].GetComponent<P_controls>().KeyUse) && p[0].GetComponent<P_throw>().spawnStone == 0)
        {
            p[0].GetComponent<P_throw>().spawnStone = 1;
            GetComponent<BoxCollider2D>().isTrigger = true;
            onDistance1 = false;
            Destroy(gameObject);
        }
        if (onDistance2 && Input.GetKeyDown(p[1].GetComponent<P_controls>().KeyUse) && p[1].GetComponent<P_throw>().spawnStone == 0)
        {
            p[1].GetComponent<P_throw>().spawnStone = 1;
            GetComponent<BoxCollider2D>().isTrigger = true;
            onDistance2 = false;
            Destroy(gameObject);
        }
    }

}
