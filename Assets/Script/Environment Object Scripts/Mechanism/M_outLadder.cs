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
    
    void ResetCollisions()
    {
        for (int i = 0; i < walls.Length; i++)
        {
            if (walls[i].GetComponent<TilemapCollider2D>() != null)
            {
                Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), walls[i].GetComponent<TilemapCollider2D>(), false);
            }
            if (walls[i].GetComponent<BoxCollider2D>() != null)
            {
                Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), walls[i].GetComponent<BoxCollider2D>(), false);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D player)
    {
            if (Input.GetKey(player.GetComponent<P_controls>().KeyLeft))
            {
               ResetCollisions();
                player.GetComponent<P_controls>().OnLadder = false;
            }
            else if (Input.GetKey(player.GetComponent<P_controls>().KeyRight))
            {
                ResetCollisions();
                player.GetComponent<P_controls>().OnLadder = false;
            }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ResetCollisions();

    }
  
}
