using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_barrelHide : MonoBehaviour {

	public GameObject p;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (GetComponent<BoxCollider2D> ().IsTouching (p.GetComponent<BoxCollider2D> ())) {  //enable and disable player object
			if (Input.GetKeyDown (GetComponent<P_controls>().KeyUse)) {
				p.SetActive (false);
			}
		} else {
			if (Input.GetKeyDown (GetComponent<P_controls>().KeyUse)) {
				p.SetActive (true);
			}
		}
	}
}
