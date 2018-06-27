using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_platformFollow : MonoBehaviour {
    

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D platform)
    {
        if (platform.transform.tag == "MovingPlatform") {
            transform.parent = platform.transform;
        }
    }


    private void OnCollisionExit2D(Collision2D platform)
    {
        if (platform.transform.tag == "MovingPlatform")
        {
            transform.parent = null;
        }
    }
}
