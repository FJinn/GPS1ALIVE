using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPopUp : MonoBehaviour
{

    public GameObject Ui;
    public bool canSpawn = true;
    public bool player1Inside = false;
    public bool player2Inside = false;
    public float maxDistance;
    public GameObject []player;
    private GameObject spawnedObject;

    float distP1;
    float distP2;  

    void Update()
    {
        checkDistance();
        Despawn();
        showUi();
        Interacting();
    }

    void checkDistance()
    {
        if(GetComponent<FixedJoint2D>().enabled == false)
        {
            distP1 = Vector3.Distance(player[0].GetComponent<Transform>().position, transform.position);
            distP2 = Vector3.Distance(player[1].GetComponent<Transform>().position, transform.position);
            if (distP1 <= maxDistance)
            {
                player1Inside = true;
            }
            else
            {
                player1Inside = false;
            }
            if (distP2 <= maxDistance)
            {
                player2Inside = true;
            }
            else
            {
                player2Inside = false;
            }
        }
    }

    void showUi()
    {
        Vector3 spawnPos = new Vector3(0, 8f, 0);
        Quaternion spawnRot = Quaternion.Euler(0, 0, 0);
        if (player1Inside || player2Inside)
        {        
            if (canSpawn)
            {
                spawnedObject = Instantiate(Ui, (transform.position + spawnPos), spawnRot);
                spawnedObject.GetComponent<IncreasingAlpha>().objectSpawner = gameObject;
                canSpawn = false;
                if (spawnedObject != null)
                {
                    spawnedObject.GetComponent<IncreasingAlpha>().cancelEverything();
                    spawnedObject.GetComponent<IncreasingAlpha>().callingFadeTo();
                }               
            }
        }
    }

    void Despawn()
    {
        if (distP1 > maxDistance && !player1Inside && !player2Inside ||
            distP2 > maxDistance && !player1Inside && !player2Inside)
        {
            if (spawnedObject != null)
            {
                spawnedObject.GetComponent<IncreasingAlpha>().cancelEverything();
                spawnedObject.GetComponent<IncreasingAlpha>().callingFadeOut();
              
            }
        }
    }

    void Interacting()
    {
        if(GetComponent<FixedJoint2D>().enabled == true)
        {
            if (spawnedObject != null)
            {
                Destroy(spawnedObject);
                canSpawn = true;
            }
        }
    }
}
