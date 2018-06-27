using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_BoxPull : MonoBehaviour {


    public bool beingPush;
    private float xPos;
    private Vector2 velocity;

    [Header("Does it pushable at start?")]
    public bool pushable;
    
	// Use this for initialization
	void Start () {
        xPos = transform.position.x;
	}
	
	// Update is called once per frame
    
	void Update () {
        if (beingPush == false  || pushable == false)
        {
               GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
               // make it remain on the same position X
               transform.position = new Vector3(xPos, transform.position.y);
        }
        else
        {
            // pushing? update the xPos to latest position
            xPos = transform.position.x;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("TriggerPush"))
        {
            pushable = true;
        }
    }
    
}

