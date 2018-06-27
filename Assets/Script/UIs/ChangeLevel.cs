using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.Alpha1)){
            SceneManager.LoadScene("Prototype_Level_1");
        }
        else if (Input.GetKeyUp(KeyCode.Alpha2)){
            SceneManager.LoadScene("Prototype_Level_2");
        }
        else if (Input.GetKeyUp(KeyCode.Alpha3)){
            SceneManager.LoadScene("Prototype_Level_3");
        }
    }
}
