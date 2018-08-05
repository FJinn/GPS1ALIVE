using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_BangingDoor : MonoBehaviour {

    public GameObject enemy;
    public Animator ac;
    private D_Manager doorScript;

    bool doorClosed = false;

    int duration;
    float count;

    private void Start()
    {
        doorScript = GetComponent<D_Manager>();
    }

    // Update is called once per frame
    void Update () {
		if(ac.GetCurrentAnimatorStateInfo(0).IsName("Doorlock"))
        {
            doorClosed = true;
            GetComponent<BoxCollider2D>().enabled = true;
        }

        if(doorClosed)
        {
            if (count >= duration)
            {
                enemy.SetActive(true);
            }
            else
            {
                count += Time.deltaTime;
            }
        }
	}
}
