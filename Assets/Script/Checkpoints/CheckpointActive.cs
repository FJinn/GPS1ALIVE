using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointActive : MonoBehaviour {

    private GameObject CheckpointManager;
    public int setCheckpointInt;

	// Use this for initialization
	void Start () {
        CheckpointManager = GameObject.FindGameObjectWithTag("CheckpointManager");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") || collision.CompareTag("Player2"))
        {
            CheckpointManager.GetComponent<Checkpoint>().spawnPoint = transform.position;
            CheckpointManager.GetComponent<Checkpoint>().CheckpointCamera = setCheckpointInt;
        }
    }
}
