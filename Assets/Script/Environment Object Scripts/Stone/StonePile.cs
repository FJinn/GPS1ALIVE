using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StonePile : MonoBehaviour {

    private GameObject[] p = new GameObject[2];

    // Use this for initialization
    void Start () {
        p[0] = GameObject.FindGameObjectWithTag("Player");
        p[1] = GameObject.FindGameObjectWithTag("Player2");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        for (int i = 0; i < p.Length; i++)
        {
            if (Input.GetKeyDown(p[i].GetComponent<P_controls>().KeyUse) && p[i].GetComponent<P_throw>().throwStance == false)
            {
                p[i].GetComponent<P_throw>().spawnStone = 1;
            }
        }
    }
}
