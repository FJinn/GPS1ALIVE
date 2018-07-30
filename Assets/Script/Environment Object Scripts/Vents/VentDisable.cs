using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentDisable : MonoBehaviour {

    [SerializeField] GameObject[] ventsToDisable;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") || collision.CompareTag("Player2"))
        {
            for (int i = 0; i < ventsToDisable.Length; i++)
            {
                ventsToDisable[i].GetComponent<CrawlIntoCrack>().enabled = false;
            }
        }
    }
}
