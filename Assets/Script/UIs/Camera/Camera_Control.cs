using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Control : MonoBehaviour {

    private Vector2 velocity;

    public float smoothX;
    
    public GameObject player;
    public GameObject player2;

    private float midpointX;
    private float midpointY;
    private float distanceTwoPlayer;
    public float SmoothenSize;
    public float InitSize;

    public int roomCameraInt;
    public GameObject[] roomCameraFocusObject;
    public float[] roomCameraSize;
    public Camera currentCamera;
    private float cameraSmooth;
    public float cameraSizeSmoothTimer;

    public bool targetRoom = false;
    [HideInInspector] public bool endingCamera;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        player2 = GameObject.FindGameObjectWithTag("Player2");

        InitSize = currentCamera.orthographicSize;
        roomCameraInt = GameObject.FindGameObjectWithTag("CheckpointManager").GetComponent<Checkpoint>().CheckpointCamera;
        transform.position = roomCameraFocusObject[roomCameraInt].transform.position;
    }
	
    float getDistance()
    {
        distanceTwoPlayer = Vector2.Distance(player2.transform.position, player.transform.position);

        return distanceTwoPlayer;
    }
    
	// Update is called once per frame
	void Update () {
        
        if(endingCamera)
        {
            cameraSmooth = Mathf.Lerp(roomCameraSize[8], 15, 10f);

            midpointX = (player.transform.position.x + player2.transform.position.x) / 2;
            midpointY = (player.transform.position.y + player2.transform.position.y) / 2;

            float xPos = Mathf.SmoothDamp(transform.position.x, midpointX, ref velocity.x, smoothX);
            float yPos = Mathf.SmoothDamp(transform.position.y, midpointY, ref velocity.y, smoothX);

            transform.position = new Vector3(xPos, yPos, transform.position.z);
            
            currentCamera.orthographicSize = cameraSmooth;

            return;
        }

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
                currentCamera.orthographicSize = InitSize + (Mathf.Sqrt(getDistance()) * 2.4f);
            }else if (targetRoom)
            {
                
                float xPos = Mathf.SmoothDamp(transform.position.x, roomCameraFocusObject[roomCameraInt].transform.position.x, ref velocity.x, smoothX);
                float yPos = Mathf.SmoothDamp(transform.position.y, roomCameraFocusObject[roomCameraInt].transform.position.y, ref velocity.y, smoothX);
                transform.position = new Vector3(xPos,yPos,transform.position.z);

                if(cameraSizeSmoothTimer <= 10f)
                {
                    cameraSizeSmoothTimer += 2f * Time.deltaTime;
                }

                if(roomCameraInt == 0)
                {
                currentCamera.orthographicSize = roomCameraSize[0];
                }else
                {
                    cameraSmooth = Mathf.Lerp(roomCameraSize[roomCameraInt - 1], roomCameraSize[roomCameraInt], cameraSizeSmoothTimer);

                    currentCamera.orthographicSize = cameraSmooth;
                  }
            }
        
        
    }
}
