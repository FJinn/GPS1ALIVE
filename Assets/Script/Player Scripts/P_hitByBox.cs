using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_hitByBox : MonoBehaviour {

    private void OnTriggerEnter2D (Collider2D other)
    {
        if(other.tag == "PushPull"){
            GetComponentInParent<P_Death>().isDead = true;
            GetComponentInParent<P_Death>().StartCoroutine("Dead");
            BoxDeathSound();
        } 
    }

    public void BoxDeathSound()
    {
        FindObjectOfType<AudioManager>().Play("BoxDeath");
    }



}
