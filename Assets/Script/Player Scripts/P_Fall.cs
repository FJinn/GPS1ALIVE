using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Fall : MonoBehaviour {

    public float fallSpeed;
    private Rigidbody2D rb2d;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void FallDeathSound()
    {
        FindObjectOfType<AudioManager>().Play("FallDeath");
    }

    // Update is called once per frame
    void Update(){
        if (rb2d.velocity.y <= -fallSpeed && !GetComponent<P_Death>().isDead){
            GetComponent<P_controls>().fallen = true;
            GetComponent<P_Death>().isDead = true;
            GetComponent<P_Death>().StartCoroutine("Dead");
            FallDeathSound();
        }       
    }
    
}
