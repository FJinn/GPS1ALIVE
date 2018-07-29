using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneElevator : MonoBehaviour {

    private bool player1;
    private bool player2;
    public bool triggerOnce = false;
    [SerializeField] private GameObject Elevator;
    [SerializeField] private GameObject CloseDoor;

    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            P_controls tempControl;
            tempControl = collision.GetComponent<P_controls>();

            tempControl.KeyLeft = KeyCode.None;
            tempControl.KeyRight = KeyCode.None;
            tempControl.KeyDown = KeyCode.None;
            tempControl.KeyUp = KeyCode.None;
            tempControl.KeyUse = KeyCode.None;

            player1 = true;
        }
        if (collision.CompareTag("Player2"))
        {
            P_controls tempControl;
            tempControl = collision.GetComponent<P_controls>();

            tempControl.KeyLeft = KeyCode.None;
            tempControl.KeyRight = KeyCode.None;
            tempControl.KeyDown = KeyCode.None;
            tempControl.KeyUp = KeyCode.None;
            tempControl.KeyUse = KeyCode.None;

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
       //     FindObjectOfType<AudioManager>().Play("EscapeBGM");
            FindObjectOfType<Camera_Control>().GetComponent<Camera_Control>().endingCamera = true;
            FindObjectOfType<Camera_Control>().GetComponent<Camera_Control>().cameraSizeSmoothTimer = 0;
            CloseDoor.GetComponent<Animator>().Play("GD_Open");
            Elevator.GetComponent<M_Trigger>().Trigger();
        }
    }

}
