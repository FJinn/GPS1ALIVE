using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_pushPull : MonoBehaviour {

    public float distance;
    public LayerMask boxMask;
    public bool isPulling;
    
    GameObject box;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        // using raycast to determine Line of Sight or calculating distance (NOTE: might reuse raycast of LoS calculation etc)
        Physics2D.queriesStartInColliders = false;
        RaycastHit2D hit = Physics2D.Raycast(transform.Find("Eye").position, Vector2.right * transform.localScale.x, distance, boxMask);

        if (hit.collider != null && Input.GetKeyDown(GetComponent<P_controls>().KeyUse) && hit.collider.tag == "PushPull")
        {
            box = hit.collider.gameObject;
            box.GetComponent<FixedJoint2D>().enabled = true;
            box.GetComponent<M_BoxPull>().beingPush = true;
            box.GetComponent<FixedJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();
        }
        else if (Input.GetKeyUp(GetComponent<P_controls>().KeyUse) && box != null)
        {
            box.GetComponent<FixedJoint2D>().enabled = false;
            box.GetComponent<M_BoxPull>().beingPush = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.Find("Eye").position, (Vector2)transform.Find("Eye").position + Vector2.right * transform.localScale.x * distance);
    }
    

    
}
