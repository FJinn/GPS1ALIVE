using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class M_outLadder : MonoBehaviour {

    public GameObject[] Players;
    public bool check;
    private GameObject[] walls;

    private void Start()
    {
        Players = new GameObject[2];

        Players[0] = GameObject.FindGameObjectWithTag("Player");
        Players[1] = GameObject.FindGameObjectWithTag("Player2");

        walls = GameObject.FindGameObjectsWithTag("Walls");

    }
    

    private void OnTriggerStay2D(Collider2D player)
    {
        if(player.CompareTag("Player") || player.CompareTag("Player2"))
        {
            if (Input.GetKey(player.GetComponent<P_controls>().KeyLeft))
            {
                player.GetComponent<P_controls>().OnLadder = false;
            }
            else if (Input.GetKey(player.GetComponent<P_controls>().KeyRight))
            {
                player.GetComponent<P_controls>().OnLadder = false;
            }
        }
    }
    
  
}
