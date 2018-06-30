using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressed : MonoBehaviour {

    public Transform Spawnpoint;
    public GameObject Ui;
    public bool canSpawn = true;
    public bool player1Inside = false;
    public bool player2Inside = false;
    private GameObject spawnedObject;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("1");
            player1Inside = true;            
        }

        if(other.tag == "Player2")
        {
            player2Inside = true;
            Debug.Log("2");
        }     
 
        if (canSpawn && other.tag == "Player" ||
            canSpawn && other.tag == "Player2")
        {
            spawnedObject = Instantiate(Ui, Spawnpoint.position, Spawnpoint.rotation);           
            spawnedObject.GetComponent<IncreasingAlpha>().objectSpawner = gameObject;
            canSpawn = false;
        }
        if (spawnedObject != null)
        {
            spawnedObject.GetComponent<IncreasingAlpha>().cancelEverything();
            spawnedObject.GetComponent<IncreasingAlpha>().callingFadeTo();
        }      
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (GetComponent<FixedJoint2D>().enabled)
        {
            Debug.Log("Pushing");
            if(spawnedObject != null)
            {
                Destroy(spawnedObject);
            }          
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            player1Inside = false;
        }

        if(other.tag == "Player2")
        {
            player2Inside = false;
        }

        if(!player1Inside && !player2Inside)           
        {
            Despawn();
            canSpawn = true;
        }
    }

    void Despawn()
    {
        if (spawnedObject != null)
        {
            spawnedObject.GetComponent<IncreasingAlpha>().cancelEverything();
            spawnedObject.GetComponent<IncreasingAlpha>().callingFadeOut();
        }     
    }
}
