using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Spawn : MonoBehaviour {

    public GameObject spawnLocation;
    private GameObject spawnManager;


	// Use this for initialization
	void Start () {
        spawnManager = GameObject.Find("SO_CheckpointManager");
        GetComponent<P_controls>().CameraStarted = false;

        if (gameObject.CompareTag("Player"))
        {
            transform.position = spawnManager.GetComponent<Checkpoint>().spawnPoint + new Vector3(-2, 0,0);
        }else
        if(gameObject.CompareTag("Player2"))
        {
            transform.position = spawnManager.GetComponent<Checkpoint>().spawnPoint + new Vector3(2, 0, 0);
        }

        Invoke("StartCamera", 0.5f);
	}

    void StartCamera()
    {
        GetComponent<P_controls>().CameraStarted = true;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
