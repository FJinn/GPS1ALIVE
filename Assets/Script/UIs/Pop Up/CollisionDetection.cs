using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour {

	private bool showDebug = true;

    void OnTriggerEnter2D()
    {
        Debug.Log("Enter");
    }

	void OnTriggerStay2D()
    {
		if (showDebug) {
			Debug.Log ("Inside");
			showDebug = false;
		}
    }

	void OnTriggerExit2D()
    {
        Debug.Log("Exit");
		showDebug = true;
    }
}
