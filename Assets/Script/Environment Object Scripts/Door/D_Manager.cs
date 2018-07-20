
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_Manager : MonoBehaviour {

    [SerializeField] private int doorNum;
    public GameObject[] p;
    public Animator doorUnlocked;

    private BoxCollider2D myBoxCollider;

    private void Start()
    {
        doorUnlocked = GetComponent<Animator>();
        p = new GameObject[2];
        p[0] = GameObject.FindGameObjectWithTag("Player");
        p[1] = GameObject.FindGameObjectWithTag("Player2");
        myBoxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update () {
		for(int i=0; i<p.Length; i++){
            if (myBoxCollider.IsTouching(p[i].GetComponent<BoxCollider2D>()))
            {
                if(p[i].GetComponent<P_keyHold>().keyNum == doorNum){
                    Invoke("DoorOpen", 0.5f);
                    p[i].GetComponent<P_keyHold>().keyNum = 0;
                }
            }
        }
	}

    void DoorOpen(){
        myBoxCollider.enabled = false;
        doorUnlocked.Play("DoorUnlocked");
    }

    
}
