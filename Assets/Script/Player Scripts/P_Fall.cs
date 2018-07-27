using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Fall : MonoBehaviour {

    B_Animations anim;
    public float fallSpeed;
    private Rigidbody2D rb2d;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<B_Animations>();
    }

    public void FallDeathSound()
    {
        FindObjectOfType<AudioManager>().Play("FallDeath");
    }

    // Update is called once per frame
    void Update(){
        if (rb2d.velocity.y <= -fallSpeed && !GetComponent<P_Death>().isDead && anim.player1){
            GameObject.Find("Player2").GetComponent<B_Animations>().p1FallDeath = true;
            GetComponent<P_controls>().fallen = true;
            GetComponent<P_Death>().isDead = true;
            GetComponent<P_Death>().StartCoroutine("Dead");
            FallDeathSound();
        }
        else if (rb2d.velocity.y <= -fallSpeed && !GetComponent<P_Death>().isDead && anim.player2)
        {
            GameObject.Find("Player1").GetComponent<B_Animations>().p2FallDeath = true;
            GetComponent<P_controls>().fallen = true;
            GetComponent<P_Death>().isDead = true;
            GetComponent<P_Death>().StartCoroutine("Dead");
            FallDeathSound();
        }
    }
    
}
