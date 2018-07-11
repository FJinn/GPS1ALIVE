using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class El_Manager : MonoBehaviour
{

    public GameObject[] p ;
    private GameObject p_collided;

    private int p_Counts = 0;
    // Check if there is player on elevator.
    private bool onEl = false;
    // To register which player on elevator and use it to prevent the p_Counts keeps increasing when updating
    private GameObject p_OnEl;
    public GameObject Box;
    private SliderJoint2D mySlider;

    private float posX = 310f;
    private float posY = -60f;


    // Use this for initialization
    void Start()
    {
        // So box will not push it down.
        //GetComponent<SliderJoint2D>().enabled = false;
        mySlider = GetComponent<SliderJoint2D>();
        mySlider.breakForce = Mathf.Infinity;
        p = new GameObject[2];
        p[0] = GameObject.FindGameObjectWithTag("Player");
        p[1] = GameObject.FindGameObjectWithTag("Player2");
        //InitialDestination = transform.position;
        //targetPosition = FinalDestination.position;
    }
    
    public void callElevatorCheck(GameObject playerCollider)
    {
        if (!onEl && p_OnEl != playerCollider)
        {
            p_OnEl = playerCollider;
            p_Counts++;
            onEl = true;
            //p_collided = p[i];
            mySlider.enabled = true;
            mySlider.enableCollision = true;

            if(p_Counts == 1)
            {
                mySlider.connectedAnchor = new Vector2(posX, posY);
                onEl = false;
            }
        }
    }
    


    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < p.Length; i++)
        {
            if (GetComponent<BoxCollider2D>().IsTouching(p[i].GetComponent<BoxCollider2D>()) && !onEl && p_OnEl != p[i])
            {
                p_OnEl = p[i];
                p_Counts++;
                onEl = true;
                //p_collided = p[i];
                mySlider.enabled = true;
                mySlider.enableCollision = true;
            }
        }

        if (p_Counts == 1)
        {
            //If 1 Player got onto the elevator, the elevator will go to destined distance and won't get back up.
            // Enable slider and thus, the limit will be used.
            // And the box will not push it down before it is enabled.
            
            mySlider.connectedAnchor = new Vector2(posX,posY);
            
            if (!GetComponent<BoxCollider2D>().IsTouching(p_OnEl.GetComponent<BoxCollider2D>())){
                onEl = false;
            }

            // Is this really needed?
            // transform.position = Vector3.MoveTowards(transform.position, new Vector2(InitialDestination.x, InitialDestination.y - 10f), El_Speed * Time.deltaTime);
            //InitialDestination.y = FinalDestination.y;
        }
        else if (p_Counts >= 2)
        {
            //If 2 Player got onto the elevator, the elevator will go straight down until collide with a ground
            mySlider.breakForce = 5;
            for(int i =0; i< p.Length; i ++)
            {
                Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), p[i].GetComponent<BoxCollider2D>());
            }
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), Box.GetComponent<BoxCollider2D>());
        }
    }
}
