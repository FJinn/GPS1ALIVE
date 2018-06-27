using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackTeleport : MonoBehaviour {

    public GameObject[] Players;

    [Header("Teleport to where?")]
    public GameObject TeleportTo;

	// Use this for initialization
	void Start () {
        Players = new GameObject[2];
        Players[0] = GameObject.FindGameObjectWithTag("Player");
        Players[1] = GameObject.FindGameObjectWithTag("Player2");
    }
	
	// Update is called once per frame
	void Update () {
        for (int i =0;i < Players.Length; i++)
        {
            if (GetComponent<BoxCollider2D>().IsTouching(Players[i].GetComponent<BoxCollider2D>()))
            {
                if(Input.GetKeyDown(Players[i].GetComponent<P_controls>().KeyUse))
                {
                    Players[i].transform.position = TeleportTo.transform.position;
                }
            }
        }
        
    }
}
