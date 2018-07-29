using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StonePile : MonoBehaviour {

    private GameObject[] p = new GameObject[2];
    [Header("Adjust stone projectile height")] //Changing speed Y to adjust stone projectile height
    public float height;
    [Header("Change the spawning stone sound radius")]
    public float soundRadius;

    P_throw[] pThrow = new P_throw[2];

    // Use this for initialization
    void Start () {
        p[0] = GameObject.FindGameObjectWithTag("Player");
        p[1] = GameObject.FindGameObjectWithTag("Player2");
        pThrow[0] = p[0].GetComponent<P_throw>();
        pThrow[1] = p[1].GetComponent<P_throw>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        for (int i = 0; i < p.Length; i++)
        {
            if (Input.GetKeyDown(p[i].GetComponent<P_controls>().KeyUse) && pThrow[i].throwStance == false && pThrow[i].canUseStonePile && !pThrow[i].inTheAir)
            {
                if (collision.gameObject == p[i] /*&& Input.GetKeyDown(p[i].GetComponent<P_controls>().KeyUse) && p[i].GetComponent<P_throw>().throwStance == false*/)
                {
                    pThrow[i].spawnStone = 1;
                    pThrow[i].pickedUp = true;
                }
                pThrow[i].speedY = height;
            }

            if (pThrow[i].stoneTemp != null)
            {
                pThrow[i].stoneTemp.GetComponent<S_SoundRadius>().s_soundRadius = soundRadius;
            }
        }
    }
}
