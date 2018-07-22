using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_levelProceed : MonoBehaviour {

    public int levelIndex;

    public bool player1Enter;
    public bool player2Enter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            player1Enter = true;
        }
        if(collision.CompareTag("Player2"))
        {
            player2Enter = true;
        }
    }
    
}
