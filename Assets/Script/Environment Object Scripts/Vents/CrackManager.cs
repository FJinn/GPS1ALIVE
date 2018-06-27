using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackManager : MonoBehaviour {

    public GameObject[] p;
    public GameObject firstTeleportPoint;
    public GameObject secondTeleportPoint;

    // Use this for initialization
    void Start()
    {
        p = new GameObject[2];
        p[0] = GameObject.FindGameObjectWithTag("Player");
        p[1] = GameObject.FindGameObjectWithTag("Player2");
    }

    // Update is called once per frame
    void Update () {
        
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        for (int i = 0; i < p.Length; i++)
        {
            collision = p[i].GetComponent<BoxCollider2D>();
            if (Input.GetKeyDown(p[i].GetComponent<P_controls>().KeyUse) && p[i].GetComponent<P_Vent>().onCrack == false /*&& GetComponent<BoxCollider2D>().IsTouching(p[i].GetComponent<BoxCollider2D>())*/)
            {
                p[i].GetComponent<P_Vent>().onVent = true;
                p[i].transform.position = secondTeleportPoint.transform.position;
                p[i].GetComponent<P_Vent>().onCrack = true;
            }
            else if (Input.GetKeyDown(p[i].GetComponent<P_controls>().KeyUse) && p[i].GetComponent<P_Vent>().onCrack == true /*&& GetComponent<BoxCollider2D>().IsTouching(p[i].GetComponent<BoxCollider2D>())*/)
        {
                p[i].transform.position = firstTeleportPoint.transform.position;
                p[i].GetComponent<P_Vent>().onVent = false;
                p[i].GetComponent<P_Vent>().onCrack = false;
            }
        }
    }
}
