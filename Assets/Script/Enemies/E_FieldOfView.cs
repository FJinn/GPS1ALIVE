using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_FieldOfView : MonoBehaviour {

    public GameObject[] Players;
    private Vector2 playerPos;
    public float Distance;

    private float dstEnemy;

    public LayerMask playerMask;
   
    public bool Detected;

    void Start()
    {
        Players = new GameObject[2];
        Players[0] = GameObject.FindGameObjectWithTag("Player");
        Players[1] = GameObject.FindGameObjectWithTag("Player2");
    }


    void DetectedEye()
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag("T_EnemyEyeLight"))
            {
                child.gameObject.GetComponent<E_DetectionIntensity>().fb_value += 10f;
            }
        }
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
                    if (!Detected && !hit.collider.gameObject.GetComponent<P_ShadowDetect>().P_isUnderShadow)
                    {
                        DetectedEye();
                        // gameObject.GetComponent<E_Sound_Detection>().EM_EyeLight.GetComponent<E_DetectionIntensity>().fb_value += 10f;
                        // Instantiate(Alerted_Spawn, transform.position - new Vector3(0,transform.localScale.y - 6f,0), transform.rotation);
                        Detected = true;
                    }

                }

                if (GetComponent<BoxCollider2D>().IsTouching(player.GetComponent<BoxCollider2D>()) && !Detected)
                {
                    DetectedEye();
                    transform.localScale = new Vector3(Mathf.Sign(playerPos.x), 1f, 1f);
                    Detected = true;
                }
            }

        }
        
    }



}
