using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_BoxRotate : MonoBehaviour {
    
	void Update () {
		// if z rotation is not 0, 90, 180 ,360, then disable M_BoxPull
        if( (transform.eulerAngles.z <= 1 && transform.eulerAngles.z >= -1)
            || (transform.eulerAngles.z <= 91 && transform.eulerAngles.z >= 89)
            || (transform.eulerAngles.z <= 181 && transform.eulerAngles.z >= 179)
            || (transform.eulerAngles.z <= 271 && transform.eulerAngles.z >= 269)
            || (transform.eulerAngles.z <= 361 && transform.eulerAngles.z >= 359)){
            if (GetComponent<M_BoxFallControl>().onSupport){
                GetComponent<M_BoxPull>().enabled = true;
            }
        }
        else
        {
            GetComponent<M_BoxPull>().enabled = false;
            GetComponent<M_BoxPull>().xPos = transform.position.x;
            GetComponent<FixedJoint2D>().connectedBody = null;
        }
	}
}
