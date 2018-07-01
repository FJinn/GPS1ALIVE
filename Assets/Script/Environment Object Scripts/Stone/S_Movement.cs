using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Movement : MonoBehaviour {

	private GameObject p; 

	public Vector2 launchVelocity;
	public float timeForLaunching;

	public float SpeedX;
	public float SpeedY;

	public bool launched = true;
	private Rigidbody2D s_rigidbody;

	void Awake(){
		s_rigidbody = GetComponent<Rigidbody2D> ();
		GetComponent<BoxCollider2D> ().isTrigger = true;
        p = GetComponent<FS_searchNearest>().FindClosestEnemy();
	}
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		SpeedX = p.GetComponent<P_throw>().speedX; 
		SpeedY = p.GetComponent<P_throw>().speedY;
		launchVelocity = new Vector2 (SpeedX * p.transform.localScale.x , SpeedY);
			
		timeForLaunching -= Time.deltaTime;

		if (!launched && timeForLaunching <= 0) {
			Launch ();
            gameObject.GetComponent<AudioSource>().Play();
        }
	}

	private void Launch(){
		s_rigidbody.velocity = launchVelocity;

		launched = true;
	}
}
