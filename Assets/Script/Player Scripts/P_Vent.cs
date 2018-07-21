using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class P_Vent : MonoBehaviour {

    public bool onVent = false;
    public bool onCrack = false;
    public bool exitVent = false;
    private bool crouched = false;

    public BoxCollider2D BoxColliderOrigin;
    private Rigidbody2D rg2b;
    private SpriteRenderer SR;
    private int iniSortingLayer;
    public LayerMask bypassEnemyMask;

    public bool preventColliderErase;

    private GameObject[] passthroughWall;


    void Start()
    {
        rg2b = GetComponent<Rigidbody2D>();
        passthroughWall = GameObject.FindGameObjectsWithTag("Walls");
        
        iniSortingLayer = GetComponent<SpriteRenderer>().sortingOrder;
        BoxColliderOrigin = GetComponent<BoxCollider2D>();
        SR = GetComponent<SpriteRenderer>();
    }

    public void ResetCollisions()
    {
        var foundEnemies = Physics2D.OverlapCircleAll(transform.position, 50000f, bypassEnemyMask);
        for (int k = 0; k < foundEnemies.Length; k++)
        {
            Physics2D.IgnoreCollision(BoxColliderOrigin, foundEnemies[k]);
        }
        

        if (onVent == false)
        {

            for (int i = 0; i < passthroughWall.Length; i++)
            {
                if (passthroughWall[i].GetComponent<TilemapCollider2D>() != null)
                {
                    Physics2D.IgnoreCollision(BoxColliderOrigin, passthroughWall[i].GetComponent<TilemapCollider2D>(), false);
                }
                else if (passthroughWall[i].GetComponent<BoxCollider2D>() != null)
                {
                    Physics2D.IgnoreCollision(BoxColliderOrigin, passthroughWall[i].GetComponent<BoxCollider2D>(), false);
                }
            }
        }

        if (CompareTag("Player"))
        {
            GameObject tempObject = GameObject.FindGameObjectWithTag("Player2");
            if(tempObject.GetComponent<P_Vent>().BoxColliderOrigin != null)
            {
                Physics2D.IgnoreCollision(BoxColliderOrigin, tempObject.GetComponent<P_Vent>().BoxColliderOrigin);
            }
            
        }
        else if(CompareTag("Player2"))
        {
            GameObject tempObject = GameObject.FindGameObjectWithTag("Player");
            if (tempObject.GetComponent<P_Vent>().BoxColliderOrigin != null)
            {
                Physics2D.IgnoreCollision(BoxColliderOrigin, tempObject.GetComponent<P_Vent>().BoxColliderOrigin);
            }
        }

    }

    // Update is called once per frame
    void Update () {

		if(onVent == true && !crouched){
            crouched = true;
           // GetComponent<BoxCollider2D>().isTrigger = true;
            rg2b.gravityScale = 0;
            rg2b.velocity = new Vector2(rg2b.velocity.x, 0);
            SR.sortingOrder = iniSortingLayer - 4;
            GetComponent<P_controls>().onVent = true;

            for (int i = 0; i < passthroughWall.Length; i++)
            {
                if(passthroughWall[i].GetComponent<TilemapCollider2D>() != null)
                {
                    Physics2D.IgnoreCollision(BoxColliderOrigin, passthroughWall[i].GetComponent<TilemapCollider2D>());
                }else if(passthroughWall[i].GetComponent<BoxCollider2D>() != null)
                {
                    Physics2D.IgnoreCollision(BoxColliderOrigin, passthroughWall[i].GetComponent<BoxCollider2D>());
                }
            }
        }
        else if (onVent == false && crouched)
        {
            crouched = false;         
            GetComponent<P_controls>().onVent = false;
            rg2b.gravityScale = 10;
            SR.sortingOrder = iniSortingLayer;
            
        }
        
    }
}
