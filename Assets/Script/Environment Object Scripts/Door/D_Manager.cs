using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_Manager : MonoBehaviour {

    [SerializeField] private int doorNum;
    public GameObject[] p;

    private void Start()
    {
        p = new GameObject[2];
        p[0] = GameObject.FindGameObjectWithTag("Player");
        p[1] = GameObject.FindGameObjectWithTag("Player2");
    }

    // Update is called once per frame
    void Update () {
		for(int i=0; i<p.Length; i++){
            if (GetComponent<BoxCollider2D>().IsTouching(p[i].GetComponent<BoxCollider2D>()))
            {
                if(p[i].GetComponent<P_keyHold>().keyNum == doorNum){
                    Invoke("DoorOpen", 0.5f);
                    p[i].GetComponent<P_keyHold>().keyNum = 0;
                }
            }
        }
	}

    void DoorOpen(){
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
