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
            if (Input.GetKey(player.GetComponent<P_controls>().KeyLeft))
            {

                for (int i = 0; i < walls.Length; i++)
            {
                Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), walls[i].GetComponent<TilemapCollider2D>(), false);
            }
                player.GetComponent<P_controls>().OnLadder = false;
            }
            else if (Input.GetKey(player.GetComponent<P_controls>().KeyRight))
            {

                for (int i = 0; i < walls.Length; i++)
            {
                Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), walls[i].GetComponent<TilemapCollider2D>(), false);
            }
               player.GetComponent<P_controls>().OnLadder = false;
            }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        for (int i = 0; i < walls.Length; i++)
        {
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), walls[i].GetComponent<TilemapCollider2D>(),false);
        }

    }
  
}
