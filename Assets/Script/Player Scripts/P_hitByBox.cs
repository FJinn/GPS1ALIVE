using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_hitByBox : MonoBehaviour {

    GameObject temp;

    private void Start()
    {
        temp = transform.parent.gameObject;
    }

    private void OnTriggerEnter2D (Collider2D other)
    {
        if(other.CompareTag("PushPull")){
            Physics2D.IgnoreCollision(temp.GetComponent<BoxCollider2D>(), other.GetComponent<BoxCollider2D>());

            GetComponentInParent<P_Death>().killByBox = true;
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
