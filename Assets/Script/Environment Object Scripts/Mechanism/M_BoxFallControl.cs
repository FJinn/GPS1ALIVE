using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_BoxFallControl : MonoBehaviour {

    // to prevent M_BoxRotate enable M_BoxPull
    public bool onSupport = true;

    GameObject[] p = new GameObject[2];
    P_Death[] death = new P_Death[2];
    GameObject temp;
    public GameObject blood;
    bool onKill = false;
    [Header("Position for blood spawning offset")]
    public float offsetX;
    public float offsetY;


    private SpriteRenderer SR;
    private Vector3 offset;

    Rigidbody2D rb2d;

    private void Start()
    {
        offset = new Vector3(offsetX, offsetY);
        rb2d = GetComponent<Rigidbody2D>();
        SR = GetComponent<SpriteRenderer>();
        p[0] = GameObject.FindGameObjectWithTag("Player");
        p[1] = GameObject.FindGameObjectWithTag("Player2");
        death[0] = p[0].GetComponent<P_Death>();
        death[1] = p[1].GetComponent<P_Death>();
    }

    void Update () {
/*
        if(p[0] == null)
        {
            p[0] = GameObject.FindGameObjectWithTag("Player");
            p[1] = GameObject.FindGameObjectWithTag("Player2");
            death[0] = p[0].GetComponent<P_Death>();
            death[1] = p[1].GetComponent<P_Death>();
        }
*/
        Physics2D.queriesStartInColliders = false;
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, Vector2.down, 5f);

        Debug.DrawRay(transform.position, Vector2.down * 5, Color.yellow);

        if (hit == false) {
            onSupport = false;
            onKill = true;
            GetComponent<M_BoxPull>().enabled = false;
        }
        else{
            onSupport = true;
            GetComponent<M_BoxPull>().enabled = true;
            if(!death[0].isDead && !death[1].isDead)
            {
                onKill = false;
            }
        }
        for (int i = 0; i < 2; i++)
        {
            if (death[i].killByBox && rb2d.velocity.y >= -0.5 && rb2d.velocity.y <= 0.5 && onKill)
            {
                SR.sortingOrder = 7;
                temp = (GameObject)Instantiate(blood, transform.position + offset, Quaternion.identity);
                death[i].forGroundToCheckBlood = temp.transform.position;
            //    temp.GetComponent<SpriteRenderer>().sortingOrder = 8;
                onKill = false;
                death[i].killByBox = false;
                death[i].groundOnBlood = true;
            }
        }
    }
}
