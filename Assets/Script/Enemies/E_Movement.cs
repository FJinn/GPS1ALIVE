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

	// Use this for initialization
	void Start () {
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
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (tempHolder.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
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
                
			} else {
				waitTime -= Time.deltaTime;
			}
		}

	}
}
