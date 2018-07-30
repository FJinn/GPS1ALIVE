using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneL1Room2R : MonoBehaviour {

    [SerializeField] GameObject cutsceneToDestroy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") || collision.CompareTag("Player2"))
        {
            Destroy(cutsceneToDestroy);
        }
    }
}
