﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P_throw : MonoBehaviour {
    
	public bool throwStance;
	public GameObject stone;
	public int spawnStone;
    public AudioSource audiosource;
    
    public GameObject s_Indicator;
    GameObject temp;
    bool spawned = false;
    Vector2 head;
    Vector2 barPosition;
    float tempYSize;
    Vector2 tempPos;
    public float s_indicatorHeight;
    public float s_indicatorWidth;
    public bool canThrow = false;
    public bool pickedUp = false;
    public bool throwed = false;
    public bool droppedStone = false;
    public bool canUseStonePile = false;

    GameObject[] player = new GameObject[2];
    GameObject otherStone;

    private P_controls control;

    public Camera cam;
    public Vector3 screenPosition;
    bool isThrowing = false;
    private P_keyHold keyHold;

    private Rigidbody2D rb2d;
    public bool inTheAir = false;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        control = GetComponent<P_controls>();
        // Offset Y
        tempYSize = GetComponent<BoxCollider2D>().size.y / 2;

        player[0] = GameObject.FindGameObjectWithTag("Player");
        player[1] = GameObject.FindGameObjectWithTag("Player2");

        keyHold = GetComponent<P_keyHold>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update () {
        if(rb2d.velocity.y >= -0.5f && rb2d.velocity.y <= 0.5f)
        {
            inTheAir = false;
        }
        else
        {
            inTheAir = true;
        }

        if(keyHold.keyNum == 0)
        {
            head = new Vector2(transform.position.x - 1.3f, transform.position.y + s_indicatorHeight);
        }
        else
        {
            head = new Vector2(transform.position.x + s_indicatorWidth, transform.position.y + s_indicatorHeight);
        }

        barPosition = new Vector2(transform.position.x, transform.position.y + 9f);
        if (spawnStone > 0)
        {
            if (!spawned)
            {
                spawned = true;
   //             temp = (GameObject)Instantiate(s_Indicator, head, Quaternion.identity);
            }
   //         temp.transform.position = head;
            throwStance = true;
        }
        else if (spawnStone == 0)
        {
   //         if(temp != null)
   //         {
   //             Destroy(temp);
    //        }
            spawned = false;
            throwStance = false;
        }

        tempPos = new Vector2(transform.position.x, transform.position.y + tempYSize);
        screenPosition = cam.WorldToScreenPoint(head);
        
        if (throwStance)
        {
            control.StopGameControl = true;
            FindObjectOfType<AudioManager>().Play("StonePickup");
            if (Input.GetKeyDown(control.KeyUse) && canThrow && !inTheAir)
            {
                throwing();
                //spawnStone = 1;// for testing purpose, infinite stone ==> not infinity stone ;)
            }
            if(canThrow)
            {
                dropStone();
            }
        }

        if(isThrowing)
        {
            if (bufferCount > spawnStoneBuffer)
            {
                throwStance = false;
                bufferCount = 0;
                isThrowing = false;
            }
            else
            {
                bufferCount += Time.deltaTime;
            }
        }
        DotsSpawner();
	}

    [HideInInspector]public GameObject stoneTemp;

    int spawnStoneBuffer = 1;
    float bufferCount = 0;

	void throwing(){
        throwed = true;
        stoneTemp = (GameObject)Instantiate (stone, tempPos, Quaternion.identity);
        // ignore collision with stone

        Physics2D.IgnoreCollision(this.GetComponent<BoxCollider2D>(), stoneTemp.GetComponent<BoxCollider2D>());
        

        Physics2D.IgnoreCollision(player[0].GetComponent<BoxCollider2D>(), stoneTemp.GetComponent<BoxCollider2D>());
        Physics2D.IgnoreCollision(player[1].GetComponent<BoxCollider2D>(), stoneTemp.GetComponent<BoxCollider2D>());

        otherStone = GameObject.FindGameObjectWithTag("Stone");
        Physics2D.IgnoreCollision(otherStone.GetComponent<BoxCollider2D>(), stoneTemp.GetComponent<BoxCollider2D>());


        spawnStone = 0;
        isThrowing = true;
        //control.StopGameControl = false;
        dropStoneCount = 0;
        Destroy(tempBar);
        ThrowingSound();
	}

    public void ThrowingSound()
    {
        FindObjectOfType<AudioManager>().Play("StoneThrowing");
    }

    public int dropStoneTime = 3;
    public float dropStoneCount = 0;
 //   public GameObject fillBar;
    GameObject tempBar;
    Transform tempMask;

    void dropStone()
    {
  //      if (tempBar == null)
  //      {
            //        tempBar = (GameObject)Instantiate(fillBar, head, Quaternion.identity);
     //       tempBar = (GameObject)Instantiate(stoneMid, head, Quaternion.identity);
            //        foreach (Transform child in tempBar.transform)
            //        {
            //           if (child.CompareTag("Nothing"))
            //           {
            //               tempMask = child;
            //          }
            //      }
   //     }
        if (dropStoneCount > dropStoneTime)
        {
            droppedStone = true;            
            spawnStone = 0;
       //     stoneTemp = (GameObject)Instantiate(stone, tempPos, Quaternion.identity);
       //     stoneTemp.GetComponent<S_Control>().launched = true;
            // ignore collision with stone
    //        Physics2D.IgnoreCollision(this.GetComponent<BoxCollider2D>(), stoneTemp.GetComponent<BoxCollider2D>());
            
            dropStoneCount = 0;
            control.StopGameControl = false;
            throwStance = false;
            GetComponent<B_Animations>().ResetThrowInteraction();
   //         Destroy(tempBar);
        }
        else
        {
            dropStoneCount += Time.deltaTime;
        }
  //      if(tempMask != null)
   //     {
   //         tempMask.localPosition = new Vector2(tempMask.localPosition.x, -(dropStoneCount / dropStoneTime) - 0.3f);
   //     }
    }
    
	// Trajectory line
	public int numDots;
	public float dotsPositionOverTime;  // seconds: if 10s, means the distance between two dots takes 10 seconds to reach
	private int count = 0;
	public GameObject dots;
	private Vector2 p_position;

	public float speedX;
	public float speedY;

	private Vector2 GRAVITY = new Vector2(0, -45f);
    private GameObject[] trajectoryDots = new GameObject[60];

	private void DotsSpawner(){

		if (throwStance && count == 0) {
			p_position = transform.position;
            // Offset Y
            float tempYSize = GetComponent<BoxCollider2D>().size.y / 2;
            p_position.y = p_position.y + tempYSize;
			for (int i = 0; i < numDots; i++) {
				dots.transform.position = CalculatePosition (dotsPositionOverTime * i);  // set position based on calculation the position of dots over time
				trajectoryDots[i] = (GameObject)Instantiate (dots,dots.transform.position,Quaternion.identity);
			}
			count = 1;
		}else if(!throwStance && count == 1){
			foreach(GameObject Dots in trajectoryDots){
				Destroy(Dots);
			}
			count = 0;
		}else if(throwStance && Input.GetKey((control.KeyUp)) && speedX <= 45f){		// adjust trajectory with 10 x limits 
			foreach(GameObject Dots in trajectoryDots)
            {
				Destroy(Dots);
			}
            dropStoneCount = 0; // testing andrea method
			speedX += 0.25f;
			count = 0;
			DotsSpawner ();
		}else if(throwStance && Input.GetKey((control.KeyDown)) && speedX >= 5f){		// adjust trajectory with 5 x limits 
			foreach(GameObject Dots in trajectoryDots)
            {
				Destroy(Dots);
            }
            dropStoneCount = 0; // testing andrea method
            speedX -= 0.25f;
			count = 0;
			DotsSpawner ();
		}
        else if (throwStance && Input.GetKeyDown(control.KeyLeft))
        {		// change trajectory to left
			transform.localScale = new Vector3(-1f, transform.localScale.y,transform.localScale.z);
			foreach(GameObject Dots in trajectoryDots)
            {
				Destroy(Dots);
			}
			count = 0;
			DotsSpawner ();
		}else if (throwStance && Input.GetKeyDown(control.KeyRight))
        {		// change trajectory to right
			transform.localScale = new Vector3(1f, transform.localScale.y, transform.localScale.z);
			foreach(GameObject Dots in trajectoryDots)
            {
				Destroy(Dots);
			}
			count = 0;
			DotsSpawner ();
		}
	}

	private Vector2 CalculatePosition(float elapsedTime){		// calculate the position of dots over time
		Vector2 d_launchVelocity = new Vector2(speedX * transform.localScale.x,speedY);
		return GRAVITY * elapsedTime * elapsedTime * 0.5f + d_launchVelocity * elapsedTime + p_position;
	}
}



