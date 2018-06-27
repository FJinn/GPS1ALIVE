using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Vent : MonoBehaviour {

    public bool onVent = false;
    public bool onCrack = false;
    private bool crouched = false;

    private int iniSortingLayer;

    private Vector2 IniColSize;
    private Vector2 HalfColSize;
    private Vector2 CrouchReposition;
    private float xPos;
    
    void Start()
    {
        // Get initial collider size
        IniColSize = GetComponent<BoxCollider2D>().size;
        // Half its height
        HalfColSize = new Vector2(GetComponent<BoxCollider2D>().size.x, GetComponent<BoxCollider2D>().size.y / 2);
        iniSortingLayer = GetComponent<SpriteRenderer>().sortingOrder;

    }

    // Update is called once per frame
    void Update () {
        xPos = transform.position.x;

		if(onVent == true && !crouched){
            CrouchReposition = new Vector2(xPos, transform.position.y - HalfColSize.y);
            crouched = true;
            GetComponent<BoxCollider2D>().size = HalfColSize;
            transform.position = CrouchReposition;
           // GetComponent<BoxCollider2D>().isTrigger = true;
            GetComponent<Rigidbody2D>().gravityScale = 0;
            GetComponent<SpriteRenderer>().sortingOrder = iniSortingLayer - 1;
            GetComponent<P_controls>().onVent = true;

        }
        else if (onVent == false && crouched)
        {
            GetComponent<BoxCollider2D>().size = IniColSize;
            crouched = false;
            GetComponent<P_controls>().onVent = false;
            //GetComponent<BoxCollider2D>().isTrigger = false;
            GetComponent<Rigidbody2D>().gravityScale = 2;
            GetComponent<SpriteRenderer>().sortingOrder = iniSortingLayer;
        }

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
    }
}
