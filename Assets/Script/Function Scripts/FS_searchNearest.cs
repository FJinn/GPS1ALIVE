using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FS_searchNearest : MonoBehaviour {
    

	// Use this for initialization
	void Start () {
		
	}
    
    public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = new GameObject[2];
        gos[0] = GameObject.FindGameObjectWithTag("Player");
        gos[1] = GameObject.FindGameObjectWithTag("Player2");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
