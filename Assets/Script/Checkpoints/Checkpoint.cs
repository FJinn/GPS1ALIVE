using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    private int CheckpointInt;
    public Vector3 spawnPoint;
    public static bool managerCallOnce;


    // Use this for initialization
    void Start () {
        if(!managerCallOnce)
        {
            managerCallOnce = true;
            DontDestroyOnLoad(gameObject);
        }else
        {
            Destroy(gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
}
