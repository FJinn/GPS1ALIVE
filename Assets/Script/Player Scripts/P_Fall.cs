using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Fall : MonoBehaviour {

    public float fallSpeed;

    // Update is called once per frame
    void Update(){
        if (GetComponent<Rigidbody2D>().velocity.y <= -fallSpeed && !GetComponent<P_Death>().isDead){
            GetComponent<P_Death>().isDead = true;
            GetComponent<P_Death>().StartCoroutine("Dead");
        }
    }
}
