using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMovement : MonoBehaviour {

    public GameObject[] Enemies;

	// Use this for initialization
	void Start () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("PushPull"))
        {
            for(int i =0;i < Enemies.Length; i++)
            {
                Destroy(Enemies[i].GetComponent<E_Sound_Detection>());
                if(Enemies[i].GetComponent<E_Movement>().isNurse)
                {
                    Enemies[i].GetComponent<Animator>().Play("N_IdleAnim");
                }
                else
                {
                    Enemies[i].GetComponent<Animator>().Play("D_IdleAnim");
                }
                Destroy(Enemies[i].GetComponent<E_Movement>());
                Enemies[i].transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }
}
