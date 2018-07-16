using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Movement : MonoBehaviour {

	public float e_patrolSpeed;
	public Transform[] moveSpots;
	public float startWaitTime;

	private Vector2 tempPos;
	private float waitTime;
    private int moveSpotsCount;

	private Vector3 tempHolder;
    public Animator anim;
    public bool isNurse;
    private Rigidbody2D rb2d;
    // private bool isRight = true;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        moveSpotsCount = 0;
		tempHolder = moveSpots [0].position;
		tempPos = tempHolder;
	}
	
	// Update is called once per frame
	void Update () {
		tempPos.y = transform.position.y;
		tempHolder.y = tempPos.y;
        // considering using moveposition
		transform.position = Vector2.MoveTowards (transform.position, tempHolder , e_patrolSpeed * Time.deltaTime);

        if (tempHolder.x > transform.position.x)
        {
            // isRight = true;
            
            if (isNurse)
            {
                anim.Play("N_PatrolAnim");
<<<<<<< HEAD
                transform.localScale = new Vector3(1, 1, 1);
=======
            } else
            {
                anim.Play("D_PatrolAnim");
>>>>>>> a7aa502ef9e932ec1a696eb79aa055c8988bbffc
            }
            
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (tempHolder.x < transform.position.x)
        {
            // isRight = false;
            
            if (isNurse)
            {
                anim.Play("N_PatrolAnim");
<<<<<<< HEAD
                transform.localScale = new Vector3(-1, 1, 1);
=======
>>>>>>> a7aa502ef9e932ec1a696eb79aa055c8988bbffc
            }
            else
            {
                anim.Play("D_PatrolAnim");
            }
            
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            
            if (isNurse)
            {
<<<<<<< HEAD
                // isRight = true;
                anim.Play("D_PatrolAnim");
                transform.localScale = new Vector3(1, 1, 1);
=======
                anim.Play("N_IdleAnim");
>>>>>>> a7aa502ef9e932ec1a696eb79aa055c8988bbffc
            }
            else
            {
<<<<<<< HEAD
                // isRight = false;
                anim.Play("D_PatrolAnim");
                transform.localScale = new Vector3(-1, 1, 1);
=======
                anim.Play("D_IdleAnim");
>>>>>>> a7aa502ef9e932ec1a696eb79aa055c8988bbffc
            }
            
        }
        

        if (Vector2.Distance(transform.position, tempHolder) < 1f ){
			if (waitTime <= 0)
            {
                moveSpotsCount++;
                if (moveSpotsCount >= moveSpots.Length)
                {
                    moveSpotsCount = 0;
                }

				tempHolder = moveSpots [moveSpotsCount].position;
                

                //   transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
                tempPos = tempHolder;
				waitTime = startWaitTime;
               // anim.Play("E_IdleAnim");
                
			} else {
				waitTime -= Time.deltaTime;
			}
		}

	}
}
