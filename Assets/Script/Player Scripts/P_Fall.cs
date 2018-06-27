using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Fall : MonoBehaviour {

    public float fallSpeed;

	// Use this for initialization
	void Start () {
        
	}

    // Update is called once per frame
    void Update(){
        if (GetComponent<Rigidbody2D>().velocity.y <= -fallSpeed)
        {
            GetComponent<P_Death>().isDead = true;
        }
    }
}
