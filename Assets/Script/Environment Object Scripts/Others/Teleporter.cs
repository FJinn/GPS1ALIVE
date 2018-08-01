using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {

    [SerializeField] GameObject teleportPos1;
    [SerializeField] GameObject teleportPos2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.position = teleportPos1.transform.position;
        }

        if (collision.CompareTag("Player2"))
        {
            collision.transform.position = teleportPos2.transform.position;
        }
    }
}
