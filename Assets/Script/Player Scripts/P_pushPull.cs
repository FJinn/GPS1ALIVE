using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_pushPull : MonoBehaviour {

    public float distance;
    public LayerMask boxMask;
    public bool isPulling;
    public bool OnBox = false;

    GameObject box;
    private Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        // using raycast to determine Line of Sight or calculating distance (NOTE: might reuse raycast of LoS calculation etc)
        Physics2D.queriesStartInColliders = false;
        RaycastHit2D hit = Physics2D.Raycast(transform.Find("Eye").position, Vector2.right * transform.localScale.x, distance, boxMask);

        if (hit.collider != null && Input.GetKeyDown(GetComponent<P_controls>().KeyUse) && hit.collider.tag == "PushPull" && (GetComponent<Rigidbody2D>().velocity.y <= 0.5f && GetComponent<Rigidbody2D>().velocity.y >= -0.5f)) {
            box = hit.collider.gameObject;
            if(box.GetComponent<M_BoxPull>().isActiveAndEnabled && box.GetComponent<M_BoxPull>().pushable && box.GetComponent<FixedJoint2D>().connectedBody == null) {
                box.GetComponent<FixedJoint2D>().enabled = true;
                box.GetComponent<FixedJoint2D>().connectedBody = rb2d;
                box.GetComponent<M_BoxPull>().beingPush = true;
                GetComponent<P_controls>().noJump = true;
                OnBox = true;
            }
        }
        else if (Input.GetKeyUp(GetComponent<P_controls>().KeyUse) && box != null && box.GetComponent<FixedJoint2D>().connectedBody == rb2d){
            box.GetComponent<FixedJoint2D>().enabled = false;
            box.GetComponent<M_BoxPull>().beingPush = false;
            box.GetComponent<FixedJoint2D>().connectedBody = null;
            GetComponent<P_controls>().noJump = false;
            OnBox = false;
            box = null;
        }


        if (box != null)
        {
            if (!box.GetComponent<M_BoxPull>().isActiveAndEnabled)
            {
                box.GetComponent<FixedJoint2D>().enabled = false;
                
                if (!box.GetComponent<M_BoxPull>().isActiveAndEnabled)
                {
                    box.GetComponent<FixedJoint2D>().enabled = false;
                    box.GetComponent<M_BoxPull>().beingPush = false;
                    GetComponent<P_controls>().noJump = false;
                    OnBox = false;
                    box = null;
                }
            }
            
        }
        
        if(GetComponent<P_controls>().moveHorizontal == 0 && box != null )
        {
            if( box.GetComponent<FixedJoint2D>().connectedBody == rb2d)
            {
                box.GetComponent<Rigidbody2D>().velocity = new Vector2(0, box.GetComponent<Rigidbody2D>().velocity.y);
            }
        }

    }

    private void OnDrawGizmos(){
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.Find("Eye").position, (Vector2)transform.Find("Eye").position + Vector2.right * transform.localScale.x * distance);
    }
    

    
}
