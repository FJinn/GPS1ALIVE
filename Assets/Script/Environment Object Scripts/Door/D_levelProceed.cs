using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_levelProceed : MonoBehaviour {

    public int levelIndex;

    public bool player1Enter;
    public bool player2Enter;

    private void Update()
    {
        Collider2D[] players = Physics2D.OverlapCircleAll(transform.position, 10f);
        for(int i =0;i<players.Length;i++)
        {
            if(players[i].CompareTag("Player"))
            {
                player1Enter = true;
            }
            if(players[i].CompareTag("Player2"))
            {
                player2Enter = true;
            }
        }
        
    }
}
