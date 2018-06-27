using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_outLadder : MonoBehaviour {

    public GameObject[] Players;

    private void Start()
    {
        Players = new GameObject[2];
        Players[0] = GameObject.FindGameObjectWithTag("Player");
        Players[1] = GameObject.FindGameObjectWithTag("Player2");
    }

    private void OnTriggerStay2D(Collider2D player)
    {
        for(int i=0; i<Players.Length; i++){ 
            if (Input.GetKey(Players[i].GetComponent<P_controls>().KeyLeft))
            {
                Players[i].GetComponent<P_controls>().OnLadder = false;
            }
            else if (Input.GetKey(Players[i].GetComponent<P_controls>().KeyRight))
            {
                Players[i].GetComponent<P_controls>().OnLadder = false;
            }

        }
    }
}
