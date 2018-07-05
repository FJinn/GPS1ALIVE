using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_BoxFallControl : MonoBehaviour {

    // to prevent M_BoxRotate enable M_BoxPull
    public bool onSupport = true;

	void Update () {

        Physics2D.queriesStartInColliders = false;
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, Vector2.down, 5f);

        Debug.DrawRay(transform.position, Vector2.down * 5, Color.yellow);

        if (hit == false) {
            onSupport = false;
            GetComponent<M_BoxPull>().enabled = false;
        }
        else{
            onSupport = true;
            GetComponent<M_BoxPull>().enabled = true;
        }

	}
}
