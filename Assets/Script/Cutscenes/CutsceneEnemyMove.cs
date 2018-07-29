using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneEnemyMove : MonoBehaviour {

    // Use this for initialization
    [SerializeField] GameObject[] enemyToMove;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") || collision.CompareTag("Player2"))
        {
            for(int i =0;i< enemyToMove.Length; i++)
            {
                enemyToMove[i].GetComponent<E_Movement>().enabled = true;
            }
        }
    }
}
