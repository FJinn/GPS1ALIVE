using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class M_outLadder : MonoBehaviour {

    public GameObject[] Players;
    public bool check;


    private void Start()
    {
        Players = new GameObject[2];
        Players[0] = GameObject.FindGameObjectWithTag("Player");
        Players[1] = GameObject.FindGameObjectWithTag("Player2");
    }
    

    private void OnTriggerStay2D(Collider2D player)
    {
            if (Input.GetKey(player.GetComponent<P_controls>().KeyLeft))
            {
                Physics2D.IgnoreCollision(player.GetComponent<BoxCollider2D>(), GameObject.Find("Wall_Floors").GetComponent<TilemapCollider2D>() , false);
                player.GetComponent<P_controls>().OnLadder = false;
            }
            else if (Input.GetKey(player.GetComponent<P_controls>().KeyRight))
            {
                Physics2D.IgnoreCollision(player.GetComponent<BoxCollider2D>(), GameObject.Find("Wall_Floors").GetComponent<TilemapCollider2D>(), false);
                player.GetComponent<P_controls>().OnLadder = false;
            }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Physics2D.IgnoreCollision(collision.GetComponent<BoxCollider2D>(), GameObject.Find("Wall_Floors").GetComponent<TilemapCollider2D>(), false);

    }
  
}
