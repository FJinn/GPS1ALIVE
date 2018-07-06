using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_FieldOfView : MonoBehaviour {

    public GameObject[] Players;
    private Vector2 playerPos;
    public float Distance;

    private float dstEnemy;

    public LayerMask playerMask;

    private GameObject myAlerted_Spawn;
    public GameObject Alerted_Spawn;
   
    public bool Detected;

    void Start()
    {
        Players = new GameObject[2];
        Players[0] = GameObject.FindGameObjectWithTag("Player");
        Players[1] = GameObject.FindGameObjectWithTag("Player2");
    }

    

    void Update()
    {
        foreach (GameObject player in Players)
        {
            dstEnemy = Vector2.Distance(player.transform.position, transform.position);

            if (dstEnemy < Distance)
            {
                playerPos = new Vector2(player.transform.transform.Find("Eye").position.x - transform.Find("Eye").position.x, player.transform.transform.Find("Eye").position.y - transform.Find("Eye").position.y);

                Physics2D.queriesStartInColliders = false;
                RaycastHit2D hit = Physics2D.Raycast(transform.Find("Eye").position, playerPos, dstEnemy, playerMask);

                if (hit.collider != null && Mathf.Sign(transform.localScale.x) == Mathf.Sign(playerPos.x) && (hit.collider.tag == "Player" || hit.collider.tag == "Player2"))
                {
                    if (!Detected && !hit.collider.GetComponent<P_ShadowDetect>().P_isUnderShadow)
                    {
                        GameObject myAlerted_Spawn = Instantiate(Alerted_Spawn, transform.position - new Vector3(0,transform.localScale.y - 17f,0), transform.rotation);
                        myAlerted_Spawn.GetComponent<E_AM_Destroy>().AM_enemy = this.gameObject;

                        Detected = true;
                    }

                }
                
            }

        }
        
    }



}
