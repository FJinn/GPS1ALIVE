using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressed : MonoBehaviour {

    public Transform Spawnpoint;
    public GameObject Ui;
    private GameObject spawnedObject;
    public bool canSpawn = true;
    public bool objectTaken = false;
	public bool playerEnter = false;

	void OnTriggerEnter2D()
    {
        if (canSpawn && objectTaken == false)
        {
			spawnedObject = Instantiate (Ui, Spawnpoint.position, Spawnpoint.rotation);
			canSpawn = false;
			playerEnter = true;
			spawnedObject.GetComponent<IncreasingAlpha> ().objectSpawner = gameObject;
        }    
		if (spawnedObject != null) {
			spawnedObject.GetComponent<IncreasingAlpha> ().cancelEverything ();
			spawnedObject.GetComponent<IncreasingAlpha> ().callingFadeTo ();
		}
		//GameObject.Find ("Player").GetComponent<Movement> ().collidedWithObject = true;
    }

	void OnTriggerExit2D()
    {
		spawnedObject.GetComponent<IncreasingAlpha> ().cancelEverything ();
		spawnedObject.GetComponent<IncreasingAlpha> ().callingFadeOut ();
       // GameObject.Find("Player").GetComponent<Movement>().collidedWithObject = false;
		playerEnter = false;
    }
}
