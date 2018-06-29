using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_RoomTrigger : MonoBehaviour {

    private bool p1Triggered = false;
    private bool p2Triggered = false;

    public int CameraIndexTrigger;

    public GameObject AllowPass;
    private GameObject MainCamera;

	// Use this for initialization
	void Start () {
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
		if(p1Triggered && p2Triggered)
        {
            MainCamera.GetComponent<Camera_Control>().roomCameraInt = CameraIndexTrigger;
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !p1Triggered)
        {
            p1Triggered = true;
            MainCamera.GetComponent<Camera_Control>().targetRoomsMiddle = true;

        }
        else if(collision.CompareTag("Player2") && !p2Triggered)
        {
            p2Triggered = true;
            MainCamera.GetComponent<Camera_Control>().targetRoomsMiddle = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && p1Triggered)
        {
           p1Triggered = false;

        }
        else if (collision.CompareTag("Player2") && p2Triggered)
        {
            p2Triggered = false;
        }

    }
}
