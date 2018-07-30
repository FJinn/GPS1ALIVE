using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_avoidEnemyVent : MonoBehaviour {

    // To check if the player inside vent or not
    public bool firstTap = false;

    // cache tag
    string temp;
    // cache position
    float tempX;

    //rb2d
    private Rigidbody2D rb2d;

    private void Start(){
        temp = this.gameObject.tag;
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update () {
        tempX = transform.position.x;
        if (firstTap) {
            this.gameObject.tag = "Untagged";
        }
        else {
            this.gameObject.tag = temp;
        }
    }

    private void OnTriggerStay2D (Collider2D other)
    {
        if (other.CompareTag("leftCrack") && rb2d.velocity.x < 0 && firstTap)
        {
            transform.position = new Vector2(tempX, transform.position.y);
        }else
        if (other.CompareTag("rightCrack") && rb2d.velocity.x > 0 && firstTap)
        {
            transform.position = new Vector2(tempX, transform.position.y);
        }
    }
}
