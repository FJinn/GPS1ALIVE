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

    private void Awake(){
        temp = this.gameObject.tag;
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
        if (other.tag == "leftCrack" && GetComponent<Rigidbody2D>().velocity.x < 0 && firstTap)
        {
            transform.position = new Vector2(tempX, transform.position.y);
        }
        if (other.tag == "rightCrack" && GetComponent<Rigidbody2D>().velocity.x > 0 && firstTap)
        {
            transform.position = new Vector2(tempX, transform.position.y);
        }
    }
}
