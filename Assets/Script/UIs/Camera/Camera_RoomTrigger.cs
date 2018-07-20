using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_RoomTrigger : MonoBehaviour {

    private bool p1Triggered = false;
    private bool p2Triggered = false;
    private bool triggerOnce = false;
    private GameObject[] players;

    public int CameraIndexTrigger;
    public bool ActiveDynamic;
    public GameObject[] ActiveBlocker;
    public bool DisableActiveBlocker;
    public bool KillEnemy;
    public GameObject[] Enemies;



    [Header("For dynamic movement!")]
    public bool changeCameraSize = false;
    public int changeSize = 0;
    
    
    private GameObject MainCamera;

	// Use this for initialization
	void Start () {
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        players = new GameObject[2];
        players[0] = GameObject.FindGameObjectWithTag("Player");
        players[1] = GameObject.FindGameObjectWithTag("Player2");
	}
	
	// Update is called once per frame
	void Update () {
        if (p1Triggered && p2Triggered)
        {
            if (ActiveBlocker.Length != 0)
            {
                for(int i = 0; i < ActiveBlocker.Length; i ++)
                {
                    if(!DisableActiveBlocker)
                    {
                        ActiveBlocker[i].GetComponent<BoxCollider2D>().isTrigger = false;
                    }else
                    {
                        ActiveBlocker[i].GetComponent<BoxCollider2D>().isTrigger = true;
                    }
                }
            }
            MainCamera.GetComponent<Camera_Control>().roomCameraInt = CameraIndexTrigger;

            Invoke("ResumeCamera", 1f);

            if(KillEnemy)
            {
               for(int i =0;i < Enemies.Length;i++)
                {
                    if(Enemies[i] != null)
                    {
                        Destroy(Enemies[i]);
                    }
                }
            }

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

    void ResumeCamera()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<P_controls>().CameraStarted = true;
            players[i].GetComponent<P_controls>().StopGameControl = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == players[0].GetComponent<BoxCollider2D>() && !p1Triggered)
        {
            p1Triggered = true;
        }
        else if(collision == players[1].GetComponent<BoxCollider2D>() && !p2Triggered)
        {
            p2Triggered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == players[0].GetComponent<BoxCollider2D>() && p1Triggered)
        {
           p1Triggered = false;

        }
        else if (collision == players[1].GetComponent<BoxCollider2D>() && p2Triggered)
        {
            p2Triggered = false;
        }

    }
}
