using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_keyHold : MonoBehaviour {

    public int keyNum = 0;
    public GameObject key;
    GameObject temp;
    bool spawned = false;
    Vector2 head;
    public float k_indicatorHeight;
    public float k_indicatorWidth;

    private P_throw pThrow;

    private void Start()
    {
        pThrow = GetComponent<P_throw>();
    }

    // Update is called once per frame
    void Update () {
        if(pThrow.spawnStone == 0)
        {
            head = new Vector2(transform.position.x - k_indicatorWidth, transform.position.y + k_indicatorHeight);
        }
        else
        {
            head = new Vector2(transform.position.x -1.3f, transform.position.y + k_indicatorHeight);
        }
        
        if (keyNum > 0)
        {
            if(!spawned)
            {
                spawned = true;
                temp = (GameObject)Instantiate(key, head, Quaternion.identity);
            }
            if(temp != null)
            {
                temp.transform.position = head;
            }
        }
        else
        {
            Destroy(temp);
            spawned = false;
        }
   
	}
}
