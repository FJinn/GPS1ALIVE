using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L_Destroy : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Stone"){
            foreach (Transform child in this.transform){
                Destroy(child.gameObject);
            }
        }
    }
}
