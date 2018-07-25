using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Movement : MonoBehaviour {

	public float e_patrolSpeed;
	public Transform[] moveSpots;
	public float startWaitTime;
    public bool faceOriginDirection;
    private float originDirection;

    private Vector2 tempPos;
	private float waitTime;
    private int moveSpotsCount;

	private Vector3 tempHolder;
    public Animator anim;
    public bool isNurse;

    private BoxCollider2D myCollider;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private bool IgnoreEnemies;
    // private bool isRight = true;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        moveSpotsCount = 0;
		tempHolder = moveSpots [0].position;
		tempPos = tempHolder;
        originDirection = transform.localScale.x;
        myCollider = GetComponent<BoxCollider2D>();

        if(IgnoreEnemies)
        {
            Collider2D[] allEnemies = Physics2D.OverlapCircleAll(transform.position, 10000f, enemyMask);
            {
                for (int i = 0; i < allEnemies.Length; i++)
                {
                    Physics2D.IgnoreCollision(myCollider, allEnemies[i].GetComponent<BoxCollider2D>());
                }
            }
            Collider2D[] allEnemies2 = Physics2D.OverlapCircleAll(transform.position, 1000f);
            {
                for (int i = 0; i < allEnemies2.Length; i++)
                {
                    if(allEnemies2[i].CompareTag("PushPull"))
                    {
                        Physics2D.IgnoreCollision(myCollider, allEnemies2[i].GetComponent<BoxCollider2D>());
                    }
                }
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		tempPos.y = transform.position.y;
		tempHolder.y = tempPos.y;
        // considering using moveposition
		transform.position = Vector3.MoveTowards (transform.position, tempHolder , e_patrolSpeed * Time.deltaTime);

        if (tempHolder.x > transform.position.x)
        {
            // isRight = true;
            
            if (isNurse)
            {
                anim.Play("N_PatrolAnim");
            } else
            {
                anim.Play("D_PatrolAnim");
            }
            
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (tempHolder.x < transform.position.x)
        {
            // isRight = false;
            
            if (isNurse)
            {
                anim.Play("N_PatrolAnim");
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
                anim.Play("N_IdleAnim");
            }
            else
            {
                anim.Play("D_IdleAnim");
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
               if(faceOriginDirection)
                {
                    transform.localScale = new Vector3(originDirection, transform.localScale.y, transform.localScale.z);
                }
                
			} else {
				waitTime -= Time.deltaTime;
			}
		}

	}
}
