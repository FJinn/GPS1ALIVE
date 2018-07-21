using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideVent : MonoBehaviour {

    private GameObject[] players;

    void Start()
    {
        players = new GameObject[2];
        players[0] = GameObject.FindGameObjectWithTag("Player");
        players[1] = GameObject.FindGameObjectWithTag("Player2");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other == players[0] || other == players[1])
        {
            other.GetComponent<P_Vent>().onVent = false;
            other.GetComponent<P_avoidEnemyVent>().firstTap = false;
            other.GetComponent<P_Vent>().ResetCollisions();
        }
    }
}
