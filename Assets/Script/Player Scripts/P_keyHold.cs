using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_keyHold : MonoBehaviour {

    public int keyNum = 0;
    public GameObject key;
    GameObject temp;
    bool spawned = false;
    Vector2 head;
    

    // Update is called once per frame
    void Update () {
        head = new Vector2(transform.position.x - 0.5f, transform.position.y + 7f);
        if (keyNum > 0)
        {
            if(!spawned)
            {
                spawned = true;
                temp = (GameObject)Instantiate(key, head, Quaternion.identity);
            }
            temp.transform.position = head;
        }
        else
        {
            Destroy(temp);
            spawned = false;
        }

	}
}
