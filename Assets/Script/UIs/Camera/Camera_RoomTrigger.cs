using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_RoomTrigger : MonoBehaviour {

    private bool p1Triggered = false;
    private bool p2Triggered = false;
    private bool triggerOnce = false;

    public int CameraIndexTrigger;
    public bool ActiveDynamic;
    public GameObject[] ActiveBlocker;

    [Header("For dynamic movement!")]
    public bool changeCameraSize = false;
    public int changeSize = 0;
    
    
    private GameObject MainCamera;

	// Use this for initialization
	void Start () {
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
        if (p1Triggered && p2Triggered)
        {
            if (ActiveBlocker.Length != 0)
            {
                for(int i = 0; i < ActiveBlocker.Length; i ++)
                {
                    ActiveBlocker[i].GetComponent<BoxCollider2D>().isTrigger = false;
                }
            }
            MainCamera.GetComponent<Camera_Control>().roomCameraInt = CameraIndexTrigger;

            if(!triggerOnce)
            {
                triggerOnce = true;
                MainCamera.GetComponent<Camera_Control>().cameraSizeSmoothTimer = 0;
            }
            if (ActiveDynamic)
            {
                MainCamera.GetComponent<Camera_Control>().targetRoom = false;
            }
            else
            {
                MainCamera.GetComponent<Camera_Control>().targetRoom = true;
            }

            if(changeCameraSize)
            {
                MainCamera.GetComponent<Camera_Control>().InitSize = changeSize;
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !p1Triggered)
        {
            p1Triggered = true;
        }
        else if(collision.CompareTag("Player2") && !p2Triggered)
        {
            p2Triggered = true;
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
