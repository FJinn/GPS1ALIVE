using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_throw : MonoBehaviour {

	public float doubleTapTime;
	private bool throwReady = false;
	public bool onThrow;
	public GameObject stone;
	public int spawnStone;
    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetKeyDown(GetComponent<P_controls>().KeyUse)) {		// to set double tap
			if (throwReady) {
				onThrow = true;
			}
			else if(spawnStone != 0){
				prepareThrow(true);
			}
		}
			
		if (onThrow) {
			if (Input.GetKeyUp(GetComponent<P_controls>().KeyUse)) {		// for release second tap to throw
				throwing ();
				onThrow = false;
				//spawnStone = 1;// for testing purpose, infinite stone ==> not infinity stone ;)
			}
		}
		DotsSpawner();
	}

	void throwing(){
		throwReady = false;
		if (spawnStone > 0) {		// spawn only one stone
            // Offset Y
            float tempYSize = GetComponent<BoxCollider2D>().size.y / 2;
            Vector2 tempPos = new Vector2 (transform.position.x , transform.position.y + tempYSize);
			Instantiate (stone, tempPos, Quaternion.identity);
            stone.GetComponent<S_Movement>().enabled = true;
			spawnStone = 0;
		}
	}

	void prepareThrow(bool makeReady){
		CancelInvoke ("cancelThrow");
		Invoke ("cancelThrow", doubleTapTime);
		throwReady = true;
	}

	void cancelThrow(){
		throwReady = false;
	}
		
	// Trajectory line
	public int numDots;
	public float dotsPositionOverTime;  // seconds: if 10s, means the distance between two dots takes 10 seconds to reach
	private int count = 0;
	public GameObject dots;
	private Vector2 p_position;

	public float speedX;
	public float speedY;

	private Vector2 GRAVITY = new Vector2(0, -9.81f);

	private void DotsSpawner(){
		
		var DOTS = GameObject.FindGameObjectsWithTag ("Dots");
		if (onThrow && count == 0) {
			p_position = transform.position;
            // Offset Y
            float tempYSize = GetComponent<BoxCollider2D>().size.y / 2;
            p_position.y = p_position.y + tempYSize;
			for (int i = 0; i < numDots; i++) {
				dots.transform.position = CalculatePosition (dotsPositionOverTime * i);  // set position based on calculation the position of dots over time
				Instantiate (dots,dots.transform.position,Quaternion.identity);
			}
			count = 1;
		}else if(!onThrow && count == 1){
			foreach(var Dots in DOTS){
				Destroy(Dots);
			}
			count = 0;
		}else if(onThrow && Input.GetKey((GetComponent<P_controls>().KeyUp)) && speedX <= 20f){		// adjust trajectory with 10 x limits 
			foreach(var Dots in DOTS){
				Destroy(Dots);
			}
			speedX += 0.1f;
			count = 0;
			DotsSpawner ();
		}else if(onThrow && Input.GetKey((GetComponent<P_controls>().KeyDown)) && speedX >= 5f){		// adjust trajectory with 5 x limits 
			foreach(var Dots in DOTS){
				Destroy(Dots);
			}
			speedX -= 0.1f;
			count = 0;
			DotsSpawner ();
		}else if (onThrow && Input.GetKeyDown(GetComponent<P_controls>().KeyLeft))
        {		// change trajectory to left
			transform.localScale = new Vector3(-1f, transform.localScale.y,transform.localScale.z);
			foreach(var Dots in DOTS){
				Destroy(Dots);
			}
			count = 0;
			DotsSpawner ();
		}else if (onThrow && Input.GetKeyDown(GetComponent<P_controls>().KeyRight))
        {		// change trajectory to right
			transform.localScale = new Vector3(1f, transform.localScale.y, transform.localScale.z);
			foreach(var Dots in DOTS){
				Destroy(Dots);
			}
			count = 0;
			DotsSpawner ();
		}
	}

	private Vector2 CalculatePosition(float elapsedTime){		// calculate the position of dots over time
		Vector2 d_launchVelocity = new Vector2(speedX * transform.localScale.x,speedY);
		return GRAVITY * elapsedTime * elapsedTime * 0.5f + d_launchVelocity * elapsedTime + p_position;
	}
}
