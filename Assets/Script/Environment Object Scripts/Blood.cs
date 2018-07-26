using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour {

    GameObject temp;
    public GameObject blood;
    GameObject[] p = new GameObject[2];
    P_Death[] death = new P_Death[2];
    [Header("Position for blood spawning offset")]
    public float offsetX;
    public float offsetY;

    private Vector3 offset;

    void Start () {
        offset = new Vector3(offsetX, -offsetY);
        p[0] = GameObject.FindGameObjectWithTag("Player");
        p[1] = GameObject.FindGameObjectWithTag("Player2");
        death[0] = p[0].GetComponent<P_Death>();
        death[1] = p[1].GetComponent<P_Death>();
    }
	
	void Update () {
		for(int i=0; i<2; i++)
        {
            if(death[i].groundOnBlood)
            {
                temp = (GameObject)Instantiate(blood, death[i].forGroundToCheckBlood + offset, Quaternion.identity);
                death[i].groundOnBlood = false;
            }
        }
	}
}
