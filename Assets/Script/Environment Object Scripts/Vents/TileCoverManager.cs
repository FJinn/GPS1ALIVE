using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCoverManager : MonoBehaviour
{

    public GameObject[] p;
    private Renderer c;
    private Color myColor;

    // Use this for initialization
    void Start()
    {
        p = new GameObject[2];
        p[0] = GameObject.FindGameObjectWithTag("Player");
        p[1] = GameObject.FindGameObjectWithTag("Player2");
        c = GetComponent<Renderer>();
        myColor = c.material.color;

    }

    // Update is called once per frame
    void Update()
    {
        if (p[0].GetComponent<P_Vent>().onVent == true || p[1].GetComponent<P_Vent>().onVent == true)
        {
            c.material.color = new Color(myColor.r, myColor.g, myColor.b, 0.5f);
        }
        else if (p[0].GetComponent<P_Vent>().onVent == false || p[1].GetComponent<P_Vent>().onVent == false){

            c.material.color = new Color(myColor.r, myColor.g, myColor.b, 1f);
        }
    }
}
