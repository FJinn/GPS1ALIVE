using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_HangingBar : MonoBehaviour {

	public GameObject[] p;
    private GameObject p_collided;
	// Use this for initialization
	void Start () {
        p = new GameObject[2];
        p[0] = GameObject.FindGameObjectWithTag("Player");
        p[1] = GameObject.FindGameObjectWithTag("Player2");

    }
	
	// Update is called once per frame
	void Update () {

	//	JointMotor2D tempMotor = GetComponent<SliderJoint2D> ().motor;

	//	GetComponent<SliderJoint2D> ().motor = tempMotor;

		for (int i = 0; i < p.Length; i++) {

			if(GetComponent<BoxCollider2D>().IsTouching(p[i].GetComponent<BoxCollider2D>())){
                p_collided = p[i];
                p_collided.GetComponent<FixedJoint2D>().connectedBody = GetComponent<Rigidbody2D>();
                p_collided.GetComponent<FixedJoint2D>().enableCollision = true;
                p_collided.GetComponent<FixedJoint2D>().enabled = true;
                p_collided.GetComponent<Rigidbody2D>().gravityScale = 0f;
                p_collided.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                p_collided.transform.position = new Vector3(transform.position.x, transform.position.y - transform.localScale.y, p_collided.transform.position.z);
            }
            
		}
	}
    
}

