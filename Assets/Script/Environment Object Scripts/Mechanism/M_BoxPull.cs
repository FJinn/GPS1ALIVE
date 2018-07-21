using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_BoxPull : MonoBehaviour {


    public bool beingPush;
    public float xPos;
    private Vector2 velocity;
    private Rigidbody2D rb2d;

    [Header("Does it pushable at start?")]
    public bool pushable;
    
	// Use this for initialization
	void Start () {
        xPos = transform.position.x;
        rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
    
	void Update () {
        if (beingPush == false  || pushable == false){
               rb2d.velocity = new Vector2(0, rb2d.velocity.y);
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

