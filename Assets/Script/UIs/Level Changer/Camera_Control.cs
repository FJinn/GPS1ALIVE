using System.Collections;
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

    public GameObject enemy;
    private float distanceTwoPlayer;
    private float distancePlayerAndEnemy;
    private float midPointPlayer;
    private float midPointEnemyX;
    private float midPointEnemyY;

    public float SmoothenSize;
    private float InitSize;
   
    public Camera currentCamera;
    

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        player2 = GameObject.FindGameObjectWithTag("Player2");
        enemy = GameObject.FindGameObjectWithTag("Enemy");

        InitSize = currentCamera.fieldOfView;
    }
	
    float getDistance()
    {
        distanceTwoPlayer = Vector2.Distance(player2.transform.position, player.transform.position);
        distancePlayerAndEnemy = Vector2.Distance(((player2.transform.position + player.transform.position)/2), enemy.transform.position);

        return distanceTwoPlayer;
        /*
        if (distancePlayerAndEnemy <= 5f)
        {
            return distancePlayerAndEnemy;
        }
        else
            return distanceTwoPlayer;
        */
        //return Mathf.Sqrt(Mathf.Pow((player2.transform.position.x - player.transform.position.x),2) + Mathf.Pow((player2.transform.position.y - player.transform.position.y),2));
    }
    
	// Update is called once per frame
	void Update () {
        //midPointEnemyX = 
        

        midpointX = (player.transform.position.x + player2.transform.position.x) / 2;
        midpointY = (player.transform.position.y + player2.transform.position.y) / 2;

        float xPos = Mathf.SmoothDamp(transform.position.x, midpointX, ref velocity.x, smoothX);
        float yPos = Mathf.SmoothDamp(transform.position.y, midpointY, ref velocity.y, smoothX);

        transform.position = new Vector3(xPos, yPos, transform.position.z);


        // too buggy for the function below
        //float SmoothSize = Mathf.SmoothDamp(InitSize, InitSize +  distance(), ref velocitySize, SmoothenSize); 
        // currentCamera.orthographicSize = InitSize + + Mathf.Sqrt(distance() / 1.5f);
        currentCamera.fieldOfView = InitSize + (Mathf.Sqrt(getDistance()) * 4f);
    }
}