/* // hold opposite key to drop
 * void dropStone()
    {
        if(control.faceLeft)
        {
            if (Input.GetKey(control.KeyRight))
            {
                if(tempBar == null)
                {
                    tempBar = (GameObject)Instantiate(fillBar, barPosition, Quaternion.identity);
                    foreach (Transform child in tempBar.transform)
                    {
                        if (child.CompareTag("Nothing"))
                        {
                            tempMask = child;
                        }
                    }
                }
                if (dropStoneCount > dropStoneTime)
                {
                    spawnStone = 0;
                    stoneTemp = (GameObject)Instantiate(stone, tempPos, Quaternion.identity);
                    stoneTemp.GetComponent<S_Control>().launched = true;
                    dropStoneCount = 0;
                    control.StopGameControl = false;
                    throwStance = false;
                    Destroy(tempBar);
                }
                else
                {
                    dropStoneCount += Time.deltaTime;
                }

                tempMask.localScale = new Vector2(dropStoneCount, tempMask.localScale.y);
            }
            else
            {
                dropStoneCount = 0;
                Destroy(tempBar);
            }
        }
        else
        {
            if (Input.GetKey(control.KeyLeft))
            { 
                if (tempBar == null)
                {
                    tempBar = (GameObject)Instantiate(fillBar, barPosition, Quaternion.identity);
                    foreach (Transform child in tempBar.transform)
                    {
                        if (child.CompareTag("Nothing"))
                        {
                            tempMask = child;
                        }
                    }
                }
                if (dropStoneCount > dropStoneTime)
                {
                    spawnStone = 0;
                    stoneTemp = (GameObject)Instantiate(stone, tempPos, Quaternion.identity);
                    stoneTemp.GetComponent<S_Control>().launched = true;
                    dropStoneCount = 0;
                    control.StopGameControl = false;
                    throwStance = false;
                    Destroy(tempBar);
                }
                else
                {
                    dropStoneCount += Time.deltaTime;
                }

                tempMask.localScale = new Vector2(dropStoneCount, tempMask.localScale.y);
            }
            else
            {
                dropStoneCount = 0;
                Destroy(tempBar);
            }
        }
    }

    */