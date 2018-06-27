using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L_InLight : MonoBehaviour {
    
    public LayerMask playerMask;
    public float LightDetectionRadius;

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        Collider2D[] playerWithinRange = Physics2D.OverlapCircleAll(transform.position, LightDetectionRadius, playerMask);

        foreach (Collider2D player in playerWithinRange)
        {
           
            if (player.CompareTag("Player") || player.CompareTag("Player2"))
            {
                if (Vector2.Distance(player.transform.position, transform.position) <= LightDetectionRadius)
                {
                    player.GetComponent<P_ShadowDetect>().P_isUnderShadow = false;
                }else
                {
                    player.GetComponent<P_ShadowDetect>().P_isUnderShadow = true;
                }
            }
        }
        
        
	}
    
}
