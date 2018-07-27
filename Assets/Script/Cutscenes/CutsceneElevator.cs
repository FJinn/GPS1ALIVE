using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneElevator : MonoBehaviour {

    private bool player1;
    private bool player2;
    private bool triggerOnce = false;
    [SerializeField] private GameObject Elevator;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            player1 = true;
        }
        if (collision.CompareTag("Player2"))
        {
            player2 = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            player1 = false ;
        }
        if (collision.CompareTag("Player2"))
        {
            player2 = false;
        }
    }

    private void Update()
    {
        if(player1 && player2 && !triggerOnce)
        {
            triggerOnce = true;
            Elevator.GetComponent<M_Trigger>().Trigger();
        }
    }

}
