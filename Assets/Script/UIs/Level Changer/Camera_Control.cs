﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Control : MonoBehaviour {

    private Vector2 velocity;

    public float smoothX;
    public float smoothY;
    
    public GameObject player;
    public GameObject player2;

    private float midpointX;
    private float midpointY;
    private float distanceTwoPlayer;
    public float SmoothenSize;
    private float InitSize;

    public int roomCameraInt;
    public GameObject[] roomCameraFocusObject;
    public int[] roomCameraSize;
    public Camera currentCamera;

    public bool targetRoom = false;
    public bool targetRoomsMiddle = false;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        player2 = GameObject.FindGameObjectWithTag("Player2");

        InitSize = currentCamera.orthographicSize;
    }
	
    float getDistance()
    {
        distanceTwoPlayer = Vector2.Distance(player2.transform.position, player.transform.position);

        return distanceTwoPlayer;
    }
    
	// Update is called once per frame
	void Update () {
        midpointX = (player.transform.position.x + player2.transform.position.x) / 2;
        midpointY = (player.transform.position.y + player2.transform.position.y) / 2;

        float xPos = Mathf.SmoothDamp(transform.position.x, midpointX, ref velocity.x, smoothX);
        float yPos = Mathf.SmoothDamp(transform.position.y, midpointY, ref velocity.y, smoothX);

        transform.position = new Vector3(xPos, yPos, transform.position.z);


        // too buggy for the function below
        //float SmoothSize = Mathf.SmoothDamp(InitSize, InitSize +  distance(), ref velocitySize, SmoothenSize); 
        // currentCamera.orthographicSize = InitSize + + Mathf.Sqrt(distance() / 1.5f);
        currentCamera.orthographicSize = InitSize + (Mathf.Sqrt(getDistance()));
        /*
        if (!targetRoom)
            {
                midpointX = (player.transform.position.x + player2.transform.position.x) / 2;
                midpointY = (player.transform.position.y + player2.transform.position.y) / 2;

                float xPos = Mathf.SmoothDamp(transform.position.x, midpointX, ref velocity.x, smoothX);
                float yPos = Mathf.SmoothDamp(transform.position.y, midpointY, ref velocity.y, smoothX);

                transform.position = new Vector3(xPos, yPos, transform.position.z);


                // too buggy for the function below
                //float SmoothSize = Mathf.SmoothDamp(InitSize, InitSize +  distance(), ref velocitySize, SmoothenSize); 
                // currentCamera.orthographicSize = InitSize + + Mathf.Sqrt(distance() / 1.5f);
                currentCamera.orthographicSize = InitSize + (Mathf.Sqrt(getDistance()));
            }else if (targetRoom)
            {
                transform.position = roomCameraFocusObject[roomCameraInt].transform.position;
                currentCamera.orthographicSize = roomCameraSize[roomCameraInt];
            }
        
        */
    }
}
