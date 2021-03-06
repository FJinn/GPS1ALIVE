﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressed : MonoBehaviour {

    public GameObject Ui;
    public bool canSpawn = true;
    public bool player1Inside = false;
    public bool player2Inside = false;
    public bool boxUi = false;
    public float spawnPosY;
    public float spawnPosX;
    
    private GameObject spawnedObject;
    Quaternion iniRot;

    private void Start()
    {
        iniRot = transform.rotation;       
    }

    private void LateUpdate()
    {
        transform.rotation = iniRot;
    }

    void Update()
    {
        if (boxUi)
        {
            if (!GetComponentInParent<M_BoxPull>().enabled)
            {
                Destroy(spawnedObject);
            }
            if (GetComponentInParent<FixedJoint2D>().enabled)
            {
                GetComponent<BoxCollider2D>().enabled = false;
                if (spawnedObject != null)
                {
                    Destroy(spawnedObject);
                    canSpawn = true;
                }
            }
            else
            {
                GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Vector3 spawnPos = new Vector3(spawnPosX,spawnPosY,0);
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

    public void Despawn()
    {
        if (spawnedObject != null)
        {
            spawnedObject.GetComponent<IncreasingAlpha>().cancelEverything();
            spawnedObject.GetComponent<IncreasingAlpha>().callingFadeOut();
        }     
    }
}
