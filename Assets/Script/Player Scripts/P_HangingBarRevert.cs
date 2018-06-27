using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_HangingBarRevert : MonoBehaviour {

    private bool onBar = false;
    private float initGravity;

	// Use this for initialization
	void Start () {
        initGravity = GetComponent<Rigidbody2D>().gravityScale;
	}
	
	// Update is called once per frame
	void Update () {

        if(GetComponent<FixedJoint2D>().connectedBody != null){
            onBar = true;
        }

        if (Input.GetKeyDown(GetComponent<P_controls>().KeyDown) && onBar)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z);
            GetComponent<FixedJoint2D>().connectedBody = null;
            GetComponent<FixedJoint2D>().enabled = false;
            GetComponent<Rigidbody2D>().gravityScale = initGravity;
            onBar = false;
        }

    }
}
