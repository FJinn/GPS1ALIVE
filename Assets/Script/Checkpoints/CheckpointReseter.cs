using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckpointReseter : MonoBehaviour {

    private GameObject CheckpointManager;

    public void ResetCheckpoint(int levelReset)
    {
        CheckpointManager = GameObject.FindGameObjectWithTag("CheckpointManager");
        if(CheckpointManager != null)
        {
            if(levelReset == 2)
            {
                CheckpointManager.GetComponent<Checkpoint>().spawnPoint = new Vector3(-0.3f, 3, 0);
            }else
            if(levelReset == 3)
            {
                CheckpointManager.GetComponent<Checkpoint>().spawnPoint = new Vector3(2.1f, -29.5f, 0);
            }
        }
    }

}
