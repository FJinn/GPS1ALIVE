using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StonePile : MonoBehaviour {

    private GameObject[] p = new GameObject[2];
    [Header("Adjust stone projectile height")] //Changing speed Y to adjust stone projectile height
    public float height;
    [Header("Change the spawning stone sound radius")]
    public float soundRadius;

    // Use this for initialization
    void Start () {
        p[0] = GameObject.FindGameObjectWithTag("Player");
        p[1] = GameObject.FindGameObjectWithTag("Player2");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        for (int i = 0; i < p.Length; i++)
        {
            if (Input.GetKeyDown(p[i].GetComponent<P_controls>().KeyUse) && p[i].GetComponent<P_throw>().throwStance == false)
            {
                p[i].GetComponent<P_throw>().spawnStone = 1;
            }

            if (p[i].GetComponent<P_throw>().stoneTemp != null)
            {
                p[i].GetComponent<P_throw>().stoneTemp.GetComponent<S_SoundRadius>().s_soundRadius = soundRadius;
            }

            p[i].GetComponent<P_throw>().speedY = height;
        }
    }
}
