using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressed : MonoBehaviour {

    public Transform Spawnpoint;
    public GameObject Ui;
    public GameObject[] player;
    public bool canSpawn = true;
    private GameObject spawnedObject;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(gameObject.GetComponent<FixedJoint2D>().isActiveAndEnabled)
        {
            canSpawn = false;
        }
        else
        {
            canSpawn = true;
        }
        if (canSpawn && other.tag == "Player" ||
            canSpawn && other.tag == "Player2")
        {
            spawnedObject = Instantiate(Ui, Spawnpoint.position, Spawnpoint.rotation);
            canSpawn = false;
            spawnedObject.GetComponent<IncreasingAlpha>().objectSpawner = gameObject;
        }
        if (spawnedObject != null)
        {
            spawnedObject.GetComponent<IncreasingAlpha>().cancelEverything();
            spawnedObject.GetComponent<IncreasingAlpha>().callingFadeTo();
        }
    }

	void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player" && spawnedObject != null ||
           other.tag == "Player2" && spawnedObject != null)
        {
            spawnedObject.GetComponent<IncreasingAlpha>().cancelEverything();
            spawnedObject.GetComponent<IncreasingAlpha>().callingFadeOut();
            canSpawn = true;
        }
    }
}
