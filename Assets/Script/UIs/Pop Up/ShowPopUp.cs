using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPopUp : MonoBehaviour {

    public Transform Spawnpoint;
    public GameObject Ui;
    private GameObject spawnedObject;
    bool canSpawn = true;

	void OnTriggerEnter2D()
    {
        if(canSpawn)
        {
            spawnedObject = Instantiate(Ui, Spawnpoint.position, Spawnpoint.rotation);
            canSpawn = false;
        }       
    }

	void OnTriggerExit2D()
    {
        Destroy(spawnedObject);
        canSpawn = true;
    }
}
