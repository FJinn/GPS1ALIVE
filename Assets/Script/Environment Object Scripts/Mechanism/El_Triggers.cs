using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class El_Triggers : MonoBehaviour {

    public GameObject Elevator;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player") || collision.collider.CompareTag("Player2"))
        {
            Debug.Log("Hit box");
            Elevator.GetComponent<El_Manager>().callElevatorCheck(collision.collider.gameObject);
        }
    }
    
}
