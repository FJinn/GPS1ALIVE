using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Spawn : MonoBehaviour {

    public GameObject spawnLocation;
    private GameObject spawnManager;

	// Use this for initialization
	void Start () {
        spawnManager = GameObject.Find("EO_CheckpointManager");
        if (gameObject.CompareTag("Player"))
        {
            transform.position = spawnManager.GetComponent<Checkpoint>().spawnPoint + new Vector3(-5, 0,0);
        }else
        if(gameObject.CompareTag("Player2"))
        {
            transform.position = spawnManager.GetComponent<Checkpoint>().spawnPoint + new Vector3(3, 0, 0);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
