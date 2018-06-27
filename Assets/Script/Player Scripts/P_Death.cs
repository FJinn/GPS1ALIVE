using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class P_Death : MonoBehaviour {

    public bool isDead = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isDead){
            SceneManager.LoadScene("Prototype_Level_2");
            isDead = false;
        }
	}
}
