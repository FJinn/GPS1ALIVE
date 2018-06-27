using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_disableThrow : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (GetComponent<P_throw> ().spawnStone == 1) {
            GetComponent<P_throw>().enabled = true;

		} else if (GetComponent<P_throw> ().spawnStone == 0) {
			GetComponent<P_throw> ().enabled = false;
		}
	}
}
