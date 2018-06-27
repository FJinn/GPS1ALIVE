using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlIntoVent : MonoBehaviour {

    // player
    public GameObject[] p;

    // cache sprite renderer color
    Color spriteR;

    private void Awake()
    {
        p = new GameObject[2];
        p[0] = GameObject.FindGameObjectWithTag("Player");
        p[1] = GameObject.FindGameObjectWithTag("Player2");
        spriteR = GetComponent<SpriteRenderer>().color;
    }

    void Update()
    {

            if (p[0].GetComponent<P_avoidEnemyVent>().firstTap || p[1].GetComponent<P_avoidEnemyVent>().firstTap)
            {
                spriteR.a = 0.7f;
                GetComponent<SpriteRenderer>().color = spriteR;
            }
            else if (!p[0].GetComponent<P_avoidEnemyVent>().firstTap && !p[1].GetComponent<P_avoidEnemyVent>().firstTap)
            {
                spriteR.a = 1f;
                GetComponent<SpriteRenderer>().color = spriteR;
            }

    }
}
