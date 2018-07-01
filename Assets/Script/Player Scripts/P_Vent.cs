using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class P_Vent : MonoBehaviour {

    public bool onVent = false;
    public bool onCrack = false;
    private bool crouched = false;

    public BoxCollider2D BoxColliderOrigin;
    private int iniSortingLayer;
    public LayerMask bypassEnemyMask;

    public bool preventColliderErase;

    private GameObject passthroughWall;

    void Start()
    {

        passthroughWall = GameObject.Find("WallwithVents");

        iniSortingLayer = GetComponent<SpriteRenderer>().sortingOrder;
        BoxColliderOrigin = GetComponent<BoxCollider2D>();
        
    }

    IEnumerator ResetCollider()
    {
        Destroy(BoxColliderOrigin);
        yield return new WaitForSeconds(0.05f);
        BoxColliderOrigin = gameObject.AddComponent<BoxCollider2D>() as BoxCollider2D;

        var foundEnemies = Physics2D.OverlapCircleAll(transform.position, 150f, bypassEnemyMask);
        for (int k = 0; k < foundEnemies.Length; k++)
        {
            Physics2D.IgnoreCollision(BoxColliderOrigin, foundEnemies[k]);
        }
        
        if(gameObject.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(BoxColliderOrigin, GameObject.FindGameObjectWithTag("Player2").GetComponent<P_Vent>().BoxColliderOrigin);
        }
        if(gameObject.CompareTag("Player2"))
        {
            Physics2D.IgnoreCollision(BoxColliderOrigin, GameObject.FindGameObjectWithTag("Player").GetComponent<P_Vent>().BoxColliderOrigin);
        }

        if(onVent == false)
        {
            Physics2D.IgnoreCollision(BoxColliderOrigin, passthroughWall.GetComponent<TilemapCollider2D>(), false);
        }
    }

    // Update is called once per frame
    void Update () {

		if(onVent == true && !crouched){
            crouched = true;
           // GetComponent<BoxCollider2D>().isTrigger = true;
            GetComponent<Rigidbody2D>().gravityScale = 0;
            GetComponent<SpriteRenderer>().sortingOrder = iniSortingLayer - 2;
            GetComponent<P_controls>().onVent = true;

        }
        else if (onVent == false && crouched)
        {
            crouched = false;
            GetComponent<P_controls>().onVent = false;
            //GetComponent<BoxCollider2D>().isTrigger = false;
            GetComponent<Rigidbody2D>().gravityScale = 10;
            GetComponent<SpriteRenderer>().sortingOrder = iniSortingLayer;
            
        }

        /*
        if(crouched && onVent){
            if (gameObject.GetComponent<Rigidbody2D>().velocity.x > 2f || gameObject.GetComponent<Rigidbody2D>().velocity.x < -2f)
            {
                GetComponent<P_controls>().anim.Play(GetComponent<P_controls>().animList[3]);
            }
            else
            {
                GetComponent<P_controls>().anim.Play(GetComponent<P_controls>().animList[2]);
            }
        }
        */
    }

}
