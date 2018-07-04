using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressed : MonoBehaviour {

    public GameObject Ui;
    public bool canSpawn = true;
    public bool player1Inside = false;
    public bool player2Inside = false;
    private GameObject spawnedObject;

    void Update()
    {
        if (GetComponent<FixedJoint2D>().enabled)
        {
            GetComponent<EdgeCollider2D>().enabled = false;
            if (spawnedObject != null)
            {
                Destroy(spawnedObject);
                canSpawn = true;
            }
        }
        else
        {
            GetComponent<EdgeCollider2D>().enabled = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Vector3 spawnPos = new Vector3(0,8f,0);
        Quaternion spawnRot = Quaternion.Euler(0, 0, 0);

        if (other.tag == "Player")
        {
            player1Inside = true;            
        }

        if(other.tag == "Player2")
        {
            player2Inside = true;
        }     
 
        if (canSpawn && other.tag == "Player" ||
            canSpawn && other.tag == "Player2")
        {
            spawnedObject = Instantiate(Ui,(transform.position + spawnPos),spawnRot);           
            spawnedObject.GetComponent<IncreasingAlpha>().objectSpawner = gameObject;
            canSpawn = false;
        }
        if (spawnedObject != null)
        {
            spawnedObject.GetComponent<IncreasingAlpha>().cancelEverything();
            spawnedObject.GetComponent<IncreasingAlpha>().callingFadeTo();
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
